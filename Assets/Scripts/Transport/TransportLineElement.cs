using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportLineElement : MonoBehaviour
{
    private ItemPlace itemPlace;

    public void SetItem(ItemPlace newItemPlace)
    {
        itemPlace = newItemPlace;
        //TODO  заспавнить визуал объектов.
    }

    public bool Compare(ItemPlace compareItemPlace)
    {
        bool result = true;

        result &= compareItemPlace.Items.Count == itemPlace.Items.Count;        
        foreach (var item1 in compareItemPlace.Items)
        {
            result &= itemPlace.Items[item1.Key] == item1.Value;          
        }     

        return result;
    }
}
