using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoSingleton<ShootingManager>
{
    public delegate void GameOverDel();
    public event GameOverDel gameOverEvent;
    public void GameOver()
    {
        gameOverEvent.Invoke();
    }
}
