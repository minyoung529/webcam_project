using OpenCVForUnity.UnityUtils.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzerController : MonoBehaviour
{
    [SerializeField]
    private WebCamTextureToMatHelper webcamTextureHelper;
    private WebcamAnalyzer[] analyzers;

    [SerializeField]
    private FaceController faceController;

    void Awake()
    {
        analyzers = GetComponents<WebcamAnalyzer>();

        webcamTextureHelper.onInitialized.AddListener(faceController.Initialize);

        foreach (WebcamAnalyzer analyzer in analyzers)
        {
            webcamTextureHelper.onInitialized.AddListener(analyzer.Initialize);
            webcamTextureHelper.onDisposed.AddListener(analyzer.OnDisposed);
        }
    }
}
