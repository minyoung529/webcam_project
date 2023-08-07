using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgcodecsModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnityExample.DnnModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WebcamAnalyzer : MonoBehaviour
{
    [SerializeField]
    protected InformationType informationType;

    protected Mat bgrMat;
    protected WebCamTextureToMatHelper webCamTextureToMatHelper;

    #region Face Detection
    protected static readonly string FACE_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_recognition_sface_2021dec.onnx";
    protected static readonly string FACE_DETECTION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_detection_yunet_2023mar.onnx";

    protected string face_detection_model_filepath;
    protected string face_recognition_model_filepath;

    protected YuNetV2FaceDetector faceDetector;
    #endregion

    #region Window Size
    protected int inputSizeW = 320;
    protected int inputSizeH = 320;
    protected float scoreThreshold = 0.9f;
    protected float nmsThreshold = 0.3f;
    protected int topK = 5000;
    #endregion

#if UNITY_WEBGL
        IEnumerator getFilePath_Coroutine;
#endif

    public virtual void Initialize()
    {
        webCamTextureToMatHelper = GetComponent<WebCamTextureToMatHelper>();

#if UNITY_WEBGL
            getFilePath_Coroutine = GetFilePath();
            StartCoroutine(getFilePath_Coroutine);
#else
        face_detection_model_filepath = Utils.getFilePath(FACE_DETECTION_MODEL_FILENAME);
        face_recognition_model_filepath = Utils.getFilePath(FACE_RECOGNITION_MODEL_FILENAME);
        SetFilePath();
        Run();
#endif
    }

    public abstract object GetInformation();

    protected abstract void OnChildDisposed();

    public virtual void Run()
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
    }

    public void OnDisposed()
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

        OnChildDisposed();
    }

    #region FilePath

    public virtual void SetFilePath()
    {

    }

    protected IEnumerator GetFilePathCoroutine(string fileName, Action<string> setFilePathAction)
    {
        var getFilePathAsync_0_Coroutine = Utils.getFilePathAsync(fileName, (result) =>
        {
            setFilePathAction?.Invoke(result);
        });
        yield return getFilePathAsync_0_Coroutine;
    }


#if UNITY_WEBGL
    private IEnumerator GetFilePath()
    {
        yield return GetFilePathCoroutine(FACE_DETECTION_MODEL_FILENAME, (x) => face_detection_model_filepath = x);
        yield return GetFilePathCoroutine(FACE_RECOGNITION_MODEL_FILENAME, (x) => face_recognition_model_filepath = x);

        getFilePath_Coroutine = null;

        Run();
    }
#endif

    #endregion

    protected virtual IEnumerator GetChildFilePath_WEBGL()
    {
        yield return null;
    }
}
