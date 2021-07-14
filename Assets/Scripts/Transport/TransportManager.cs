using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportManager : MonoBehaviour
{
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

    public bool CanMoveToLine { get; protected set; }

    public void AddItem(ItemsSO itemsSO)
    {
        if(workLineElement != null)
          itemPlace.AddItem(itemsSO);
    }

    private void CreateTransportElement()
    {
        itemPlace = new ItemPlace();
        workLineElement = Instantiate(lineElementPrefab, workPlaceRoot);
        workLineElement.SetItem(itemPlace);

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
            Invoke(nameof(CreateTransportElement), 0.1f);
            Invoke(nameof(CanMove), waitToNewElement);
        }
    }

    private void CanMove()
    {
        CanMoveToLine = true;
    }

    
}
