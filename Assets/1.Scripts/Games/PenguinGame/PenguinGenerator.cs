using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using Random = UnityEngine.Random;

public class PenguinGenerator : MonoBehaviour
{
    ObjectPool<Penguin> pool;

    [SerializeField]
    private Penguin penguinPrefab;

    [SerializeField]
    private Transform[] spawnPositions;

    private Penguin prevPenguin;
    public Penguin PrevPenguin => prevPenguin;

    [SerializeField]
    private Penguin startPenguin;

    [SerializeField]
    private Transform root;


    private void Awake()
    {
        pool = new(OnCreate, OnGet, OnRelease, OnDestroyed, true, 5);

        EventManager.StartListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StartListening(EventName.OnMiniGameStop, GameStop);
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionEnded, OnPenguinInstalled);  // Æë±Ï ¼³Ä¡ Á÷ÈÄ
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionSuccessed, UpdatePrevPenguin);  // Æë±Ï ¼³Ä¡ ¼º°ø
    }

    private void UpdatePrevPenguin(Penguin penguin)
    {
        prevPenguin = penguin;
    }

    private void OnPenguinInstalled(Penguin penguin)
    {
        Spawn();
    }

    private void DelayOneFrameSpawn()
    {
        StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return null;
        Spawn();
    }

    private void Spawn()
    {
        Penguin penguin = pool.Get();

        // left = 0, right = 1
        int rand = Random.Range(0, 2);
        bool isLeft = rand == 0;

        penguin.transform.position = spawnPositions[rand].position;
        penguin.IsLeft = isLeft;
    }

    private void GameStart()
    {
        prevPenguin = startPenguin;
        DelayOneFrameSpawn();
    }

    private void GameStop()
    {
    }

    private void GameOver()
    {
    }

    #region POOL
    Penguin OnCreate()
    {
        Penguin penguin = Instantiate(penguinPrefab, root);
        penguin.ReleaseAction += Release;
        penguin.OnCreated();
        return penguin;
    }

    private void Release(Penguin penguin)
    {
        pool.Release(penguin);
    }

    void OnGet(Penguin penguin)
    {
        penguin.OnGet();
    }

    void OnRelease(Penguin penguin)
    {
        penguin.OnRelease();
    }

    void OnDestroyed(Penguin penguin)
    {
        penguin.ReleaseAction -= Release;
        penguin.OnDestroyed();
    }
    #endregion

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StopListening(EventName.OnMiniGameStop, GameStop);
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionEnded, OnPenguinInstalled);  // Æë±Ï ¼³Ä¡ Á÷ÈÄ
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionSuccessed, UpdatePrevPenguin);  // Æë±Ï ¼³Ä¡ ¼º°ø
    }
}
