using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerWishView : MonoBehaviour
{
    [SerializeField]
    private CustomerController customerController;
    [SerializeField]
    private Transform rootView;

    [SerializeField]
    private Image template;

    private List<Image> images = new List<Image>();    

    private void OnEnable()
    {
        int index = 0;

        if (customerController.ItemPlace == null)
            return;

        foreach (var item in images)
        {
            item.gameObject.SetActive(false);
        }

        foreach (var item in customerController.ItemPlace.Items)
        {
            for (int i = 0; i < item.Value; i++)
            {
                if(images.Count <= index)
                {
                    images.Add(Instantiate(template, rootView));
                }
                images[index].sprite = item.Key.Sprite;
                images[index].gameObject.SetActive(true);
                index++;
            }
        }
    }
}
