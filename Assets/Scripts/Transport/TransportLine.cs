using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportLine : MonoBehaviour
{    
    public List<TransportLineElement> TransportLineElements { get; private set; } = new List<TransportLineElement>();

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
        TransportLineElements.Add(transportLineElement);

        transportLineElement.transform.position = startPoint.position;

        var move = transportLineElement.gameObject.AddComponent<MoveToController>();
        move.SetTarget(endPoint, moveTime, delegate { EndMove(transportLineElement); });
    }

    public void RemoveElemets(TransportLineElement transportLineElement)
    {
        TransportLineElements.Remove(transportLineElement);
        var move = transportLineElement.GetComponent<MoveToController>();
        Destroy(move);
    }

    private void Awake()
    {
        moveSpeed = (startPoint.position - endPoint.position).magnitude / moveTime;
    }   

    public void ClearLine()
    {
        foreach (var item in TransportLineElements.ToArray())
        {
            Destroy(item.gameObject);
        }
        TransportLineElements.Clear();
    }

    private void EndMove(TransportLineElement item)
    {
        TransportLineElements.Remove(item);
        trashBin.MoveToTrash(item);

    }

   
      
}
