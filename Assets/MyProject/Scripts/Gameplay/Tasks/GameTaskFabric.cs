using UnityEngine;

public static class GameTaskFabric
{
    public static GameTask CreateGameTask(TaskData taskData, Transform player)
    {
        if (taskData is ComeToTaskData)
        {
            ComeToTask task = new(taskData as ComeToTaskData, player);
            return task;
        }
        else
        {
            MoveTask task = new(taskData as MoveTaskData);
            return task;
        }
    }
}
