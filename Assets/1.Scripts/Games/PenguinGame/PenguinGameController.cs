using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PenguinGameController : MonoBehaviour
{
    #region STATIC
    private static float speed = START_SPEED;
    public static float Speed => speed;

    private static bool readyInput = true;
    public static bool ReadyInupt
    {
        get { return readyInput; }
        set { readyInput = value; }
    }

    #endregion

    private const float START_SPEED = 10f;

    private int score = 0;
    private int highScore = 0;

    private int hp = 0;
    private const int MAX_HP = 3;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    // hi, score
    public Action<float, float> OnScoreChange { get; set; }

    private PenguinGenerator generator;

    private FaceController faceController;

    private void Awake()
    {
        generator = GetComponent<PenguinGenerator>();

        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StartListening(EventName.OnMiniGameStart, OnGameStart);
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionSuccessed, OnGameSuccessed);
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionFailed, OnGameFailed);
    }

    private void OnGameFailed(Penguin penguin)
    {
        readyInput = true;
    }

    private void OnGameSuccessed(Penguin penguin)
    {
        score++;
    }

    private void Start()
    {
        EventManager.TriggerEvent(EventName.OnMiniGameStart);

        faceController = FindObjectOfType<FaceController>();

        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, BubblePop);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        // Æë±Ï ¹èÄ¡ 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BubblePop();
        }
    }
#endif

    public void BubblePop()
    {
        if (!ReadyInupt) return;

        EventManager<Penguin>.TriggerEvent(EventName.OnMiniGameActionStarted, generator.PrevPenguin);
        ReadyInupt = false;
    }

    public void OnGameStart()
    {
        isGameOver = false;
        readyInput = true;
        speed = START_SPEED;
        hp = MAX_HP;
        highScore = PlayerPrefs.GetInt("PENGUIN_HIGHSCORE");
        score = 0;
    }

    public void GameOver()
    {
        isGameOver = true;
        speed = 0f;
        readyInput = false;

        if (score > highScore)
        {
            highScore = score;
            score = 0;
            PlayerPrefs.SetInt("PENGUIN_HIGHSCORE", highScore);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StopListening(EventName.OnMiniGameStart, OnGameStart);

        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionSuccessed, OnGameSuccessed);
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionFailed, OnGameFailed);

        if (faceController)
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, BubblePop);
    }
}
