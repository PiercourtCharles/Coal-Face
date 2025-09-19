using UnityEngine;

public class Task : MonoBehaviour
{
    public string TaskText = "";
    public bool IsComplete { get; private set; } = false;

    public virtual void Begin()
    {
        //Debug.Log("Begin");
    }

    public virtual void End()
    {
        //Debug.Log("End");
    }

    public void TaskComplete()
    {
        if (IsComplete)
            return;

        if (!TaskManager.Instance.CheckOrder(this))
            return;

        //Debug.Log("Check");
        IsComplete = true;
        TaskManager.Instance.CheckTask();
    }
}