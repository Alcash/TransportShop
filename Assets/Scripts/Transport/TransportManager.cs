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

    public void AddItem(ItemsSO itemsSO)
    {
        itemPlace.AddItem(itemsSO);
    }

    private void CreateTransportElement()
    {
        itemPlace = new ItemPlace();
        workLineElement = Instantiate(lineElementPrefab, workPlaceRoot);
        workLineElement.SetItem(itemPlace);

    }
   
    public void MoveToLine()
    {       
        transportLine.AddElements(workLineElement);
    }
}
