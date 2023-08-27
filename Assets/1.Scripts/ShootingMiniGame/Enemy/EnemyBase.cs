using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private EnemyStat enemyStat = new EnemyStat();
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Transform[] posArr;
    [SerializeField]
    private PathMode pathMode;

    /// <summary>
    /// ex
    /// </summary>
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //        MovePosition(pos.position);
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        Vector3[] curSetV3 = new Vector3[posArr.Length];
    //        for (int i = 0; i < posArr.Length; i++)
    //        {
    //            curSetV3[i] = posArr[i].position;
    //        }
    //        MovePositions(curSetV3);
    //    }
    //}
    private void ResetTurn()
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
