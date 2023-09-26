using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchColorController : MonoBehaviour
{
    [SerializeField] FloorBlock[] floors;

    private float[] zPos = { 30, 5, -20, -45 };

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
    }


    private void GameStart()
    {
        if (isGame) return;
        UI.OffGameOverUI();

        for (int i = 0; i < floors.Length; i++)
        {
            Vector3 pos = new Vector3(0, -20, zPos[i]);
            floors[i].transform.position = pos;
        }

        isGame = true;
        level = 1;
        ScoreTime();
    }

    private void GameOver()
    {
        if (!isGame) return;

        isGame = false;
        StopCoroutine(ScoreUP());

        UI.GameOverUI();
    }

    private void LevelUP()
    {
        level += 1;
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floors[i].Speed * level);
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
        EventManager.StartListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);

    }


    private void StopListening()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, GameStart);
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
    }

    #endregion

    private void OnDestroy()
    {
        StopListening();
    }
}
