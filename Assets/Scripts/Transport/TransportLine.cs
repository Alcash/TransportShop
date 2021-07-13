using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportLine : MonoBehaviour
{
    private List<TransportLineElement> transportLineElements = new List<TransportLineElement>();

    

    public void AddElements(TransportLineElement transportLineElement)
    {

        transportLineElement.transform.SetParent(transform);
        transportLineElements.Add(transportLineElement);
    }

    public void RemoveElemets(TransportLineElement transportLineElement)
    {
        transportLineElements.Remove(transportLineElement);
    }

    private void Update()
    {
        MoveElements();
    }

    private void MoveElements()
    {

    }
}
