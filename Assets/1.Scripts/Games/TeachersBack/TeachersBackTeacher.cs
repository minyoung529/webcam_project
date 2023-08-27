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
    }

    public void Init()
    {
        StopAllCoroutines();
        anim.SetBool("Scold", false);
        transform.eulerAngles = new Vector3(0, 0, 0);
        IdleState();
    }
    public void StopPlay()
    {
        StopAllCoroutines();
        anim.SetBool("Finding", false);
        teacherState = TeachersBackTeacherState.None;
    }
    #region State

    public void Scold()
    {
        teacherState = TeachersBackTeacherState.SCOLD;

        transform.eulerAngles = new Vector3(0, 100, 0);
        anim.SetBool("Scold", true);
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
        EventManager<bool>.TriggerEvent(EventName.OnTeacherFinding, true);

        anim.SetBool("Finding", true);
        yield return new WaitForSeconds(randomWaitTime);
        IdleState();
    }

    #endregion
}
