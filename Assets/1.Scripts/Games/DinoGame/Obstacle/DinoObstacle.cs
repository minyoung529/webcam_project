using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DinoObstacle : MonoBehaviour
{
    [SerializeField]
    protected float speedMultiplier;

    private float distance;
    private const float MAX_DISTANCE = 25f;

    public Action<DinoObstacle> ReleaseAction { get; set; }

    private bool isRelease = false;

    private void Awake()
    {
        // 재시작일 경우
        EventManager.StartListening(EventName.OnMiniGameStart, Release);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            Activate();

            if (distance > MAX_DISTANCE)
            {
                // 릴리즈
                Release();
            }
        }
    }

    protected virtual void Activate()
    {
        float speed = DinoGameController.Speed * speedMultiplier * Time.deltaTime;
        distance += speed;
        transform.Translate(Vector3.left * speed);
    }

    #region POOL
    private void Release()
    {
        if (!isRelease)
            ReleaseAction?.Invoke(this);
    }

    public void OnCreated()
    {

    }

    public void OnGet()
    {
        distance = 0f;
        isRelease = false;
        gameObject.SetActive(true);
    }

    public void OnRelease()
    {
        distance = 0f;
        isRelease = true;
        gameObject.SetActive(false);
    }

    public void OnDestroyed()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, Release);
    }
    #endregion
}
