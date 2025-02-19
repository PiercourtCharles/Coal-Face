using UnityEngine;

public class BedInteraction : IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands)
    {
        var bedMod = script.transform.GetComponent<Bed>();

        if (bedMod != null)
            bedMod.ChangePanelMod();
    }
}
