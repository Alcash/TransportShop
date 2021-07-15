using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToController))]
public class CustomerController : MonoBehaviour,IUpdatable,IScoreSendable
{
    public event Action<CustomerController> OnReturned = delegate { };
    public event Action<int> OnChangeScore = delegate { };

    private Vector3 homePoint = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;

    [SerializeField]
    private float moveTime = 5;

    private MoveToController moveController;
    private TransportLine transportLine;

    [SerializeField]
    private Transform rootHand = null;
    private float closeEnoughDistance = 0.1f;

    private ItemPlace itemPlace;
    private WishGenerator wishGenerator;
    private int minElementtCount = 1;
    private int maxElementCount = 3;

   

    private int scoreAmount =1;

    private void Awake()
    {
        homePoint = transform.position;
        moveController = GetComponent<MoveToController>();
        transportLine = FindObjectOfType<TransportLine>();

        wishGenerator = FindObjectOfType<WishGenerator>();
    }

    public void SetPoint(Transform point)
    {
        targetPoint = point.position;
        moveController.SetTarget(point, moveTime, OnEndMoveHandler);
        moveController.TurnTo(point.position);
        itemPlace = wishGenerator.GetShopBasket(minElementtCount, maxElementCount);

        foreach (var item in itemPlace.Items)
        {
            Debug.Log(item.Key.NameId);
        }
       
    }

    private void OnEndMoveHandler()
    {        
        UpdateManager.AddUpdateObject(this);
    }

    private void AtHome()
    {
        OnReturned(this);
    }  

    private void OnDisable()
    {
        UpdateManager.RemoveUpdateObject(this);      
    }

    void IUpdatable.DoUpdate(float delta)
    {        
        foreach (var item in transportLine.TransportLineElements.ToArray())
        {
            if (Mathf.Abs(transform.InverseTransformPoint(item.transform.position).x) < closeEnoughDistance)
            {              
                if (item.Compare(itemPlace))
                {                  
                    transportLine.RemoveElemets(item);
                    item.transform.parent = rootHand;
                    item.transform.localPosition = Vector3.zero;
                    moveController.SetTarget(homePoint, moveTime, OnEndMoveHandler);
                    moveController.enabled = true;
                    moveController.TurnTo(homePoint);

                    UpdateManager.RemoveUpdateObject(this);
                    OnChangeScore(scoreAmount);
                }
            }
        }

    }

    
}
