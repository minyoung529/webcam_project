using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

interface Shootable
{
    abstract void InstantiateBullet();
    abstract void GetFireTransform();
    abstract void GetBullet();
}
public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    protected EnemyStat enemyStat = new EnemyStat();
    [SerializeField]
    protected PathMode pathMode;
    [SerializeField]
    protected MeshRenderer modelingMaterial;
    protected Transform[] posArr;

    private void Start()
    {
        if (modelingMaterial == null) return;
        modelingMaterial.material.EnableKeyword("_EMISSION");
        modelingMaterial.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

    }
    protected void ResetTurn()
    {
        transform.DORotate(Vector3.zero, 0.1f);
    }
    public void MovePosition(Vector3 position, Action tweenCallback = null)
    {
        float speed = Vector3.Distance(transform.position, position) / enemyStat.speed;
        transform.DORotate(new Vector3(0, 0, Quaternion.FromToRotation(Vector3.up, position - transform.position).eulerAngles.z), 0.3f);
        if (tweenCallback != null)
            transform.DOMove(position, speed).OnComplete(ResetTurn).OnComplete(() => { tweenCallback.Invoke(); });
        else
            transform.DOMove(position, speed).OnComplete(ResetTurn);
    }
    public void MovePositions(Vector3[] position, bool isLoop = false, int loopNum = 0)
    {
        //transform.DORotate(new Vector3(0, 0, Quaternion.FromToRotation(Vector3.up, position[position.Length - 1] - transform.position).eulerAngles.z), 0.3f);
        transform.DOPath(position, 3f, PathType.CatmullRom, pathMode).OnComplete(ResetTurn).SetLoops((isLoop) ? -1 : loopNum).SetLookAt(0.001f);
    }
    public void StopMoving()
    {
        transform.DOKill();
    }
    public void GetAttack()
    {
        enemyStat.hp--;
        StartCoroutine(ChangeEmission(Color.red));
    }

    protected IEnumerator ChangeEmission(Color color)
    {
        modelingMaterial.material.SetColor("_EmissionColor", color);
        yield return new WaitForSeconds(0.3f);
        modelingMaterial.material.SetColor("_EmissionColor", Color.black);
    }
}
