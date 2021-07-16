using System;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : BaseState
{
    private TimerManager timerManager;
    [SerializeField]
    private int timerMax = 60;

    private int scoreCount = 0;

    public int ScoreCount
    {
        get { return scoreCount; }
        set
        {
            scoreCount = value;
            OnChangeScore();
        }
    }

    public event Action OnChangeScore = delegate { };   

    public override void StartState()
    {
        base.StartState();
        scoreCount = 0;
        OnChangeScore();
        timerManager.PlayTimer(timerMax);
        timerManager.OnEndTime += EndTimeHandler;
    }

    private void EndTimeHandler()
    {
        //TODO вдруг нужно условие последнего шанса
        stateManager.StartState(typeof(EndTimerState));
    }

    public override void EndState()
    {
        base.EndState();
        timerManager.OnEndTime -= EndTimeHandler;
    }

    public override void InitState(IStateManager stateManager)
    {
        base.InitState(stateManager);
        timerManager = FindObjectOfType<TimerManager>();
    }
}
