using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private Transform bulletPool;
    [SerializeField]
    private float instantiateRange = 2f;
    private bool isFire = false;
    private void Start()
    {
        StartCoroutine(InstantiateBullet());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isFire = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFire = false;
        }
    }
    private IEnumerator InstantiateBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(instantiateRange);
            if (isFire)
            {
                GameObject bullet = PoolManager.Instance.GetPoolObject("PlayerBullet");
                bullet.transform.position = fireTransform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.transform.SetParent(bulletPool);
                print("fire!");
            }
        }
    }
}
