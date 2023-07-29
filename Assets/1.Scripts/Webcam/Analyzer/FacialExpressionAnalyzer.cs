using MediaPipe.BlazeFace;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgcodecsModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using OpenCVForUnityExample.DnnModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionAnalyzer : WebcamAnalyzer
{
    private FacialExpressionRecognizer recognizer;
    private YuNetV2FaceDetector faceDetector;

    string facial_expression_recognition_model_filepath;
    string face_detection_model_filepath;
    string face_recognition_model_filepath;

    protected static readonly string FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/facial_expression_recognition_mobilefacenet_2022july.onnx";
    protected static readonly string FACE_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_recognition_sface_2021dec.onnx";
    protected static readonly string FACE_DETECTION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_detection_yunet_2023mar.onnx";

    private Texture2D texture;
    Mat bgrMat;

    private WebCamTextureToMatHelper webCamTextureToMatHelper;

    int inputSizeW = 320;
    int inputSizeH = 320;
    float scoreThreshold = 0.9f;
    float nmsThreshold = 0.3f;
    int topK = 5000;

    private void Start()
    {
        webCamTextureToMatHelper = GetComponent<WebCamTextureToMatHelper>();
        texture = webCamTextureToMatHelper.GetWebcamTexture2D();

#if UNITY_WEBGL
            getFilePath_Coroutine = GetFilePath();
            StartCoroutine(getFilePath_Coroutine);
#else
        face_detection_model_filepath = Utils.getFilePath(FACE_DETECTION_MODEL_FILENAME);
        facial_expression_recognition_model_filepath = Utils.getFilePath(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME);
        face_recognition_model_filepath = Utils.getFilePath(FACE_RECOGNITION_MODEL_FILENAME);
        Run();
#endif

        recognizer = new FacialExpressionRecognizer(facial_expression_recognition_model_filepath, face_recognition_model_filepath, "");
    }

#if UNITY_WEBGL
        private IEnumerator GetFilePath()
        {
            var getFilePathAsync_0_Coroutine = Utils.getFilePathAsync(FACE_DETECTION_MODEL_FILENAME, (result) =>
            {
                face_detection_model_filepath = result;
            });
            yield return getFilePathAsync_0_Coroutine;

            var getFilePathAsync_1_Coroutine = Utils.getFilePathAsync(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME, (result) =>
            {
                facial_expression_recognition_model_filepath = result;
            });
            yield return getFilePathAsync_1_Coroutine;

            var getFilePathAsync_2_Coroutine = Utils.getFilePathAsync(FACE_RECOGNITION_MODEL_FILENAME, (result) =>
            {
                face_recognition_model_filepath = result;
            });
            yield return getFilePathAsync_2_Coroutine;

            getFilePath_Coroutine = null;

            Run();
        }
#endif

    private void Update()
    {
        GetInformation();
    }

    public override void Calculate()
    {
    }

    public override void OnDestroyed()
    {
        faceDetector?.dispose();
        recognizer?.dispose();

        Utils.setDebugMode(false);

#if UNITY_WEBGL
            if (getFilePath_Coroutine != null)
            {
                StopCoroutine(getFilePath_Coroutine);
                ((IDisposable)getFilePath_Coroutine).Dispose();
            }
#endif
    }

    // Use this for initialization
    void Run()
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

        if (string.IsNullOrEmpty(facial_expression_recognition_model_filepath) || string.IsNullOrEmpty(face_recognition_model_filepath))
        {
            Debug.LogError(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME + " or " + FACE_RECOGNITION_MODEL_FILENAME + " is not loaded. Please read ¡°StreamingAssets/OpenCVForUnity/dnn/setup_dnn_module.pdf¡± to make the necessary setup.");
        }
        else
        {
            recognizer = new FacialExpressionRecognizer(facial_expression_recognition_model_filepath, face_recognition_model_filepath, "");
        }


#if UNITY_ANDROID && !UNITY_EDITOR
                // Avoids the front camera low light issue that occurs in only some Android devices (e.g. Google Pixel, Pixel2).
                webCamTextureToMatHelper.avoidAndroidFrontCameraLowLightIssue = true;
#endif
        //webCamTextureToMatHelper.Initialize();
    }

    public override object GetInformation()
    {
        Mat rgbaMat = webCamTextureToMatHelper.GetMat();

        if (faceDetector == null || recognizer == null)
        {
            Imgproc.putText(rgbaMat, "model file is not loaded.", new Point(5, rgbaMat.rows() - 30), Imgproc.FONT_HERSHEY_SIMPLEX, 0.7, new Scalar(255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
            Imgproc.putText(rgbaMat, "Please read console message.", new Point(5, rgbaMat.rows() - 10), Imgproc.FONT_HERSHEY_SIMPLEX, 0.7, new Scalar(255, 255, 255, 255), 2, Imgproc.LINE_AA, false);
        }
        else
        {
            Imgproc.cvtColor(rgbaMat, bgrMat, Imgproc.COLOR_RGBA2BGR);

            //TickMeter tm = new TickMeter();
            //tm.start();

            Mat faces = faceDetector.infer(bgrMat);

            //tm.stop();
            //Debug.Log("YuNetFaceDetector Inference time, ms: " + tm.getTimeMilli());

            List<Mat> expressions = new List<Mat>();

            // Estimate the expression of each face
            for (int i = 0; i < faces.rows(); ++i)
            {
                //tm.reset();
                //tm.start();

                // Facial expression recognizer inference
                Mat facialExpression = recognizer.infer(bgrMat, faces.row(i));

                //tm.stop();
                //Debug.Log("FacialExpressionRecognizer Inference time (preprocess + infer + postprocess), ms: " + tm.getTimeMilli());

                if (!facialExpression.empty())
                    expressions.Add(facialExpression);
            }

            Imgproc.cvtColor(bgrMat, rgbaMat, Imgproc.COLOR_BGR2RGBA);

            //faceDetector.visualize(rgbaMat, faces, false, true);
            recognizer.visualize(rgbaMat, expressions, faces, false, true);
        }

        Utils.matToTexture2D(rgbaMat, texture);
        return null;
    }
}
