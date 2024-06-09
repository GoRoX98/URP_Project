using UnityEngine;

public class MoveTask : GameTask
{
    private MoveTaskData _taskData;

    public MoveTask(MoveTaskData data) : base(data)
    {
        _taskData = data;
    }

    public override void CheckComplete()
    {
        if (Vector3.Distance(_taskData.Target.position, _taskData.TargetEndPoint.position) <= _taskData.CheckDistance)
            Complete();
    }
}
