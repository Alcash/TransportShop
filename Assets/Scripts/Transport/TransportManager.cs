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

    private TransportLine transportLine;

    private ItemPlace itemPlace;

    private void CreateItemPlace()
    {
        itemPlace = new ItemPlace();

    }

    public void AddItem(ItemsSO itemsSO)
    {
        itemPlace.AddItem(itemsSO);
    }

    private void CreateTransportElement()
    {
        workLineElement = Instantiate(lineElementPrefab, workPlaceRoot);


    }
   
    public void MoveToLine()
    {
        workLineElement.SetItem(itemPlace);
        transportLine.AddElements(workLineElement);
    }
}
