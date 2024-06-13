using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData _data;
    [SerializeField] private HUD _hud;
    [SerializeField] private List<Transform> _transformTaskList = new List<Transform>();

    private int _maxScore;
    private int _currentScore = 0;
    private Transform _player;
    private List<GameTask> _tasks = new List<GameTask>();
    private List<NPCPresenter> _npcs = new List<NPCPresenter>();

    public string Name => _data.Name;
    public int MaxScore => _maxScore;
    public int CurrentScore => _currentScore;
    public LevelData Data => _data;

    private void Awake()
    {
        if (_data == null)
            _data = GameManager.Instance.Level;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _maxScore = _data.MaxScore;
        List<Transform> transformsTasks = new(_transformTaskList);


        foreach (TaskData task in _data.Tasks) 
        { 
            if (task is MoveTaskData moveTask)
            {
                moveTask.SetTarget(transformsTasks[0], transformsTasks[1]);
                transformsTasks.RemoveRange(0, 2);
            }

            _tasks.Add(GameTaskFabric.CreateGameTask(task, _player));
        }

        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
            _npcs.Add(new NPCPresenter(npc.GetComponent<NPCView>()));
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

    private void Update()
    {
        _npcs.ForEach(x => x.Update());
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

#if UNITY_EDITOR
[CustomEditor(typeof(LevelManager))]
public class EditorLevelManager : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SerializedProperty taskList = serializedObject.FindProperty("_transformTaskList");
        int count = 0;
        List<TaskData> data = ((LevelData)serializedObject.FindProperty("_data").objectReferenceValue).Tasks;

        foreach (TaskData task in data) 
        { 
            if (task is MoveTaskData)
            {
                count += 2;
                if (count > taskList.arraySize)
                    taskList.arraySize += 2;
            }
        }

        if (count < taskList.arraySize)
            taskList.arraySize = count;

        serializedObject.ApplyModifiedProperties();
    }
}
#endif