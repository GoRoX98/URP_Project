using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private LevelData _data;
    [SerializeField] private HUD _hud;

    private int _maxScore;
    private int _currentScore = 0;
    private Transform _player;
    private List<GameTask> _tasks = new List<GameTask>();

    public string Name => _data.Name;
    public int MaxScore => _maxScore;
    public int CurrentScore => _currentScore;


    private void Awake()
    {
        if (_data == null)
            _data = GameManager.Instance.Level;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _maxScore = _data.MaxScore;

        foreach (TaskData task in _data.Tasks) 
        { 
            _tasks.Add(GameTaskFabric.CreateGameTask(task, _player));
        }
    }

    private void Start()
    {
        _hud.Init(_data.Tasks);
        _hud.UpdateScore(_currentScore, _maxScore);
    }

    private void OnEnable()
    {
        GameTask.TaskComplete += OnTaskComplete;
        Exit.OnExit += OnExit;
    }

    private void OnDisable()
    {
        GameTask.TaskComplete -= OnTaskComplete;
        Exit.OnExit -= OnExit;
    }

    private void FixedUpdate()
    {
        _tasks.ForEach(task => { if (task.Status == TaskStatus.InProgress) task.CheckComplete(); } );
    }

    private void OnTaskComplete(GameTask task)
    {
        print($"Task {task.Name} complete");
        _currentScore += task.Score;
        _hud.UpdateScore( _currentScore, _maxScore );
    }

    private void OnExit()
    {
        GameManager.Instance.Save(this);
        SceneManager.LoadScene("Main");
    }
}
