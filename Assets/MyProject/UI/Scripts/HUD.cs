using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] Transform _taskList;
    [SerializeField] GameObject _taskPrefab;

    private Dictionary<TaskData, TextMeshProUGUI> _tasksUI = new Dictionary<TaskData, TextMeshProUGUI>();

    private void OnEnable()
    {
        GameTask.TaskComplete += OnTaskComplete;
    }

    private void OnDisable()
    {
        GameTask.TaskComplete -= OnTaskComplete;
    }

    public void Init(List<TaskData> tasks)
    {
        foreach (TaskData task in tasks)
            CreateTaskUI(task);
    }

    public void UpdateScore(int score, int maxScore) => _score.text = $"{score} / {maxScore}";

    private void CreateTaskUI(TaskData task)
    {
        TextMeshProUGUI tmp = Instantiate(_taskPrefab, _taskList).GetComponent<TextMeshProUGUI>();
        tmp.text = $"- {task.Name} {task.Score}";
        _tasksUI.Add(task, tmp);
    }

    private void OnTaskComplete(GameTask task)
    {
        if (_tasksUI.TryGetValue(task.Data, out TextMeshProUGUI tmp))
            tmp.fontStyle = FontStyles.Strikethrough;
    }
}
