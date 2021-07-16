using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTransportManagerView : MonoBehaviour
{
    [SerializeField]
    protected TransportManager transportManager;

    protected void Awake()
    {
        if(transportManager == null)
        transportManager = FindObjectOfType<TransportManager>();
    }

    protected void OnEnable()
    {
        transportManager.OnChangeStatus += UpdateView;
        UpdateView();
    }

    protected void OnDisable()
    {
        transportManager.OnChangeStatus -= UpdateView;
    }

    protected abstract void UpdateView();
}
