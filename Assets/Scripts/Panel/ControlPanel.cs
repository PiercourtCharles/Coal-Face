using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Transform CamPosPanel;
    public bool IsInControlMod = false;
    
    public void ChangePanelMod()
    {
        IsInControlMod = !IsInControlMod;
        Debug.Log("Touch the panel " + IsInControlMod);

        if (IsInControlMod)
        {
            GameManager.Instance.Player.Movement.IsParalysed = true;
            GameManager.Instance.Player.Look.IsOnHead = false;
            GameManager.Instance.Player.Look.CamTargetPos = CamPosPanel;
        }
        else
        {
            GameManager.Instance.Player.Movement.IsParalysed = false;
            GameManager.Instance.Player.Look.IsOnHead = true;
            GameManager.Instance.Player.Look.CamTargetPos = null;
        }
    }
}
