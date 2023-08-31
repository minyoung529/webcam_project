using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isPlayer;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        bool doesOverCam = false;
        if (isPlayer)
        {
            moveDir = transform.up;
            doesOverCam = targetScreenPos.y > Screen.height + 20f || targetScreenPos.y < 0;
        }
        else
        {
            moveDir = transform.up * -1;
            doesOverCam = targetScreenPos.y < Screen.height - 20f || targetScreenPos.y < 0;
        }
        transform.Translate(moveDir * speed);
        if (doesOverCam)
        {
            PoolManager.Instance.Push(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer)
        {
            if (other.gameObject.CompareTag("Enemy"))
                PoolManager.Instance.Push(gameObject);
            return;
        }
        if (other.gameObject.CompareTag("Player"))
            PoolManager.Instance.Push(gameObject);
    }
}
