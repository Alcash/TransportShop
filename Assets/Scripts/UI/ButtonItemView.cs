using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ButtonItemView : AbstractTransportManagerView
{
    [SerializeField]
    private Button button;

    protected override void UpdateView()
    {
        button.interactable = transportManager.CanAddItem;
    }
}
