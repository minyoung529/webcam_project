using System;
using System.Collections.Generic;


public class EventManager
{
    static private Dictionary<EventName, Action> eventDictionary = new Dictionary<EventName, Action>();

    public static void StartListening(EventName eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(EventName eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(EventName eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke();
        }
    }
}

public class EventManager<T>
{
    static private Dictionary<EventName, Action<T>> eventDictionary = new Dictionary<EventName, Action<T>>();

    public static void StartListening(EventName eventName, Action<T> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] += listener;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(EventName eventName, Action<T> listener)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] -= listener;
        }
    }

    public static void TriggerEvent(EventName eventName, T message)
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName]?.Invoke(message);
        }
    }
}

public class EventParam
{
    private Dictionary<int, object> eventParams = new Dictionary<int, object>();
    public object this[int i]
    {
        get => eventParams[i];
    }

    public object this[string str]
    {
        set => AddEventParam(str, value);
        get
        {
            int hash = str.GetHashCode();

            if (eventParams.ContainsKey(hash))
                return eventParams[hash];
            return null;
        }
    }

    public static EventParam Empty => null;

    public EventParam() { }

    public EventParam(params Param[] keyValues)
    {
        foreach (Param keyValue in keyValues)
            AddEventParam(keyValue.key, keyValue.value);
    }

    public void AddEventParam(string name, object obj)
    {
        eventParams[name.GetHashCode()] = obj;
    }

    public void RemoveEventParam(string name)
    {
        int hash = name.GetHashCode();

        if (eventParams.ContainsKey(hash))
        {
            eventParams.Remove(hash);
        }
    }

    public bool Contain(string name)
    {
        return eventParams.ContainsKey(name.GetHashCode());
    }
}

public struct Param
{
    public string key;
    public object value;

    public Param(string key, object value)
    {
        this.key = key;
        this.value = value;
    }
}

public enum EventName
{
    OnCharacterLoadComplete,
    OnVideoLoadComplete,
    OnSetTextureURL,
    Count
}