using System;
public abstract class GameTask 
{
    protected TaskData _data;
    protected TaskStatus _status;

    public TaskStatus Status => _status;
    public string Name => _data.Name;
    public string Description => _data.Description;
    public int Score => _data.Score;

    public GameTask(TaskData data)
    {
        _data = data;
    }

    public static event Action<GameTask> TaskComplete;

    public abstract void CheckComplete();
    protected virtual void Complete()
    {
        TaskComplete?.Invoke(this);
        _status = TaskStatus.Complete;
    }
}

public enum TaskStatus
{
    InProgress,
    Complete,
    Failed
}