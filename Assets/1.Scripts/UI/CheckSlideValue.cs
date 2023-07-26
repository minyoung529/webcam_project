using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckSlideValue : MonoBehaviour
{
    [SerializeField] private UnityEvent onMaxValueEvent;
    private Slider slider;

    private void Awake()
    {
        slider= GetComponent<Slider>();
    }

    public void CheckValue()
    {
        float value = slider.maxValue - slider.value;
        if (value <= 0.01)
        {
            onMaxValueEvent.Invoke();
        }
    }
}
