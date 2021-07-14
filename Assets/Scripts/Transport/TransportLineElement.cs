using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportLineElement : MonoBehaviour
{
    private ItemPlace itemPlace = null;

    private List<GameObject> instViews = null;
    [SerializeField]
    private Transform root = null;
    [SerializeField]
    private float width = 0.7f;
    [SerializeField]
    private float length = 0.7f;
    [Header("≈сли не хочетс€ самому устанавливать параметры")]
    [SerializeField]
    private Transform view = null;
  

    private int maxInLine = 3;

    private void Awake()
    {
        if(view)
            width = view.localScale.x*2;
            length = view.localScale.z*2;
    }

    public void SetItem(ItemPlace newItemPlace)
    {
        itemPlace = newItemPlace;
        itemPlace.OnChanged += UpdateView;
        UpdateView();
    }

    private void UpdateView()
    {
      
        GameObject inst = null;
        int indexInst = 0;
        foreach (var item in itemPlace.Items)
        {
            for (int i = 0; i < item.Value; i++)
            {
                if (indexInst > instViews.Count)
                {
                    inst = Instantiate(item.Key.Prefab, PosById(indexInst), Quaternion.identity,root );
                    instViews.Add(inst);
                }
                indexInst++;
            }   
        }  
    }

    private Vector3 PosById(int index)
    {
        Vector3 result = new Vector3();

        result.x = -width / 2 + (width / maxInLine) * index;
        result.y = 0;
        result.z = -length / 2 + (length / maxInLine) * (1 + (int)(index/ maxInLine));
        return result;
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

    private void OnDestroy()
    {
        if(itemPlace != null)
         itemPlace.OnChanged -= UpdateView;
    }
}
