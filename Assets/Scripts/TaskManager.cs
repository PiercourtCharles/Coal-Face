using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task[] Tasks;

    int index = 0;

    void Start()
    {
        index = 0;
    }

    public bool CheckTask()
    {
        if (Tasks[index].IsComplete)
        {
            index++;

            if (index > Tasks.Length)
            {
                index--;
                Debug.Log("No more tasks");
            }

            Debug.Log("Completed");
            return true;
        }

        return false;
    }

    public bool CheckTasks()
    {
        for (int i = 0; i < Tasks.Length; i++)
        {
            if (!Tasks[i].IsComplete)
                return false;
            Debug.Log($"Task {i} Complete");
        }
        Debug.Log("All completed");
        return true;
    }
}
