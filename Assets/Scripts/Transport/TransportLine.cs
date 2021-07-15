using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportLine : MonoBehaviour
{
    private List<TransportLineElement> transportLineElements = new List<TransportLineElement>();

    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;
    [SerializeField]
    private float moveTime = 5;
    private float moveSpeed;

    [SerializeField]
    private TrashBinController trashBin;

    public void AddElements(TransportLineElement transportLineElement)
    {
        transportLineElement.transform.SetParent(transform);
        transportLineElements.Add(transportLineElement);

        transportLineElement.transform.position = startPoint.position;

        var move = transportLineElement.gameObject.AddComponent<MoveToController>();
        move.SetTarget(endPoint, moveTime, delegate { EndMove(transportLineElement); });
    }

    public void RemoveElemets(TransportLineElement transportLineElement)
    {
        transportLineElements.Remove(transportLineElement);
    }

    private void Awake()
    {
        moveSpeed = (startPoint.position - endPoint.position).magnitude / moveTime;
    }   

    public void ClearLine()
    {
        foreach (var item in transportLineElements.ToArray())
        {
            Destroy(item.gameObject);
        }
        transportLineElements.Clear();
    }

    private void EndMove(TransportLineElement item)
    {
        transportLineElements.Remove(item);
        trashBin.MoveToTrash(item);

    }

   
      
}
