using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] TaskManager Manager;
    public bool IsComplete { get; private set; } = false;

    public void TaskComplete()
    {
        if (IsComplete)
            return;

        Debug.Log("Check");
        IsComplete = true;
        Manager.CheckTask();
    }
}
