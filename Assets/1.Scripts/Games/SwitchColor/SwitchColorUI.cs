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
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI gameOverScoreText;

    public void UpdateScoreUI(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void GameOverUI(int score)
    {
        gameOverScoreText.SetText(score.ToString());
        gameOverPanel.SetActive(true);
    }

    public void OffGameOverUI()
    {
        gameOverPanel.SetActive(false);
    }
}
