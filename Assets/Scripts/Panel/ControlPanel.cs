using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public bool IsInControlMod = false;
    
    public void ChangePanelMod()
    {
        IsInControlMod = !IsInControlMod;
        Debug.Log("Touch the panel " + IsInControlMod);
    }
}
