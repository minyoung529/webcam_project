using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SwitchColorUI : MonoBehaviour
{
    [Header("In Game")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image nextColorImage;
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

    public void UpdateNextColor(Color color)
    {
        nextColorImage.color = color;
    }
}
