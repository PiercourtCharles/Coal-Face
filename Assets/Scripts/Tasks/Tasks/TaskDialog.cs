using UnityEngine;

public class TaskDialog : Task
{
    public string ID { get; private set; }
    [SerializeField] Dialog _dialog;

    public override void Begin()
    {
        ID = _dialog.ID;
        GameManager.Instance.Dialogue.UpdateDialogue(_dialog);
    }
}
