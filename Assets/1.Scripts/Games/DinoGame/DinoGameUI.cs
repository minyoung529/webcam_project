using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DinoGameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private CanvasGroup gameOverCanvas;

    private DinoGameController gameController;


    private void Start()
    {
        gameController = FindObjectOfType<DinoGameController>();
        gameController.OnScoreChange += UpdateUI;

        EventManager.StartListening(EventName.OnMiniGameStart, InactiveGameOverUI);
        EventManager.StartListening(EventName.OnMiniGameOver, ActiveGameOverUI);
    }

    private void UpdateUI(float hi, float score)
    {
        // HI ___
        // ____
        StringBuilder builder = new StringBuilder();
        builder.Append("HI ");
        builder.Append(string.Format("{0:D5}", Mathf.RoundToInt(hi)));
        builder.AppendLine();
        builder.Append(string.Format("{0:D5}", Mathf.RoundToInt(score)));

        scoreText.text = builder.ToString();
    }

    private void InactiveGameOverUI()
    {
        gameOverCanvas.transform.DOKill();
        gameOverCanvas.transform.DOScaleY(0f, 0.5f);
    }

    private void ActiveGameOverUI()
    {
        gameOverCanvas.transform.localScale = Vector3.one;
        gameOverCanvas.alpha = 0f;

        gameOverCanvas.DOKill();
        gameOverCanvas.DOFade(1f, 0.5f);
    }

    private void OnDestroy()
    {
        gameController.OnScoreChange -= UpdateUI;

        EventManager.StopListening(EventName.OnMiniGameStart, InactiveGameOverUI);
        EventManager.StopListening(EventName.OnMiniGameOver, ActiveGameOverUI);
    }
}
