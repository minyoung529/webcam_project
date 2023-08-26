using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachersBackManager : MonoBehaviour
{
    [SerializeField] private TeachersBackPlayer player;
    [SerializeField] private TeachersBackTeacher teacher;

    [SerializeField] private CanvasGroup failMenu;

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

    public void StartGame()
    {
        Init();
    }
    
    public void StopGame()
    {
    }

    private void Init()
    {
        failMenu.alpha=0;
        snackCount = FullSnackCount;

        player.InitState();
        teacher.Init();
    }

    private void CheckPlayerEating(bool value)
    {
        if(teacher.TeacherState == TeachersBackTeacherState.FINDING)
        {
            if (player.PlayerState == TeachersBackPlayerState.EAT) Fail();
        }
        else
        {

        }
    }

    private void Fail()
    {
        player.ChangeState(TeachersBackPlayerState.FAIL);
        teacher.Scold();
        Invoke("FailUIUpdate", 1f);
    }
    private void FailUIUpdate()
    {
        teacher.StopPlay();

        failMenu.alpha = 1;
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
    FAIL,

    COUNT
}

public enum TeachersBackTeacherState
{
    None,
    
    FINDING,
    SCOLD,

    COUNT
}