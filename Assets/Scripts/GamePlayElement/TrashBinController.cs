using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinController : MonoBehaviour, IScoreSendable
{

    [SerializeField]
    private float waitToDestoyTime = 0.3f;
    private float moveSpeed;
    private GameObject trash;

    private Coroutine coroutine;

    public event Action<int> OnChangeScore = delegate{};

    private int scorePenalty =-1; 

    public void MoveToTrash(TransportLineElement transportLineElement)
    {       
        trash = transportLineElement.gameObject;
        moveSpeed = (trash.transform.position - transform.position).magnitude / waitToDestoyTime;

        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(WaitToDestoy());
    }

    private IEnumerator WaitToDestoy()
    {
        yield return new WaitForSeconds(waitToDestoyTime);
        OnChangeScore(scorePenalty);
        Destroy(trash);
    }

    private void Update()
    {
        MoveElements(Time.deltaTime);
    }

    private void MoveElements(float delta)
    {
        if (trash)
        {
            trash.transform.position = Vector3.MoveTowards(trash.transform.position, transform.position, moveSpeed * delta);
           
        }
    }
}
