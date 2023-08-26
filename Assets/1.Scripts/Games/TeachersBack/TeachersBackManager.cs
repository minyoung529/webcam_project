using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachersBackManager : MonoBehaviour
{
    [SerializeField] private TeachersBackPlayer player;
    [SerializeField] private TeachersBackTeacher teacher;

    private int snackCount = 0;
    private const int FullSnackCount = 10;

    private void Awake()
    {
        EventManager<bool>.StartListening(EventName.OnTeacherFinding, CheckPlayerEating);
        EventManager<bool>.StartListening(EventName.OnStudentEating, CheckPlayerEating);
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        Init();
    }

    private void Init()
    {
        snackCount = FullSnackCount;
    }

    private void CheckPlayerEating(bool value)
    {
        if(teacher.TeacherState == TeachersBackTeacherState.FINDING && player.PlayerState == TeachersBackPlayerState.EAT)
        {
            Fail();
        }
        else
        {

        }
    }

    private void Fail()
    {
        Debug.Log("Fail");
    }

    private void Success()
    {

    }

    private void OnDestroy()
    {
        EventManager<bool>.StopListening(EventName.OnTeacherFinding, CheckPlayerEating);
        EventManager<bool>.StopListening(EventName.OnStudentEating, CheckPlayerEating);
    }
}

public enum TeachersBackPlayerState
{
    None,

    EAT,
    LEFT,
    RIGHT,
    DOWN,

    COUNT
}

public enum TeachersBackTeacherState
{
    None,
    
    FINDING,

    COUNT
}