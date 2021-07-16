using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTimerState : BaseState
{
    public override void InitState(IStateManager stateManager)
    {
        base.InitState(stateManager);
        EndState();
    }  

    public void RestartGame()
    {
        stateManager.StartState(typeof(GamePlayState));
    }
}
