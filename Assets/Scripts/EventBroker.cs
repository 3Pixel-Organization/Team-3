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
}
