using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class DinoGameController : MonoBehaviour
{
    private const float START_SPEED = 10f;
    private static float speed = START_SPEED;
    public static float Speed => speed;

    private float score = 0f;
    private float highScore = 0f;

    private bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    // hi, score
    public Action<float, float> OnScoreChange { get; set; }

    private void Awake()
    {
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StartListening(EventName.OnMiniGameStart, OnGameStart);
    }

    private void Start()
    {
        EventManager.TriggerEvent(EventName.OnMiniGameStart);
    }

    private void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime * 10f;
            OnScoreChange?.Invoke(highScore, score);
        }
    }

    public void OnGameStart()
    {
        isGameOver = false;
        speed = START_SPEED;

        highScore = PlayerPrefs.GetFloat("DINO_HIGHSCORE");
    }

    public void GameOver()
    {
        isGameOver = true;
        speed = 0f;

        if (score > highScore)
        {
            highScore = score;
            score = 0f;
            PlayerPrefs.SetFloat("DINO_HIGHSCORE", highScore);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StopListening(EventName.OnMiniGameStart, OnGameStart);
    }
}
