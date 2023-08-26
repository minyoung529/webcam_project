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

    private bool inputLock = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (inputLock) return;
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
    public void InitState()
    {
        inputLock = false;

        stateAction.Clear();
        stateAction.Add(TeachersBackPlayerState.None, IdleState);
        stateAction.Add(TeachersBackPlayerState.EAT, EatState);
        stateAction.Add(TeachersBackPlayerState.LEFT, LeftState);
        stateAction.Add(TeachersBackPlayerState.RIGHT, RightState);
        stateAction.Add(TeachersBackPlayerState.DOWN, DownState);
        stateAction.Add(TeachersBackPlayerState.FAIL, FailState);
    }
    #endregion

    #region State Function
    private void IdleState()
    {
        anim.SetBool("Eating", false);
    }
    
    private void EatState()
    {
        EventManager<bool>.TriggerEvent(EventName.OnStudentEating, true);
        anim.SetBool("Eating", true);
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
    private void FailState()
    {
        inputLock = true;
        anim.SetTrigger("Fail");
    }
    #endregion
}
