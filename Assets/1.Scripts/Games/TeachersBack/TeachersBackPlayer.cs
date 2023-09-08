using System;
using System.Collections.Generic;
using UnityEngine;

public class TeachersBackPlayer : MonoBehaviour
{
    private Animator anim;
    private FaceController faceController;

    private TeachersBackPlayerState playerState = TeachersBackPlayerState.None;
    public TeachersBackPlayerState PlayerState => playerState;

    private Dictionary<TeachersBackPlayerState, Action> stateAction = new Dictionary<TeachersBackPlayerState, Action>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        faceController = FindObjectOfType<FaceController>();

        StartListening();
    }

    #region Game Flow

    private void GameStart()
    {
        EventManager<TeachersBackPlayer>.StartListening(EventName.OnMiniGameActionFailed, Fail);
        anim.SetBool("Eating", false);
        anim.SetBool("Fail", false);

        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, StartEat);
            faceController.Event.StartListening((int)FaceEvent.MouthClose, StopEat);
        }

        stateAction.Clear();
        stateAction.Add(TeachersBackPlayerState.None, IdleState);
        stateAction.Add(TeachersBackPlayerState.EAT, EatState);
        stateAction.Add(TeachersBackPlayerState.FAIL, FailState);

        ChangeState(TeachersBackPlayerState.None);
    }
    private void GameOver()
    {
        EventManager<TeachersBackPlayer>.StopListening(EventName.OnMiniGameActionFailed, Fail);
        anim.SetBool("Eating", false);

        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, StartEat);
            faceController.Event.StopListening((int)FaceEvent.MouthClose, StopEat);
        }

        ChangeState(TeachersBackPlayerState.FAIL);
    }

    #endregion

    #region State Function
    private void StartEat()
    {
        ChangeState(TeachersBackPlayerState.EAT);
    }
    private void StopEat()
    {
        ChangeState(TeachersBackPlayerState.None);
    }
    private void Fail(TeachersBackPlayer player)
    {
        ChangeState(TeachersBackPlayerState.FAIL);

        if (faceController)
        {
            faceController.Event.StopListening((int)FaceEvent.MouthOpen, StartEat);
            faceController.Event.StopListening((int)FaceEvent.MouthClose, StopEat);
        }
    }

    private void ChangeState(TeachersBackPlayerState changeState)
    {
        playerState = changeState;
        if (stateAction.ContainsKey(changeState))
        {
            stateAction[changeState].Invoke();
        }
    }

    private void IdleState()
    {
        anim.SetBool("Eating", false);
    }
    private void EatState()
    {
        anim.SetBool("Eating", true);
    }
    private void FailState()
    {
        anim.SetBool("Fail", true);
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
        if (faceController)
        {
            faceController.Event.StartListening((int)FaceEvent.MouthOpen, StartEat);
            faceController.Event.StartListening((int)FaceEvent.MouthClose, StopEat);
        }
        StopListening();
    }
}
