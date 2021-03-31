using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBroker
{
    public static event Action<int> InitiateGame;

    public static void CallInitiateGame(int count)
    {
        InitiateGame?.Invoke(count);
    }


    public static event Action<Material> SetGunColor;

    public static void CallSetGunColor(Material material)
    {
        SetGunColor?.Invoke(material);
    }


    public static event Action<Enemy> DeleteEnemy;

    public static void CallDeleteEnemy(Enemy enemy)
    {
        DeleteEnemy?.Invoke(enemy);
    }

    public static event Action ResumeGame;

    public static void CallResumeGame()
    {
        ResumeGame?.Invoke();
    }

    public static event Action PauseGame;

    public static void CallPauseGame()
    {
        PauseGame?.Invoke();
    }
}
