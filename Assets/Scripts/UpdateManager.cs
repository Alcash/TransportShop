using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private List<IUpdatable> updatables = new List<IUpdatable>();
    private static UpdateManager updateManager;

    private static UpdateManager instance
    {
        get
        {
            if (updateManager == null)
            {
                InitUpdateManager();
            }
            return updateManager;
        }
    }

    private static void InitUpdateManager()
    {
        updateManager = FindObjectOfType<UpdateManager>();

        if (updateManager == null)
        {
            GameObject temp = new GameObject(nameof(UpdateManager), typeof(UpdateManager));
            updateManager = temp.GetComponent<UpdateManager>();
        }
    }

    public static void AddUpdateObject(IUpdatable updatable)
    {
        instance.AddUpdateObjectInList(updatable);
    }

    private void AddUpdateObjectInList(IUpdatable updatable)
    {
        updatables.Add(updatable);
    }

    public static void RemoveUpdateObject(IUpdatable updatable)
    {
        instance.RemoveUpdateObjectInList(updatable);
    }

    private void RemoveUpdateObjectInList(IUpdatable updatable)
    {
        updatables.Remove(updatable);
    }

    private void Update()
    {
        foreach (var item in updatables.ToArray())
        {
            if (item != null)
                item.DoUpdate(Time.deltaTime);
        }
    }
}
