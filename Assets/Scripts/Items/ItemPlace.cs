using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemPlace
{

    public event Action OnChanged = delegate { };

    private Dictionary<ItemsSO,int> items = new Dictionary<ItemsSO, int>();

    public void AddItem(ItemsSO itemsSO)
    {
        if (items.ContainsKey(itemsSO))
        {
            items[itemsSO]++;
        }
        else
        {
            items.Add(itemsSO, 1);
        }
        OnChanged();
    }

    public Dictionary<ItemsSO, int> Items => items;
}
