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
       
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Init();
    }

    private void Init()
    {
        failMenu.alpha = 0.0f;
        failMenu.gameObject.SetActive(false);
        snackCount = FullSnackCount;

        EventManager<bool>.StartListening(EventName.OnTeacherFinding, CheckPlayerEating);
        EventManager<bool>.StartListening(EventName.OnStudentEating, CheckPlayerEating);

        player.Init();
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

        StartCoroutine(StopGame());
    }


    private IEnumerator StopGame()
    {
        yield return new WaitForSeconds(3f);
        teacher.StopPlay();
        player.StopPlay();

        failMenu.alpha= 1.0f;
        failMenu.gameObject.SetActive(true);
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