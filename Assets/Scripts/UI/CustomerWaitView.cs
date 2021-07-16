using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWaitView : MonoBehaviour
{
    [SerializeField]
    private CustomerController customerController;
    [SerializeField]
    private Image image;  

    private void OnEnable()
    {
        customerController.OnChangeWaitStatus += UpdateView;
        UpdateView();
    }

    private void UpdateView()
    {
        image.fillAmount = customerController.StatusWait;
    }

    private void OnDisable()
    {
        customerController.OnChangeWaitStatus -= UpdateView;
    }
}
