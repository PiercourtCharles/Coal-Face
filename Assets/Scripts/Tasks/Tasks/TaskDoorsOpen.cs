public class TaskDoorsOpen : Task
{
    public Doors[] Doors;

    public override void Begin()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i].IsLocked = false;
        }
    }
}
