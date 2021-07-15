using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManager
{
    void StartState(Type type);
    BaseState CurrentState { get; }
}
