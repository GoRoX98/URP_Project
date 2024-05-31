using UnityEngine;

public static class GameTaskFabric
{
    public static GameTask CreateGameTask(TaskData taskData, Transform player)
    {
        ComeToTask task = new(taskData as ComeToTaskData, player);
        return task;
    }
}
