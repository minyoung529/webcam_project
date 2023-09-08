using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeachersBackTeacher : MonoBehaviour
{
    private TeachersBackTeacherState teacherState;
    public TeachersBackTeacherState TeacherState => teacherState;

    private float maxFindingWaitTime = 10f;
    private float maxIdleWaitTime = 5f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        StartListening();
    }

    private void GameStart()
    {
        EventManager<TeachersBackPlayer>.StartListening(EventName.OnMiniGameActionFailed, Scold);
        
        StopAllCoroutines();
        anim.SetBool("Scold", false);
        transform.eulerAngles = new Vector3(0, 0, 0);
        IdleState();
    }
    private void GameOver()
    {
        EventManager<TeachersBackPlayer>.StopListening(EventName.OnMiniGameActionFailed, Scold);
     
        StopAllCoroutines();
        anim.SetBool("Finding", false);
        teacherState = TeachersBackTeacherState.None;
    }
    #region State

    private void Scold(TeachersBackPlayer player)
    {
        teacherState = TeachersBackTeacherState.SCOLD;

        transform.eulerAngles = new Vector3(0, 100, 0);
        anim.SetBool("Scold", true);

        StartCoroutine(GameOverDelay());
    }
    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f);
        EventManager.TriggerEvent(EventName.OnMiniGameOver);
    }
    
    private void IdleState()
    {
        teacherState = TeachersBackTeacherState.None;
        transform.eulerAngles = new Vector3(0, 0, 0);
        anim.SetBool("Finding", false);
        StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        float randomWaitTime = Random.Range(1f, maxFindingWaitTime);
        yield return new WaitForSeconds(randomWaitTime);
        FindState();
    }

    private void FindState()
    {
        StartCoroutine(Finding());
    }

    private IEnumerator Finding()
    {
        teacherState = TeachersBackTeacherState.FINDING;
        float randomWaitTime = Random.Range(1f, maxIdleWaitTime);

        anim.SetBool("Finding", true);
        yield return new WaitForSeconds(randomWaitTime);
        IdleState();
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
        StopListening();
    }
}
