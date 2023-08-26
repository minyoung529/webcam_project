using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachersBackPlayer : MonoBehaviour
{
    private TeachersBackPlayerState playerState = TeachersBackPlayerState.None;
    public TeachersBackPlayerState PlayerState => playerState;

    private Animator anim;

    private Dictionary<TeachersBackPlayerState, Action> stateAction = new Dictionary<TeachersBackPlayerState, Action>();

    private void Awake()
    {
        anim = GetComponent<Animator>();

        InitState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(TeachersBackPlayerState.EAT);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            ChangeState(TeachersBackPlayerState.None);
        }
    }

    public void ChangeState(TeachersBackPlayerState changeState)
    {
        playerState = changeState;
        if(stateAction.ContainsKey(changeState))
        {
            stateAction[changeState].Invoke();
        }
    }
    
    #region Set State
    private void InitState()
    {
        stateAction.Add(TeachersBackPlayerState.None, IdleState);
        stateAction.Add(TeachersBackPlayerState.EAT, EatState);
        stateAction.Add(TeachersBackPlayerState.LEFT, LeftState);
        stateAction.Add(TeachersBackPlayerState.RIGHT, RightState);
        stateAction.Add(TeachersBackPlayerState.DOWN, DownState);
    }
    #endregion

    #region State Function
    private void IdleState()
    {

    }
    
    private void EatState()
    {
        EventManager<bool>.TriggerEvent(EventName.OnStudentEating, true);
    }
    
    private void LeftState()
    {

    }
   
    private void RightState()
    {

    }
    
    private void DownState()
    {

    }
    #endregion
}
