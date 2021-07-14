using System;
using System.Collections.Generic;
using UnityEngine;

public class TransportManager : MonoBehaviour
{
    public event Action OnChangeStatus;

    [SerializeField]
    private Transform workPlaceRoot;
    [SerializeField]
    private TransportLineElement lineElementPrefab;
    private TransportLineElement workLineElement;

    [SerializeField]
    private TransportLine transportLine;

    private ItemPlace itemPlace;

    [SerializeField]
    private float waitToNewElement = 0.5f;

    public bool CanMoveToLine { get; protected set; } = true;
    public bool CanAddItem { get; protected set; } = true;
    private int itemOnElementCount = 0;
    [SerializeField]
    private int maxItemOnElement = 3;   

    public void AddItem(ItemsSO itemsSO)
    {
        if (workLineElement != null && CanAddItem)
        {
            itemPlace.AddItem(itemsSO);
            itemOnElementCount++;
            CanAddItem = itemOnElementCount < maxItemOnElement;
            OnChangeStatus();
        }
    }

    private void CreateTransportElement()
    {
       
        itemPlace = new ItemPlace();
        workLineElement = Instantiate(lineElementPrefab, workPlaceRoot);
        workLineElement.SetItem(itemPlace);
        itemOnElementCount = 0;
        CanAddItem = true;
        OnChangeStatus();
    }

    private void Start()
    {
        CreateTransportElement();
    }

    public void MoveToLine()
    {
        if (workLineElement)
        {
            transportLine.AddElements(workLineElement);
            workLineElement = null;
            CanMoveToLine = false;
            CanAddItem = false;
            Invoke(nameof(CreateTransportElement), 0.1f);
            Invoke(nameof(CanMove), waitToNewElement);
            OnChangeStatus();
        }
    }

    private void CanMove()
    {
        CanMoveToLine = true;
        OnChangeStatus();
    }

    
}
