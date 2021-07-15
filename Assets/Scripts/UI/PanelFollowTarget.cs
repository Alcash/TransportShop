using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFollowTarget : MonoBehaviour, IUpdatable
{
    [SerializeField]
    private Transform objectTransform;
    [SerializeField]
    private Transform targetTransform;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        UpdateManager.AddUpdateObject(this);
    }

    private void OnDisable()
    {
        UpdateManager.RemoveUpdateObject(this);
    }

    void IUpdatable.DoUpdate(float delta)
    {
        objectTransform.position = mainCamera.WorldToScreenPoint(targetTransform.position);
    }
}
