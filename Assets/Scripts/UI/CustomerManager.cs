using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerManager : MonoBehaviour, IUpdatable
{

    [SerializeField]
    private CustomerController prefabCustomer;

    [SerializeField]
    private Transform[] poointInLine;

    private float timerMin = 5;
    private float timerMax = 6;

    private CustomerController[] customers;

    private Dictionary<Transform, CustomerController> customerPoint = new Dictionary<Transform, CustomerController>();

    private float timer = 0;

    private void Awake()
    {
        SpawnCustomers();
    }

    private void OnEnable()
    {
        LetsGoCustomer();
        UpdateManager.AddUpdateObject(this);
    }

    private void OnDisable()
    {       
        foreach (var item in poointInLine)
        {
            if(customerPoint[item] != null)
                 customerPoint[item].gameObject.SetActive(false);
            customerPoint[item] = null;
        }

        UpdateManager.RemoveUpdateObject(this);

    }

    private void SpawnCustomers()
    {
        customers = new CustomerController[poointInLine.Length];
        for (int i = 0; i < customers.Length; i++)
        {
            customers[i] = Instantiate(prefabCustomer,transform.position, Quaternion.identity);
            customers[i].gameObject.SetActive(false);
            customers[i].OnReturned += CustomerReturned;            
        }      

        for (int i = 0; i < poointInLine.Length; i++)
        {
            customerPoint.Add(poointInLine[i], null);
        }
    }

    private void LetsGoCustomer()
    {
        if(enabled)
        {

            int indexCustomer = -1;
            for (int i = 0; i < customers.Length; i++)
            {
                if (customerPoint.ContainsValue(customers[i]) == false)
                {
                    indexCustomer = i;
                }
            }

            if(indexCustomer < 0)
            {
                return;
            }
            
            int rndLine = 0;
            //NOTE ищется рандомный элемент с пустым значение покупателя
            do
            {
                rndLine = Random.Range(0, customerPoint.Count);           
            }
            while (customerPoint.ElementAt(rndLine).Value != null) ;
           
            customerPoint[poointInLine[rndLine]] = customers[indexCustomer];
            customers[indexCustomer].transform.position = transform.position;
            customers[indexCustomer].SetPoint(poointInLine[rndLine]);
            customers[indexCustomer].gameObject.SetActive(true);
            timer = Random.Range(timerMin, timerMax);
        }
        
    }

    private void CustomerReturned(CustomerController customerController)
    {
        
        var lineKey = customerPoint.FirstOrDefault(x => x.Value == customerController).Key;
        customerPoint[lineKey] = null;       
        customerController.gameObject.SetActive(false);   
    }   

    private void OnDestroy()
    {
        foreach (var item in customers)
        {
            item.OnReturned -= CustomerReturned;
        }       
    }

    void IUpdatable.DoUpdate(float delta)
    {
        timer -= delta;
        if (timer <= 0)
        {
            LetsGoCustomer();
        }
    }
}
