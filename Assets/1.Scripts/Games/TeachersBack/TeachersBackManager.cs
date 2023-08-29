using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class TeachersBackManager : MonoBehaviour
{
    [SerializeField] private TeachersBackPlayer player;
    [SerializeField] private TeachersBackTeacher teacher;

    [SerializeField] private CanvasGroup failMenu;


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

        EventManager<bool>.StartListening(EventName.OnTeacherFinding, CheckPlayerEating);
        EventManager<bool>.StartListening(EventName.OnStudentEating, CheckPlayerEating);
        EventManager.StartListening(EventName.OnMiniGameOver, Fail);

        player.Init();
        teacher.Init();
    }

    private void CheckPlayerEating(bool value)
    {
        if(teacher.TeacherState == TeachersBackTeacherState.FINDING)
        {
            if (player.PlayerState == TeachersBackPlayerState.EAT) Fail();
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

    private void OnDestroy()
    {
        EventManager<bool>.StopListening(EventName.OnTeacherFinding, CheckPlayerEating);
        EventManager<bool>.StopListening(EventName.OnStudentEating, CheckPlayerEating);
        EventManager.StopListening(EventName.OnMiniGameOver, Fail);
    }
}

public enum TeachersBackPlayerState
{
    None,

    EAT,
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