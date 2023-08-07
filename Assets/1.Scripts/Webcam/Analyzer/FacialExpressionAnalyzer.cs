using MediaPipe.BlazeFace;
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

public enum Emotion
{
    Angry,
    Disgust,
    Fearful,
    Happy,
    Neutral,
    Sad,
    Surprised,

    Count
}

public class FacialExpressionAnalyzer : WebcamAnalyzer
{
    private FacialExpressionRecognizer recognizer;

    string facial_expression_recognition_model_filepath;

    protected static readonly string FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/facial_expression_recognition_mobilefacenet_2022july.onnx";

    public override void Initialize()
    {
        base.Initialize();

        recognizer = new FacialExpressionRecognizer(facial_expression_recognition_model_filepath, face_recognition_model_filepath, "");
    }

    protected override IEnumerator GetChildFilePath_WEBGL()
    {
        yield return GetFilePathCoroutine(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME, (x) => facial_expression_recognition_model_filepath = x);
    }

    public override void SetFilePath()
    {
        facial_expression_recognition_model_filepath = Utils.getFilePath(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME);
    }


    protected override void OnChildDisposed()
    {
        recognizer?.dispose();
    }

    // Use this for initialization
    public override void Run()
    {
        base.Run();

        if (string.IsNullOrEmpty(facial_expression_recognition_model_filepath) || string.IsNullOrEmpty(face_recognition_model_filepath))
        {
            Debug.LogError(FACIAL_EXPRESSION_RECOGNITION_MODEL_FILENAME + " or " + FACE_RECOGNITION_MODEL_FILENAME + " is not loaded. Please read ¡°StreamingAssets/OpenCVForUnity/dnn/setup_dnn_module.pdf¡± to make the necessary setup.");
        }
        else
        {
            recognizer = new FacialExpressionRecognizer(facial_expression_recognition_model_filepath, face_recognition_model_filepath, "");
        }
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

            Mat faces = faceDetector.infer(bgrMat);

            List<Mat> expressions = new List<Mat>();

            // Estimate the expression of each face
            for (int i = 0; i < faces.rows(); ++i)
            {
                // Facial expression recognizer inference
                Mat facialExpression = recognizer.infer(bgrMat, faces.row(i));

                if (!facialExpression.empty())
                    expressions.Add(facialExpression);
            }

            Imgproc.cvtColor(bgrMat, rgbaMat, Imgproc.COLOR_BGR2RGBA);

            //faceDetector.visualize(rgbaMat, faces, false, true);
            return recognizer.GetData(expressions);
        }
        return null;
    }
}
