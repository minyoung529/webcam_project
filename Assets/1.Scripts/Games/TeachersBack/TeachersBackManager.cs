using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeachersBackManager : MonoBehaviour
{
    [SerializeField] private TeachersBackPlayer player;
    [SerializeField] private TeachersBackTeacher teacher;

    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private CanvasGroup failMenu;

    private float FullSnackCount = 10;
    private int snackCount = 0;
    public int SnackCount { get { return snackCount; } set { snackCount = value; UpdateCountText(); } }
    private float addValue => (1f / FullSnackCount);
    private float speed = 2f;
    private float minusSpeed = 0.1f;

    public void OnGameStart()
    {

        EventManager.TriggerEvent(EventName.OnMiniGameStart);
    }

    private void Start()
    {
        StartListening();
        OnGameStart();
    }

    private void Update()
    {
        if (scoreSlider.IsActive())
        {
            SliderValue();
            CheckSlider();
            CheckPlayerEating();
        }
    }

    private void CheckPlayerEating()
    {
        if (teacher.TeacherState == TeachersBackTeacherState.FINDING)
        {
            if (player.PlayerState == TeachersBackPlayerState.EAT) EventManager<TeachersBackPlayer>.TriggerEvent(EventName.OnMiniGameActionFailed, player);
        }
    }

    #region Game Flow

    private void GameStart()
    {
        ActiveUI();
        SnackCount = 0;

        failMenu.alpha = 0.0f;
        failMenu.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        InactiveUI();

        failMenu.alpha = 1.0f;
        failMenu.gameObject.SetActive(true);
    }

    #endregion

    #region UI

    private void SliderValue()
    {
        float value = (scoreSlider.value - addValue*minusSpeed);
        if (player.PlayerState == TeachersBackPlayerState.EAT)
        {
            value = (scoreSlider.value + (addValue*speed));
        }

        scoreSlider.value = Mathf.Lerp(scoreSlider.value, value, Time.deltaTime);
    }

    private void CheckSlider()
    {
        if (scoreSlider.value >= scoreSlider.maxValue)
        {
            UpgradeLevel();
        }
        else if (scoreSlider.value <= 0f)
        {
            EventManager.TriggerEvent(EventName.OnMiniGameOver);
            return;
        }
    }

    private void UpgradeLevel()
    {
        SnackCount++;
        FullSnackCount *= 2;
        minusSpeed += 0.2f;
        scoreSlider.value = 0.5f;
    }
    private void UpdateCountText()
    {
        countText.SetText(snackCount.ToString());
    }

    private void ActiveUI()
    {
        countText.gameObject.SetActive(true);
        scoreSlider.gameObject.SetActive(true);
    }
    private void InactiveUI()
    {
        countText.gameObject.SetActive(false);
        scoreSlider.gameObject.SetActive(false);
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

public enum TeachersBackPlayerState
{
    None,

    EAT,
    FAIL,

    COUNT
}

public enum TeachersBackTeacherState
{
    None,
    
    FINDING,
    SCOLD,

    COUNT
}