using UnityEngine;

public class PanelInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var panelMod = script.transform.GetComponent<ControlPanel>();

        if (panelMod != null)
            panelMod.ChangePanelMod();
    }
}
