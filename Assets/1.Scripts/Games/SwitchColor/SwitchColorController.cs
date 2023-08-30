using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColorController : MonoBehaviour
{
    [SerializeField] float floorSpeed = 10f;
    [SerializeField] FloorBlock[] floors;

    private int score = 0;
    private int level = 1;

    private bool isGame = false;

    SwitchColorUI ui;
    SwitchColorUI UI => ui;

    private void Awake()
    {
        ui = GetComponent<SwitchColorUI>();
    }

    private void Start()
    {
        StartListening();
        OnGameStart();
    }

    public void OnGameStart()
    {
        EventManager.TriggerEvent(EventName.OnMiniGameStart);
    }

    private void StartGame()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floorSpeed);
        }

        isGame = true;
        level = 1;

        ScoreTime();
    }

    private void StopGame()
    {
        if (!isGame) return;

        isGame = false;
        StopCoroutine(ScoreUP());

        UI.GameOverUI(score);
    }

    private void LevelUP()
    {
        level += 1;
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floorSpeed * level);
        }
    }

    #region Score

    private void ScoreTime()
    {
        score = 0;
        StartCoroutine(ScoreUP());
    }

    private IEnumerator ScoreUP()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(1f);

            score += 1;
            if (score % 20 == 0) LevelUP();

            UI.UpdateScoreUI(score);
        }
    }
    #endregion

    #region Event

    private void StartListening()
    {
        EventManager.StartListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StartListening(EventName.OnMiniGameStart, StartGame);

    }
    

    private void StopListening()
    {
        EventManager.StopListening(EventName.OnMiniGameOver, StopGame);
        EventManager.StopListening(EventName.OnMiniGameStart, StartGame);
    }

    #endregion

    private void OnDestroy()
    {
        StopListening();
    }
}
