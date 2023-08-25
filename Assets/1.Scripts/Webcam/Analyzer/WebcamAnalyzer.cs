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

[RequireComponent(typeof(AnalyzerController))]
public abstract class WebcamAnalyzer : MonoBehaviour
{
    [SerializeField]
    protected InformationType informationType;

    [SerializeField]
    protected WebCamTextureToMatHelper webCamTextureToMatHelper;

    [SerializeField]
    protected bool runOnUpdate = false;

    [SerializeField]
    protected FaceController faceController;

    protected static readonly string FACE_RECOGNITION_MODEL_FILENAME = "OpenCVForUnity/dnn/face_recognition_sface_2021dec.onnx";

    protected bool initializeDone = false;

    private void Awake()
    {
        faceController.Event.StartListening((int)FaceEvent.OnRun, Run);
        faceController.Event.StartListening((int)FaceEvent.OnSetFilePath, FilePathSetting);
    }

    public virtual void Initialize()
    {
        initializeDone = true;
    }

    public abstract object GetInformation();

    protected abstract void OnChildDisposed();

    public virtual void Run()
    {
    }

    public void OnDisposed()
    {
        OnChildDisposed();
    }

    #region FilePath
    private void FilePathSetting()
    {
#if UNITY_WEBGL
        StartCoroutine(GetChildFilePath_WEBGL());
#else
        SetFilePath();
#endif
    }

    public virtual void SetFilePath() { }

    protected IEnumerator GetFilePathCoroutine(string fileName, Action<string> setFilePathAction)
    {
        var getFilePathAsync_0_Coroutine = Utils.getFilePathAsync(fileName, (result) =>
        {
            setFilePathAction?.Invoke(result);
        });
        yield return getFilePathAsync_0_Coroutine;
    }
    #endregion

    protected virtual IEnumerator GetChildFilePath_WEBGL()
    {
        yield return null;
    }
}
