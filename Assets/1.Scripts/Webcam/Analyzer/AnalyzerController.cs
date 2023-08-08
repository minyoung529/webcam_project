using OpenCVForUnity.UnityUtils.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FaceController))]
public class AnalyzerController : MonoBehaviour
{
    private WebcamAnalyzer[] analyzers;
    private WebCamTextureToMatHelper webcamTextureHelper;

    void Awake()
    {
        webcamTextureHelper = GetComponent<WebCamTextureToMatHelper>();
        analyzers = GetComponents<WebcamAnalyzer>();

        webcamTextureHelper.onInitialized.AddListener(GetComponent<FaceController>().Initialize);

        foreach (WebcamAnalyzer analyzer in analyzers)
        {
            webcamTextureHelper.onInitialized.AddListener(analyzer.Initialize);
            webcamTextureHelper.onDisposed.AddListener(analyzer.OnDisposed);
        }
    }
}
