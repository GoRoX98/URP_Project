using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private LevelData _data;

    private int _maxScore;
    private int _currentScore = 0;
    private Transform _player;
    private List<GameTask> _tasks = new List<GameTask>();

    public string Name => _data.Name;
    public int MaxScore => _maxScore;
    public int CurrentScore => _currentScore;


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _maxScore = _data.MaxScore;

        foreach (TaskData task in _data.Tasks) 
        { 
            _tasks.Add(GameTaskFabric.CreateGameTask(task, _player));
        }
    }

    private void OnEnable()
    {
        GameTask.TaskComplete += OnTaskComplete;
    }

    private void OnDisable()
    {
        GameTask.TaskComplete -= OnTaskComplete;
    }

    private void FixedUpdate()
    {
        _tasks.ForEach(task => { if (task.Status == TaskStatus.InProgress) task.CheckComplete(); } );
    }

    private void OnTaskComplete(GameTask task)
    {
        print($"Task {task.Name} complete");
        _currentScore += task.Score;
        TestJsonSave.Instance.Save();
    }

    private void OnExit()
    {
        GameManager.Instance.Save(this);
    }
}