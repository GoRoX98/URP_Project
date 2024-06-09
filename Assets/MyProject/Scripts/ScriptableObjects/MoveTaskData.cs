using UnityEngine;

[CreateAssetMenu(fileName = "New Move Task", menuName = "ScriptableObjects/Tasks/Create Move Task")]
public class MoveTaskData : TaskData
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _targetEndPoint;
    [SerializeField] private float _checkDistance = 1f;

    public Transform Target => _target;
    public Transform TargetEndPoint => _targetEndPoint;
    public float CheckDistance => _checkDistance;

    public void SetTarget(Transform target, Transform targetEndPoint)
    {
        _target = target;
        _targetEndPoint = targetEndPoint;
    }
}
