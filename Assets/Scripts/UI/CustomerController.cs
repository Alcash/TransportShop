using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MoveToController))]
public class CustomerController : MonoBehaviour, IUpdatable, IScoreSendable
{
    public event Action<CustomerController> OnReturned = delegate { };
    public event Action<int> OnChangeScore = delegate { };
    public event Action OnChangeWaitStatus = delegate { };


    private Vector3 homePoint = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;

    [SerializeField]
    private float moveTime = 5;

    private MoveToController moveController;
    private TransportLine transportLine;

    [SerializeField]
    private Transform rootHand = null;
    private float closeEnoughDistance = 0.1f;

    public ItemPlace ItemPlace { get; private set; }
    private WishGenerator wishGenerator;
    private int minElementtCount = 1;
    private int maxElementCount = 3;    
    private int scoreAmount =1;
    private int penaltyAmount = -1;

    private float statusWait = 1;

    public float StatusWait
    {
        get
        {
            return statusWait;
        }
        private set
        {
            statusWait = value;
            OnChangeWaitStatus();
        }
    }

    private float currentWaitTime;

    private float currentTime;

    [SerializeField]
    private float maxWait = 10;
    [SerializeField]
    private float minWait = 5;   

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
        moveController.SetTarget(targetPoint, moveTime, OnEndMoveHandler);
        moveController.enabled = true;
        moveController.TurnTo(targetPoint);       
        ItemPlace = wishGenerator.GetShopBasket(minElementtCount, maxElementCount);
        StatusWait = 1;
        foreach (Transform item in rootHand)
        {
            Destroy(item.gameObject);

        }       
    }

    private void OnEndMoveHandler()
    {
        currentWaitTime = Random.Range(minWait, maxWait);
        currentTime = currentWaitTime;       
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
        
        StatusWait = currentTime / currentWaitTime;
        currentTime -= delta;

        if(currentTime <= 0)
        {
            LeaveLine(AtHome);
            OnChangeScore(penaltyAmount);
            return;
        }

        foreach (var item in transportLine.TransportLineElements.ToArray())
        {
            if (Mathf.Abs(transform.InverseTransformPoint(item.transform.position).x) < closeEnoughDistance)
            {              
                if (item.Compare(ItemPlace))
                {                  
                    transportLine.RemoveElemets(item);
                    item.transform.parent = rootHand;
                    item.transform.localPosition = Vector3.zero;
                    LeaveLine(AtHome);                
                    OnChangeScore(scoreAmount);
                    break;
                   
                }
            }
        }
    }    

    private void LeaveLine(Action callback)
    {       
        moveController.SetTarget(homePoint, moveTime, callback);
        moveController.enabled = true;
        moveController.TurnTo(homePoint);
        UpdateManager.RemoveUpdateObject(this);
    }
}
