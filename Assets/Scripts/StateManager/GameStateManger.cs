using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManger : MonoBehaviour, IStateManager
{
    //NOTE —делать не по типу а по имени?
    private Dictionary<Type, BaseState> states;

    private BaseState currentState;

    public BaseState CurrentState => currentState;

    private void Start()
    {

        states = new Dictionary<Type, BaseState>();
        var tempStates = transform.GetComponentsInChildren<BaseState>();

        for (int i = 0; i < tempStates.Length; i++)
        {            
            states.Add(tempStates[i].GetType(), tempStates[i]);
            tempStates[i].InitState(this);
        }

        StartState(typeof(GamePlayState));
    }

    public void StartState(Type type)
    {
        if (currentState)
        {
            currentState.EndState();
        }
        currentState = states[type];
        currentState.StartState();
       
    }
}
