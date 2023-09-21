using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Penguin : MonoBehaviour
{
    [SerializeField]
    protected float speedMultiplier = 1f;

    private bool isRelease = false;

    [SerializeField]
    private Transform bubble;

    [SerializeField]
    private Transform head;

    private bool isLeft;

    [SerializeField]
    private UnityEvent bubblePopEvent;

    #region Property
    public Transform Head => head;
    public Action<Penguin> ReleaseAction { get; set; }
    public bool IsLeft
    {
        get { return isLeft; }
        set { isLeft = value; }
    }
    #endregion

    #region CONST
    private const float MAX_DIFF_FROM_PREV = 0.6f;
    private const float LIMIT_X = 7.2f;
    #endregion

    private bool canMove = false;

    private void Awake()
    {
        // 재시작일 경우
        EventManager.StartListening(EventName.OnMiniGameStart, Release);
    }

    private void Update()
    {
        if (canMove)
        {
            Activate();
        }
    }

    protected virtual void Activate()
    {
        float speed = Time.deltaTime * speedMultiplier * PenguinGameController.Speed;

        if (isLeft)
        {
            transform.Translate(Vector3.right * speed, Space.World);

            if (transform.position.x >= LIMIT_X)
                IsLeft = false;
        }
        else
        {
            transform.Translate(Vector3.left * speed, Space.World);

            if (transform.position.x <= -LIMIT_X)
                IsLeft = true;
        }
    }

    private void OnBubblePop(Penguin prevPenguin)
    {
        float prevX = prevPenguin.transform.position.x;
        float curX = transform.position.x;
        canMove = false;
        bubblePopEvent?.Invoke();

        if (Mathf.Abs(prevX - curX) < MAX_DIFF_FROM_PREV)    // 성공
        {
            SuccessAnimation(prevPenguin);
        }
        else // 실패
        {
            FailAnimation();
        }

        bubble.gameObject.SetActive(false);

        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionStarted, OnBubblePop);
    }

    private void SuccessAnimation(Penguin prevPenguin)
    {
        Sequence seq = DOTween.Sequence();
        float y = prevPenguin.Head.position.y + 1f;
        Vector3 destination = prevPenguin.Head.position;
        destination.x = transform.position.x;

        Vector3 estimatedHead = destination + Vector3.up * head.localPosition.y;
        Vector3 upVec = (estimatedHead - prevPenguin.Head.position).normalized;

        seq.Append(transform.DOMoveY(y, 0.5f));
        seq.Append(transform.DOMove(destination, 0.2f));
        seq.AppendCallback(() =>
        {
            transform.up = upVec;
            Vector3 euler = transform.eulerAngles;
            euler.y = -179f;
            euler.z *= -1f;
            transform.eulerAngles = euler;
        });

        seq.AppendCallback(() => EventManager<Penguin>.TriggerEvent(EventName.OnMiniGameActionSuccessed, this));
        seq.AppendCallback(() => EventManager<Penguin>.TriggerEvent(EventName.OnMiniGameActionEnded, this));
    }

    private void FailAnimation()
    {
        Sequence seq = DOTween.Sequence();
        float y = transform.localPosition.y + 3f;

        seq.Append(transform.DOLocalMoveY(y, 0.5f));
        seq.Append(transform.DOLocalMoveY(y - 8f, 0.5f));

        seq.AppendCallback(() => EventManager<Penguin>.TriggerEvent(EventName.OnMiniGameActionFailed, this));
        seq.AppendCallback(() => EventManager<Penguin>.TriggerEvent(EventName.OnMiniGameActionEnded, this));
        seq.AppendCallback(() => ReleaseAction.Invoke(this));
    }

    #region POOL
    private void Release()
    {
        if (!isRelease)
        {
            isRelease = true;
            ReleaseAction?.Invoke(this);
        }
    }

    public void OnCreated()
    {

    }

    public void OnGet()
    {
        isRelease = false;
        gameObject.SetActive(true);
        bubble.gameObject.SetActive(true);
        canMove = true;

        transform.eulerAngles = Vector3.up * -179f;

        EventManager<Penguin>.StartListening(EventName.OnMiniGameActionStarted, OnBubblePop);
    }

    public void OnRelease()
    {
        isRelease = true;
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionStarted, OnBubblePop);
        gameObject.SetActive(false);
    }

    public void OnDestroyed()
    {
        EventManager.StopListening(EventName.OnMiniGameStart, Release);
        EventManager<Penguin>.StopListening(EventName.OnMiniGameActionStarted, OnBubblePop);
    }
    #endregion
}
 