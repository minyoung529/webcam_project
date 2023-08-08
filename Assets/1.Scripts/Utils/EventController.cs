using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController
{
    private Dictionary<int, Action<object>> actionDict = new();
    private Dictionary<int, Action> actionDictNonParameter = new();

    #region StartListen
    public void StartListening(string eventName, Action<object> action)
    {
        StartListening(eventName.GetHashCode(), action);
    }

    public void StartListening(string eventName, Action action)
    {
        StartListening(eventName.GetHashCode(), action);
    }

    public void StartListening(int eventNum, Action<object> action)
    {
        if (!actionDict.ContainsKey(eventNum))
            actionDict.Add(eventNum, action);
        else
            actionDict[eventNum] += action;
    }

    public void StartListening(int eventNum, Action action)
    {
        if (!actionDictNonParameter.ContainsKey(eventNum))
            actionDictNonParameter.Add(eventNum, action);
        else
            actionDictNonParameter[eventNum] += action;
    }
    #endregion

    #region StopListen
    public void StopListening(string eventName, Action<object> action)
    {
        StopListening(eventName.GetHashCode(), action);
    }

    public void StopListening(string eventName, Action action)
    {
        StopListening(eventName.GetHashCode(), action);
    }

    public void StopListening(int eventNum, Action<object> action)
    {
        actionDict[eventNum] -= action;
    }

    public void StopListening(int eventNum, Action action)
    {
        actionDictNonParameter[eventNum] -= action;
    }

    #endregion StopListen

    #region Trigger
    public void Trigger(int eventNum, object obj)
    {
        if (actionDict.ContainsKey(eventNum))
            actionDict[eventNum]?.Invoke(obj);

        if (actionDictNonParameter.ContainsKey(eventNum))
            actionDictNonParameter[eventNum]?.Invoke();
    }

    public void Trigger(int eventNum)
    {
        if (actionDict.ContainsKey(eventNum))
            actionDict[eventNum]?.Invoke(null);

        if (actionDictNonParameter.ContainsKey(eventNum))
            actionDictNonParameter[eventNum]?.Invoke();
    }

    public void Trigger(string eventName, object obj)
    {
        Trigger(eventName.GetHashCode(), obj);
    }

    public void Trigger(string eventName)
    {
        Trigger(eventName.GetHashCode());
    }
    #endregion
}