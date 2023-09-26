using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwitchColorUI : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Game Over")]
    [SerializeField] CanvasGroup gameOverPanel;

    public void UpdateScoreUI(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void GameOverUI()
    {
        gameOverPanel.alpha = 1f;
    }

    public void OffGameOverUI()
    {
        gameOverPanel.alpha = 0f;
    }
}
