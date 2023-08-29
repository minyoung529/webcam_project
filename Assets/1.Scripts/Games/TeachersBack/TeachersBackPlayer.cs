using DG.Tweening;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeachersBackPlayer : MonoBehaviour
{
    private TeachersBackPlayerState playerState = TeachersBackPlayerState.None;
    public TeachersBackPlayerState PlayerState => playerState;

    [SerializeField] private Slider scoreSlider;
    [SerializeField] private TextMeshProUGUI countText;
    private Animator anim;

    private bool inputLock = true;
    private float FullSnackCount = 10;
    private int snackCount = 0;
    public int SnackCount { get { return snackCount; } set { snackCount = value; UpdateCountText(); } }
    private float addValue => (1f / FullSnackCount);

    private FaceController faceController;

    private Dictionary<TeachersBackPlayerState, Action> stateAction = new Dictionary<TeachersBackPlayerState, Action>();

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        faceController = GetComponent<FaceController>();
        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, StartEatState);
            faceController.Event.StartListening((int)FaceEvent.MouthClose, EndEatState);
        }
    }
    private void Update()
    {
        if (scoreSlider.IsActive())
        {
            SliderValue();
            CheckSlider();
        }
    }

    private void StartEatState()
    {
        if (inputLock) return;
                ChangeState(TeachersBackPlayerState.EAT);
    }
    private void EndEatState()
    {
        StopAllCoroutines();
        ChangeState(TeachersBackPlayerState.None);
    }


    #region Game Flow
    public void Init()
    {
        SnackCount = 0;
        scoreSlider.value = 0.5f;
        ActiveUI();

        stateAction.Clear();
        stateAction.Add(TeachersBackPlayerState.None, IdleState);
        stateAction.Add(TeachersBackPlayerState.EAT, EatState);
        stateAction.Add(TeachersBackPlayerState.FAIL, FailState);

        ChangeState(TeachersBackPlayerState.None);
    }

    public void StopPlay()
    {
        anim.SetBool("Eating", false);
    }

    private void UpgradeLevel()
    {
        Debug.Log("Upgrade");
        SnackCount++;
        FullSnackCount *= 2;
        scoreSlider.value = 0.5f;
    }

    #endregion

    #region State Function

    public void ChangeState(TeachersBackPlayerState changeState)
    {
        playerState = changeState;
        if (stateAction.ContainsKey(changeState))
        {
            stateAction[changeState].Invoke();
        }
    }

    private void IdleState()
    {
        inputLock = false;
        anim.SetBool("Eating", false);
    }

    private void EatState()
    {
        EventManager<bool>.TriggerEvent(EventName.OnStudentEating, true);
        anim.SetBool("Eating", true);
    }

    private void FailState()
    {
        InactiveUI();
        inputLock = true;
        anim.SetTrigger("Fail");
    }
    #endregion

    #region UI

    #region Slider

    private void SliderValue()
    {
        float value = scoreSlider.value - addValue;
        if (playerState == TeachersBackPlayerState.EAT)
        {
            value = scoreSlider.value + addValue;
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
            EventManager<TeachersBackPlayer>.TriggerEvent(EventName.OnMiniGameActionFailed, this);
            return;
        }
    }

    #endregion

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

    private void OnDestroy()
    {
        faceController = GetComponent<FaceController>();
        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, StartEatState);
            faceController.Event.StartListening((int)FaceEvent.MouthClose, EndEatState);
        }
    }
}
