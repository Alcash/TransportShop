using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManger : MonoBehaviour
{
    //NOTE ������� �� �� ���� � �� �����?
    private Dictionary<Type, BaseState> states;

    private BaseState currentState;

    private void Start()
    {

        states = new Dictionary<Type, BaseState>();
        var tempStates = transform.GetComponentsInChildren<BaseState>();

        for (int i = 0; i < tempStates.Length; i++)
        {            
            states.Add(tempStates[i].GetType(), tempStates[i]);
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
