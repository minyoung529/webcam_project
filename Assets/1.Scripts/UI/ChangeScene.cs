using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Dinosaur,
    Penguin,
    SwitchColor,
    TeachersBack,
    ShootingMiniGameTestScene,
    StartScene,

    Count
}

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private SceneType sceneType;

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneType.ToString());
    }
}
