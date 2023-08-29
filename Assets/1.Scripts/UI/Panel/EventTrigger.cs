using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField]
    private EventName eventName;

    public void Trigger()
    {
        EventManager.TriggerEvent(eventName);
    }
}
