using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stateObjects;

    [SerializeField]
    private MonoBehaviour[] stateComponents;

    public void StartState()
    {
        EnableGO(stateObjects, true);
        EnableComponents(stateComponents, true);
    }

    public void EndState()
    {
        EnableGO(stateObjects, false);
        EnableComponents(stateComponents, false);
    }

    protected void EnableGO(GameObject[] objects, bool balue)
    {
        foreach (var item in objects)
        {
            item.SetActive(balue);
        }
    }

    protected void EnableComponents(MonoBehaviour[] components, bool balue)
    {
        foreach (var item in components)
        {
            item.enabled = balue;
        }
    }
}
