using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public PanelInteraction PanelInt = new PanelInteraction();
    public Transform CamPosPanel;
    public bool IsInControlMod = false;
    
    public void ChangePanelMod()
    {
        IsInControlMod = !IsInControlMod;
        Debug.Log("Touch the panel " + IsInControlMod);
        var player = PlayerComponentManager.Instance;

        if (IsInControlMod)
        {
            player.Movement.IsParalysed = true;
            player.Look.IsOnHead = false;
            player.Look.CamTargetPos = CamPosPanel;
        }
        else
        {
            player.Movement.IsParalysed = false;
            player.Look.IsOnHead = true;
            player.Look.CamTargetPos = null;
        }
    }
}
