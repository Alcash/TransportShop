using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishGenerator : MonoBehaviour
{
    [SerializeField]
    private List<ItemsSO> itemsSOs;

    public ItemPlace GetShopBasket(int min, int max)
    {
        ItemPlace itemPlace = new ItemPlace();

        int count = Random.Range(min, max);

        for (int i = 0; i < count; i++)
        {
            itemPlace.AddItem(itemsSOs[Random.Range(0, itemsSOs.Count)]);
        }     

        return itemPlace;
    }

}
