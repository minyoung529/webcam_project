using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroassantEnemy : EnemyBase
{
    [SerializeField]
    private float attackRadius = 2;
    private void Start()
    {
        Attack(attackRadius);
    }
    public void Attack(float radius)
    {
        Vector3 endPos = transform.position;
        Vector3 target = FindObjectOfType<PlayerMove>().transform.position;
        Vector3[] dir = new Vector3[4];
        dir[0] = target + new Vector3(radius, 0, 0);
        dir[1] = target + new Vector3(0,-radius, 0);
        dir[2] = target + new Vector3(-radius,0, 0);
        dir[3] = endPos;
        MovePositions(dir);
    }
}
