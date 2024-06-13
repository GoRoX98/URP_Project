using UnityEngine;

[CreateAssetMenu(fileName = "New ComeTo Task", menuName = "ScriptableObjects/Tasks/Create ComeTo Task")]
public class ComeToTaskData : TaskData
{
    [SerializeField] private Vector3 _comeToPos;
    [SerializeField] private float _maxCheckDistance;

    public float MaxCheckDistance => _maxCheckDistance;
    public Vector3 ComeToPos => _comeToPos;
}