using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TeachersBackTeacher : MonoBehaviour
{
    private TeachersBackTeacherState teacherState;
    public TeachersBackTeacherState TeacherState => teacherState;

    private float maxFindingWaitTime = 10f;
    private float maxIdleWaitTime = 10f;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        teacherState = TeachersBackTeacherState.None;
        IdleState();
    }

    #region State

    private IEnumerator RandomState()
    {
        float randomWaitTime = Random.RandomRange(1f, maxFindingWaitTime);
        yield return new WaitForSeconds(randomWaitTime);
        Finding();
    }

    private void FindState()
    {
        StartCoroutine(Finding());
    }

    private IEnumerator Finding()
    {
        float randomWaitTime = Random.RandomRange(1f, maxIdleWaitTime);
        EventManager<bool>.TriggerEvent(EventName.OnTeacherFinding, true);
        yield return new WaitForSeconds(randomWaitTime);
        IdleState();
    }

    private void IdleState()
    {
        StartCoroutine(RandomState());
    }
    #endregion
}
