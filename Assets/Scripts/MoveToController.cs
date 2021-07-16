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

    public void TurnTo(Vector3 point)
    {
        var direction = point - transform.position;

        direction= Vector3.ProjectOnPlane(direction, Vector3.up);

        transform.LookAt(direction);

        Debug.DrawLine(transform.position, transform.position + direction, Color.red,2);
    }

    public void SetTarget(Transform point, float moveTime, Action callBack = null)
    {
        SetTarget(point.position, moveTime, callBack);      
    }

    public void SetTarget(Vector3 point, float moveTime, Action callBack = null)
    {
        targetPoint = point;
        moveSpeed = (transform.position - point).magnitude / moveTime;
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
