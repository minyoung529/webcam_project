#if !(PLATFORM_LUMIN && !UNITY_EDITOR)

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OpenCVForUnityExample
{
    /// <summary>
    /// WebCamTextureToMatHelper Example
    /// </summary>
    [RequireComponent(typeof(WebCamTextureToMatHelper))]
    public class WebCamTextureToMatHelperExample : MonoBehaviour
    {
        /// <summary>
        /// The requested resolution.
        /// </summary>
        public ResolutionPreset requestedResolution = ResolutionPreset._640x480;

        /// <summary>
        /// The requestedFPS.
        /// </summary>
        public FPSPreset requestedFPS = FPSPreset._30;

        /// <summary>
        /// The texture.
        /// </summary>
        Texture2D texture;

        /// <summary>
        /// The webcam texture to mat helper.
        /// </summary>
        [SerializeField]
        WebCamTextureToMatHelper webCamTextureToMatHelper;

        // Use this for initialization
        void Start()
        {
            // Get the WebCamTextureToMatHelper component attached to the current game object
            webCamTextureToMatHelper = GetComponent<WebCamTextureToMatHelper>();

            // Set the requested width, height, FPS and ColorFormat
            int width, height;
            Dimensions(requestedResolution, out width, out height);
            webCamTextureToMatHelper.requestedWidth = width;
            webCamTextureToMatHelper.requestedHeight = height;
            webCamTextureToMatHelper.requestedFPS = (int)requestedFPS;
            webCamTextureToMatHelper.outputColorFormat = WebCamTextureToMatHelper.ColorFormat.RGBA;

            // Initialize the webcam texture to Mat helper, which starts the webcam and prepares the conversion
            webCamTextureToMatHelper.Initialize();
        }

        /// <summary>
        /// Raises the webcam texture to mat helper initialized event.
        /// </summary>
        public void OnWebCamTextureToMatHelperInitialized()
        {
            Debug.Log(gameObject.name + "OnWebCamTextureToMatHelperInitialized");

            // Retrieve the current frame from the WebCamTextureToMatHelper as a Mat object
            Mat webCamTextureMat = webCamTextureToMatHelper.GetMat();

            // Create a new Texture2D with the same dimensions as the Mat and RGBA32 color format
            texture = new Texture2D(webCamTextureMat.cols(), webCamTextureMat.rows(), TextureFormat.RGBA32, false);

            // Convert the Mat to a Texture2D, effectively transferring the image data
            Utils.matToTexture2D(webCamTextureMat, texture);

            // Set the Texture2D as the main texture of the Renderer component attached to the game object
            Renderer renderer = gameObject.GetComponent<Renderer>();

            if (renderer)
            {
                renderer.material.mainTexture = texture;
            }
            else
            {
                Image image = GetComponent<Image>();
                RawImage rawImage = GetComponent<RawImage>();

                if(image)
                {
                    image.material.mainTexture = texture;
                }
                else if(rawImage)
                {
                    rawImage.material.mainTexture = texture;
                }
                else
                {
                    Debug.LogError("WEBCAM ERROR OCCURRED: Renderer is Null!");
                }
            }

            // Adjust the scale of the game object to match the dimensions of the texture
            //gameObject.transform.localScale = new Vector3(webCamTextureMat.cols(), webCamTextureMat.rows(), 1);
            //Debug.Log("Screen.width " + Screen.width + " Screen.height " + Screen.height + " Screen.orientation " + Screen.orientation);

            // Get the width and height of the webCamTextureMat
            float width = webCamTextureMat.width();
            float height = webCamTextureMat.height();

            // Calculate the scale factors for width and height based on the screen dimensions
            float widthScale = (float)Screen.width / width;
            float heightScale = (float)Screen.height / height;

            // Adjust the orthographic size of the main Camera to fit the aspect ratio of the image
            if (widthScale < heightScale)
            {
                // If the width scale is smaller, adjust the orthographic size based on width and screen height
                Camera.main.orthographicSize = (width * (float)Screen.height / (float)Screen.width) / 2;
            }
            else
            {
                // If the height scale is smaller or equal, adjust the orthographic size based on height
                Camera.main.orthographicSize = height / 2;
            }
        }

        /// <summary>
        /// Raises the webcam texture to mat helper disposed event.
        /// </summary>
        public void OnWebCamTextureToMatHelperDisposed()
        {
            Debug.Log("OnWebCamTextureToMatHelperDisposed");

            // Destroy the texture and set it to null
            if (texture != null)
            {
                Texture2D.Destroy(texture);
                texture = null;
            }
        }

        /// <summary>
        /// Raises the webcam texture to mat helper error occurred event.
        /// </summary>
        /// <param name="errorCode">Error code.</param>
        public void OnWebCamTextureToMatHelperErrorOccurred(WebCamTextureToMatHelper.ErrorCode errorCode)
        {
            Debug.Log("OnWebCamTextureToMatHelperErrorOccurred " + errorCode);
        }

        // Update is called once per frame
        void Update()
        {
            // Check if the web camera is playing and if a new frame was updated
            if (webCamTextureToMatHelper.IsPlaying() && webCamTextureToMatHelper.DidUpdateThisFrame())
            {
                // Retrieve the current frame as a Mat object
                Mat rgbaMat = webCamTextureToMatHelper.GetMat();

                // Add text overlay on the frame
                //Imgproc.putText (rgbaMat, "W:" + rgbaMat.width () + " H:" + rgbaMat.height () + " SO:" + Screen.orientation, new Point (5, rgbaMat.rows () - 10), Imgproc.FONT_HERSHEY_SIMPLEX, 1.0, new Scalar (255, 255, 255, 255), 2, Imgproc.LINE_AA, false);

                // Convert the Mat to a Texture2D to display it on a texture
                Utils.matToTexture2D(rgbaMat, texture);
            }
        }

        /// <summary>
        /// Raises the destroy event.
        /// </summary>
        void OnDestroy()
        {
            // Dispose of the webCamTextureToMatHelper object and release any resources held by it.
            webCamTextureToMatHelper.Dispose();
        }

        /// <summary>
        /// Raises the back button click event.
        /// </summary>
        public void OnBackButtonClick()
        {
            // Load the specified scene when the back button is clicked
            SceneManager.LoadScene("OpenCVForUnityExample");
        }

        /// <summary>
        /// Raises the play button click event.
        /// </summary>
        public void OnPlayButtonClick()
        {
            webCamTextureToMatHelper.Play();
        }

        /// <summary>
        /// Raises the pause button click event.
        /// </summary>
        public void OnPauseButtonClick()
        {
            webCamTextureToMatHelper.Pause();
        }

        /// <summary>
        /// Raises the stop button click event.
        /// </summary>
        public void OnStopButtonClick()
        {
            webCamTextureToMatHelper.Stop();
        }

        /// <summary>
        /// Raises the change camera button click event.
        /// </summary>
        public void OnChangeCameraButtonClick()
        {
            webCamTextureToMatHelper.requestedIsFrontFacing = !webCamTextureToMatHelper.requestedIsFrontFacing;
        }

        /// <summary>
        /// Raises the requested resolution value changed event.
        /// </summary>
        public void OnRequestedResolutionValueChanged(int result)
        {
            if ((int)requestedResolution != result)
            {
                requestedResolution = (ResolutionPreset)result;

                int width, height;
                Dimensions(requestedResolution, out width, out height);

                webCamTextureToMatHelper.Initialize(width, height);
            }
        }

        /// <summary>
        /// Raises the requestedFPS value changed event.
        /// </summary>
        public void OnRequestedFPSValueChanged(int result)
        {
            string[] enumNames = Enum.GetNames(typeof(FPSPreset));
            int value = (int)System.Enum.Parse(typeof(FPSPreset), enumNames[result], true);

            if ((int)requestedFPS != value)
            {
                requestedFPS = (FPSPreset)value;

                webCamTextureToMatHelper.requestedFPS = (int)requestedFPS;
            }
        }

        /// <summary>
        /// Raises the rotate 90 degree toggle value changed event.
        /// </summary>
        public void SetRotate90DegreeToggleValueChanged(bool isRotate)
        {
            webCamTextureToMatHelper.rotate90Degree = isRotate;
        }

        /// <summary>
        /// Raises the flip vertical toggle value changed event.
        /// </summary>
        public void SetFlipVerticalToggleValueChanged(bool isFlipped)
        {
            webCamTextureToMatHelper.flipVertical = isFlipped;
        }

        /// <summary>
        /// Raises the flip horizontal toggle value changed event.
        /// </summary>
        public void OnFlipHorizontalToggleValueChanged(bool isFlipped)
        {
            webCamTextureToMatHelper.flipHorizontal = isFlipped;
        }

        public enum FPSPreset : int
        {
            _0 = 0,
            _1 = 1,
            _5 = 5,
            _10 = 10,
            _15 = 15,
            _30 = 30,
            _60 = 60,
        }

        public enum ResolutionPreset : byte
        {
            _50x50 = 0,
            _640x480,
            _1280x720,
            _1920x1080,
            _9999x9999,
        }

        private void Dimensions(ResolutionPreset preset, out int width, out int height)
        {
            switch (preset)
            {
                case ResolutionPreset._50x50:
                    width = 50;
                    height = 50;
                    break;
                case ResolutionPreset._640x480:
                    width = 640;
                    height = 480;
                    break;
                case ResolutionPreset._1280x720:
                    width = 1280;
                    height = 720;
                    break;
                case ResolutionPreset._1920x1080:
                    width = 1920;
                    height = 1080;
                    break;
                case ResolutionPreset._9999x9999:
                    width = 9999;
                    height = 9999;
                    break;
                default:
                    width = height = 0;
                    break;
            }
        }
    }
}

#endif