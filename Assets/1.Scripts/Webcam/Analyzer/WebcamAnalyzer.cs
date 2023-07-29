using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WebcamAnalyzer : MonoBehaviour
{
    [SerializeField]
    private InformationType informationType;


    private void Awake()
    {
        Initialize();
    }

    public virtual void Initialize() { }
    public abstract void Calculate();

    public abstract object GetInformation();

    public abstract void OnDestroyed();

    private void OnDestroy()
    {
        OnDestroyed();
    }
}
