using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PengUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[] hearts;

    [SerializeField]
    private CanvasGroup gameOverCanvas;

    private int index = 0;

    private void Awake()
    {
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionFailed, Fail);
        EventManager.StartListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StartListening(EventName.OnMiniGameStart, Restart);
    }

    private void Fail(Penguin penguin)
    {
        if (index < 0 || index >= hearts.Length)
            return;

        hearts[index++].gameObject.SetActive(false);
    }

    private void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        gameOverCanvas.DOFade(1f, 1f);
    }

    private void Restart()
    {
        if (gameOverCanvas.gameObject.activeSelf)
        {
            gameOverCanvas.transform.DOScaleY(0f, 0.3f).OnComplete(() => gameOverCanvas.gameObject.SetActive(false));
        }

        foreach (GameObject heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionFailed, Fail);
        EventManager.StopListening(EventName.OnMiniGameOver, GameOver);
        EventManager.StopListening(EventName.OnMiniGameStart, Restart);
    }
}
