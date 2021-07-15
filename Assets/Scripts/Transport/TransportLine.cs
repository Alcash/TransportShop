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
    }

    public void RemoveElemets(TransportLineElement transportLineElement)
    {
        transportLineElements.Remove(transportLineElement);
    }

    private void Awake()
    {
        moveSpeed = (startPoint.position - endPoint.position).magnitude / moveTime;
    }

    private void Update()
    {
        MoveElements(Time.deltaTime);
    }

    public void ClearLine()
    {
        foreach (var item in transportLineElements.ToArray())
        {
            Destroy(item.gameObject);
        }
        transportLineElements.Clear();
    }

    private void MoveElements(float delta)
    {
        foreach (var item in transportLineElements.ToArray())
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, endPoint.position, moveSpeed* delta);

            if(item.transform.position == endPoint.position)
            {
                transportLineElements.Remove(item);
                trashBin.MoveToTrash(item);
            }
        }
    }
      
}
