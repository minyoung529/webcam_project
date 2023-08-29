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

    private bool isSpawning = false;

    private void Awake()
    {
        poolDict = new Dictionary<Type, ObjectPool<DinoObstacle>>();

        for (int i = 0; i < obstaclePrefabs.Length; i++)
        {
            DinoObstacle prefab = obstaclePrefabs[i];

            ObjectPool<DinoObstacle> pool = new(() => OnCreate(prefab), OnGet, OnRelease, OnDestroyed, true, 2);
            poolDict.Add(prefab.GetType(), pool); ;
        }

        EventManager.StartListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StartListening(EventName.OnMiniGameStop, GameStop);
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
    }

    private void Update()
    {
        if (!isSpawning) return;

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
        Type type = obstaclePrefabs[rand].GetType();

        if (poolDict.ContainsKey(type))
        {
            poolDict[type].Get();        // ·£´ý Å¸ÀÔ
        }
    }

    private void GameStart()
    {
        isSpawning = true;
    }

    private void GameStop()
    {
        isSpawning = false;
    }

    private void GameOver()
    {
        isSpawning = false;
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

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StopListening(EventName.OnMiniGameStop, GameStop);
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
    }
}
