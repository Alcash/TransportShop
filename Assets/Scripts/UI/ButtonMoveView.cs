using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ButtonMoveView : AbstractTransportManagerView
{
    [SerializeField]
    private Button button;

    protected override void UpdateView()
    {
        button.interactable = transportManager.CanMoveToLine;
    }
}
