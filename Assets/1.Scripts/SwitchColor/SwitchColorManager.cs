using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwitchColorManager : MonoBehaviour
{

    [Header("Prefab")]
    [SerializeField] GameObject player;

    [SerializeField] float floorSpeed = 10f;
    [SerializeField] FloorBlock[] floors;


    [Header("Score UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    
    private Transform playerStartPos;
    
    private int score = 0;
    private int level = 1;

    private bool isGame = false;

    private void Start()
    {
        playerStartPos = player.transform;

        Init();
        StartGame();
    }

    #region Game Flow

    private void Init()
    {
        StartListening();
    }

    public void StartGame()
    {
        for (int i = 0; i < floors.Length; i++)
        {
            floors[i].SetSpeed(floorSpeed);
        }

        player.transform.position = playerStartPos.position;
        isGame = true;
        level = 1;

        ScoreTime();
    }
    
    public void StopGame(Action onEndEvent = null)
    {
        if (!isGame) return;

        Debug.Log("Fail");
        onEndEvent?.Invoke();

        player.transform.position = playerStartPos.position;
        isGame = false;
        StopCoroutine(ScoreUP());
    }

    public void LevelUP()
    {
        level += 1;
        for(int i=0;i<floors.Length;i++)
        {
            floors[i].SetSpeed(floorSpeed * level);
        }
    }

    #endregion

    #region Score

    private void ScoreTime()
    {
        score = 0;
        StartCoroutine(ScoreUP());
    }

    private IEnumerator ScoreUP()
    {
        while(isGame)
        {
            yield return new WaitForSeconds(1f);

            score += 1;
            if (score % 20 == 0) LevelUP();

            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.SetText(score.ToString());
    }

    #endregion

    #region Event

    private void StartListening()
    {
        EventManager<Action>.StartListening(EventName.OnSwitchColorFail, StopGame);
    }
    private void StopListening()
    {
        EventManager<Action>.StartListening(EventName.OnSwitchColorFail, StopGame);
    }

    #endregion

    private void OnDestroy()
    {
        StopListening();
    }

}
