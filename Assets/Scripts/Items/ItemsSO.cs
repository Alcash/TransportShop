using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Items/new items", order = 1)]
public class ItemsSO : ScriptableObject
{
    [SerializeField]
    private string nameId;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private GameObject prefab;

    public string NameId { get => nameId;}
    public Sprite Sprite { get => sprite;}
    public GameObject Prefab { get => prefab;}
}
