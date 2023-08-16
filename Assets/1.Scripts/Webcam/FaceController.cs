using DlibFaceLandmarkDetector.UnityUtils;
using MediaPipe.BlazeFace;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnityExample.DnnModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.XR.ARSubsystems.XRCpuImage;

public class FaceController : MonoBehaviour
{
    public EventController Event { get; private set; } = new();
    protected WebCamTextureToMatHelper webCamTextureToMatHelper;

    #region Face Detection
    protected static readonly string FACE_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_recognition_sface_2021dec.onnx";
    protected static readonly string FACE_DETECTION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_detection_yunet_2023mar.onnx";

    public string face_detection_model_filepath { get; private set; }
    public string face_recognition_model_filepath { get; private set; }

    protected YuNetV2FaceDetector faceDetector;
    #endregion

    protected Mat bgrMat;
    public Mat BGRMat => bgrMat;
    protected Mat faceMat;
    public Mat Face => faceMat;

#if UNITY_WEBGL
        IEnumerator getFilePath_Coroutine;
#endif

    #region Window Size
    protected int inputSizeW = 320;
    protected int inputSizeH = 320;
    protected float scoreThreshold = 0.9f;
    protected float nmsThreshold = 0.3f;
    protected int topK = 5000;
    #endregion

    public void Initialize()
    {
        webCamTextureToMatHelper = GetComponent<WebCamTextureToMatHelper>();

#if UNITY_WEBGL
            getFilePath_Coroutine = GetFilePath();
            StartCoroutine(getFilePath_Coroutine);
#else
        face_detection_model_filepath = Utils.getFilePath(FACE_DETECTION_MODEL_FILENAME);
        face_recognition_model_filepath = Utils.getFilePath(FACE_RECOGNITION_MODEL_FILENAME);

        Event.Trigger((int)FaceEvent.OnSetFilePath);

        Run();
        Event.Trigger((int)FaceEvent.OnRun);
#endif
    }

    private void Update()
    {
        DetectFace();
    }

    public void DetectFace()
    {
        if (webCamTextureToMatHelper == null) return;

        Mat rgbaMat = webCamTextureToMatHelper.GetMat();

        if (rgbaMat == null) return;

        if (faceDetector == null)
        {
            Imgproc.putText(rgbaMat, "model file is not loaded.", new Point(5, rgbaMat.rows() - 30), Imgproc.FONT_HERSHEY_SIMPLEX, 0.7, new Scalar(255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
            Imgproc.putText(rgbaMat, "Please read console message.", new Point(5, rgbaMat.rows() - 10), Imgproc.FONT_HERSHEY_SIMPLEX, 0.7, new Scalar(255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
        }
        else
        {
            Imgproc.cvtColor(rgbaMat, bgrMat, Imgproc.COLOR_RGBA2BGR);
            faceMat = faceDetector.infer(bgrMat);
            Imgproc.cvtColor(bgrMat, rgbaMat, Imgproc.COLOR_BGR2RGBA);
        }
    }

#if UNITY_WEBGL
    private IEnumerator GetFilePath()
    {
        yield return GetFilePathCoroutine(FACE_DETECTION_MODEL_FILENAME, (x) => face_detection_model_filepath = x);
        yield return GetFilePathCoroutine(FACE_RECOGNITION_MODEL_FILENAME, (x) => face_recognition_model_filepath = x);

        Event.Trigger((int)FaceEvent.OnSetFilePath);

        yield return new WaitForSeconds(3f);

        getFilePath_Coroutine = null;

        Run();
    }
#endif

    public void Run()
    {
        //if true, The error log of the Native side OpenCV will be displayed on the Unity Editor Console.
        Utils.setDebugMode(true);

        if (string.IsNullOrEmpty(face_detection_model_filepath))
        {
            Debug.LogError(FACE_DETECTION_MODEL_FILENAME + " is not loaded. Please read ¡°StreamingAssets/OpenCVForUnity/dnn/setup_dnn_module.pdf¡± to make the necessary setup.");
        }
        else
        {
            faceDetector = new YuNetV2FaceDetector(face_detection_model_filepath, "", new Size(inputSizeW, inputSizeH), scoreThreshold, nmsThreshold, topK);
        }

#if UNITY_ANDROID && !UNITY_EDITOR
                // Avoids the front camera low light issue that occurs in only some Android devices (e.g. Google Pixel, Pixel2).
                webCamTextureToMatHelper.avoidAndroidFrontCameraLowLightIssue = true;
#endif

        Mat rgbaMat = webCamTextureToMatHelper.GetMat();
        Debug.Log(rgbaMat);
        bgrMat = new Mat(rgbaMat.rows(), rgbaMat.cols(), CvType.CV_8UC3);

        Event.Trigger((int)FaceEvent.OnRun);
    }

    public void OnDispose()
    {
        faceDetector?.dispose();
        Utils.setDebugMode(false);

#if UNITY_WEBGL
            if (getFilePath_Coroutine != null)
            {
                StopCoroutine(getFilePath_Coroutine);
                ((IDisposable)getFilePath_Coroutine).Dispose();
            }
#endif

        Event.Trigger((int)FaceEvent.OnDispose);
    }
}

public enum FaceEvent
{
    OnSetFilePath,
    OnRun,
    OnDispose,

    EmotionChanged,

    LeftEyeClose,
    RightEyeClose,
    LeftEyeOpen,
    RightEyeOpen,

    MouthClose,
    MouthOpen,

    Count
}