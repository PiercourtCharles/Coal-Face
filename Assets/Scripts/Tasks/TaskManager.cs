using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] TaskDisplay _display;
    [SerializeField] Task[] Tasks;

    int index = 0;

    void Start()
    {
        index = 0;
        Tasks[index].Begin();
        _display.UpdateText(Tasks[index].TaskText);
    }

    public bool CheckOrder(Task task)
    {
        if (task != Tasks[index])
            return false;
        else
            return true;
    }

    public bool CheckTask()
    {
        if (Tasks[index].IsComplete)
        {
            Tasks[index].End();
            index++;

            if (index >= Tasks.Length)
            {
                //index--;
                _display.gameObject.SetActive(false);
                //Debug.Log("No more tasks");
            }
            else
            {
                Tasks[index].Begin();
                _display.UpdateText(Tasks[index].TaskText);
            }

            //Debug.Log("Completed");
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
            //Debug.Log($"Task {i} Complete");
        }
        //Debug.Log("All completed");
        return true;
    }
}
