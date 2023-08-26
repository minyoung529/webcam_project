using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using Random = UnityEngine.Random;

public class DinoObstacleGenerator : MonoBehaviour
{
    private float spawnInterval = 2f;
    private float counter = 0f;

    Dictionary<Type, ObjectPool<DinoObstacle>> poolDict;

    [SerializeField]
    private DinoObstacle[] obstaclePrefabs;

    [SerializeField]
    private Transform spawnPosition;

    private void Awake()
    {
        for(int i = 0; i < obstaclePrefabs.Length; i++)
        {
            ObjectPool<DinoObstacle> pool = new(() => OnCreate(obstaclePrefabs[i]), OnGet, OnRelease, OnDestroyed);
            poolDict.Add(obstaclePrefabs[i].GetType(), pool);;
        }
    }

    private void Update()
    {
        counter += Time.deltaTime;

        if (counter >= spawnInterval)
        {
            counter -= spawnInterval;
            Spawn();
        }
    }

    private void Spawn()
    {
        int rand = Random.Range(0, obstaclePrefabs.Length);     // 0 ~ Count-1
        poolDict[obstaclePrefabs[rand].GetType()].Get();        // ·£´ý Å¸ÀÔ
    }

    #region POOL
    DinoObstacle OnCreate(DinoObstacle prefab)
    {
        DinoObstacle obstacle = Instantiate(prefab);
        obstacle.ReleaseAction += Release;
        obstacle.OnCreated();
        return obstacle;
    }

    private void Release(DinoObstacle obstacle)
    {
        poolDict[obstacle.GetType()].Release(obstacle);
    }

    void OnGet(DinoObstacle obstacle)
    {
        obstacle.transform.position = spawnPosition.position;
        obstacle.OnGet();
    }

    void OnRelease(DinoObstacle obstacle)
    {
        obstacle.OnRelease();
    }

    void OnDestroyed(DinoObstacle obstacle)
    {
        obstacle.ReleaseAction -= Release;
        obstacle.OnDestroyed();
    }
    #endregion
}
