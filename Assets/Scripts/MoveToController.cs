using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToController : MonoBehaviour, IUpdatable
{
    public event Action OnEndMove = delegate { };
    private Action callBackAction = null;
    private Vector3 targetPoint = Vector3.zero;    
    private float moveTime = 5;
    private float moveSpeed;

    private void OnEnable()
    {
        UpdateManager.AddUpdateObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.RemoveUpdateObject(this);
    }

    public void SetTarget(Transform point, float moveTime, Action callBack = null)
    {
        targetPoint = point.position;
        moveSpeed = (transform.position - point.position).magnitude / moveTime;
        callBackAction = callBack;
    }

    void IUpdatable.DoUpdate(float delta)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * delta);
        if (transform.position == targetPoint)
        {
            OnEndMove();
            callBackAction();
            enabled = false;
        }
    }

   
}
