using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Create Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<TaskData> _tasksData = new List<TaskData>();

    public List<TaskData> Tasks => _tasksData;
    public int MaxScore
    {
        get
        {
            if (_tasksData.Count == 0)
                return 0;

            int score = 0;
            _tasksData.ForEach(data => score += data.Score);
            return score;
        }
    }
}
