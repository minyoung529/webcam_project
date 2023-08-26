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
        teacherState = TeachersBackTeacherState.None;
        IdleState();
    }
    public void StopPlay()
    {
        StopAllCoroutines();
        teacherState = TeachersBackTeacherState.COUNT;
    }

    #region State

    public void Scold()
    {
        teacherState = TeachersBackTeacherState.SCOLD;
        transform.rotation = Quaternion.Euler(0, 145f, 0);
        anim.SetTrigger("Scold");
    }

    private IEnumerator RandomState()
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
        anim.SetBool("Finding", false);
        IdleState();
    }

    private void IdleState()
    {
        teacherState = TeachersBackTeacherState.None;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(RandomState());
    }
    #endregion
}
