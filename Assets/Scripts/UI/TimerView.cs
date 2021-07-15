using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    private TimerManager timerManager;

    [SerializeField]
    private Text text;

    private string timeFormat = @"mm\:ss";

    private void Awake()
    {
        timerManager = FindObjectOfType<TimerManager>();
    }

    private void ChangeTimerHandler()
    {
        if (text)
        {              
            text.text = TimeSpan.FromTicks(timerManager.TimerRaw).ToString(timeFormat);
        }
    }

    private void OnEnable()
    {
        if (timerManager)
            timerManager.OnChangeTimer += ChangeTimerHandler;
    }

    private void OnDisable()
    {
        if (timerManager)
            timerManager.OnChangeTimer -= ChangeTimerHandler;
    }
}
