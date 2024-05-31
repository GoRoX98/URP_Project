using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [SerializeField] private LevelData _data;

    private int _maxScore;
    private int _currentScore = 0;
    private Transform _player;
    private List<GameTask> _tasks = new List<GameTask>();

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
    }
}
