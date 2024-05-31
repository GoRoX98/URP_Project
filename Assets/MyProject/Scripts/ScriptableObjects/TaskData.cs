using UnityEngine;

public abstract class TaskData : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected int _score;

    public string Name => _name;
    public string Description => _description;
    public int Score => _score;
}
