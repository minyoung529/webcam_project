using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    protected Transform[] posArr;

    protected void ResetTurn()
    {
        transform.DORotate(Vector3.zero, 0.1f);
    }
    public void MovePosition(Vector3 position)
    {
        float speed = Vector3.Distance(transform.position, position) / enemyStat.speed;
        transform.DORotate(new Vector3(0, 0, Quaternion.FromToRotation(Vector3.up, position - transform.position).eulerAngles.z), 0.3f);
        transform.DOMove(position, speed).OnComplete(ResetTurn);
    }
    public void MovePositions(Vector3[] position, bool isLoop = false,int loopNum=0)
    {
        transform.DORotate(new Vector3(0, 0, Quaternion.FromToRotation(Vector3.up, position[position.Length - 1] - transform.position).eulerAngles.z), 0.3f);
        transform.DOPath(position, 3f, PathType.CatmullRom,pathMode).OnComplete(ResetTurn).SetLoops((isLoop) ? -1 : loopNum).SetLookAt(0.01f);
    }
    public void StopMoving()
    {
        transform.DOKill();
    }
}
