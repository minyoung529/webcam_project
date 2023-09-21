using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestEnemyMove : MonoBehaviour
{
    [SerializeField]
    private List<Transform> mTargets = new List<Transform>();
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private PathType pathType;
    [SerializeField]
    private PathMode pathMode;
    private void Start()
    {
        Vector3[]curTargets = new Vector3[mTargets.Count];
        for(int i = 0; i < mTargets.Count; i++)
            curTargets[i] = mTargets[i].position;
        transform.DOPath(curTargets, 4f,pathType,pathMode).SetLookAt(0.001f).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
    }
}
