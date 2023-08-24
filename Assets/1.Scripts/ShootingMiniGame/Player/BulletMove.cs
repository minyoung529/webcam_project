using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private void Update()
    {
        transform.Translate(transform.up * speed);
        Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (targetScreenPos.y > Screen.height + 20f || targetScreenPos.y < 0)
        {
            PoolManager.Instance.Push(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            PoolManager.Instance.Push(gameObject);
    }
}
