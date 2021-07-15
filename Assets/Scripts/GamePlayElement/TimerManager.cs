using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public event Action OnChangeTimer = delegate { };
    public event Action OnEndTime = delegate { };
    private long timerRaw;    
    public long TimerRaw => timerRaw;
    private const int delautTimer = 60;
    public bool TimerEnable = false;

    /// <summary>
    /// запустить таймер
    /// </summary>
    /// <param name="timerCount">Default is 60 seconds </param>
    public void PlayTimer(int timerMinuts = delautTimer)
    {        
        TimerEnable = true;
        timerRaw = timerMinuts * TimeSpan.TicksPerSecond;
        OnChangeTimer();
    }   

    private void Update()
    {
        if (TimerEnable)
        {
            timerRaw -= (long)(Time.deltaTime * TimeSpan.TicksPerSecond);
            OnChangeTimer();
            if (timerRaw <= 0)
            {
                TimerEnable = false;
                OnEndTime();
            }
        }
        
    }
}
