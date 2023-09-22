using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CroassantEnemy : EnemyBase
{
    [SerializeField]
    private float attackRadius = 2;
    [SerializeField]
    private float turnNum = 2.5f;
    [SerializeField]
    private bool clockwise = true;
    [Space(20)]
    [SerializeField]
    private float angle = 0f;
    [SerializeField]
    private float turns = 0;

    private bool isTurning = false;
    private Vector3 center;
    private Vector3 endPos;
    private void Start()
    {
        Attack(attackRadius);
    }
    private void Update()
    {
        if (isTurning)
        {
            Circle();
        }
    }
    public void Attack(float radius)
    {
        Vector3 first = transform.position;
        Vector3 target = FindObjectOfType<PlayerMove>().transform.position + new Vector3(radius, 0, 0);
        MovePosition(target, () => InitTurn(first, target - new Vector3(radius, 0, 0), true));
    }
    private void InitTurn(Vector3 endPos, Vector3 center, bool isTurning, float turns = 0, float angle = 0)
    {
        this.endPos = endPos;
        this.center = center;
        this.isTurning = isTurning;
        this.turns = turns;
        this.angle = angle;
    }
    private void Circle()
    {
        if (turns >= turnNum)
        {
            MovePosition(endPos);
            InitTurn(Vector3.zero, Vector3.zero, false);
            return;
        }
        angle += (clockwise ? -1 : 1) * enemyStat.speed * Time.deltaTime;
        //move and turn circle
        float x = center.x + attackRadius * Mathf.Cos(angle);
        float y = center.y + attackRadius * Mathf.Sin(angle);
        Vector3 dir = new Vector3(x, y, center.z); 
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Quaternion.FromToRotation(Vector3.up, dir - transform.position).eulerAngles.z));
        transform.position = dir;

        if (angle>=9f||angle<=-9f)
        {
            turns += 0.25f;
        }
    }
}
