using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PengViewDown : MonoBehaviour
{
    private void Awake()
    {
        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionSuccessed, GoDown);
    }

    private void GoDown(Penguin penguin)
    {
        float moveDistance = penguin.Head.position.y - penguin.transform.position.y;
        transform.DOMoveY(transform.position.y - moveDistance, 0.5f).OnComplete(() =>
        {
            PenguinGameController.ReadyInupt = true;
        });
    }

    private void OnDestroy()
    {
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionSuccessed, GoDown);
    }
}
