using UnityEngine;
using UnityEngine.AI;

public class FollowState : StateSM
{
    private NavMeshAgent _navMesh;
    private Transform _target;
    private IMoveble _move;
    private float _speed = 0f;
    private float _stopDistance = 2f;

    public FollowState(StateMachine machine, NavMeshAgent navMesh, IMoveble move) : base(machine)
    {
        _move = move;
        _navMesh = navMesh;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public override void Enter()
    {
        Debug.Log("[ENTER] FollowState");
        _speed = 0f;
        _navMesh.stoppingDistance = _stopDistance;
        _navMesh.SetDestination(_target.transform.position);
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] FollowState");
        _speed = 0f;
        _navMesh.stoppingDistance = 0;
    }

    public override void Update()
    {
        float distance = Vector3.Distance(_target.position, _navMesh.transform.position);

        if (_speed < _move.MaxSpeed && _navMesh.hasPath && distance > _stopDistance)
            _speed += _move.SpeedIncrase * Time.deltaTime;

        if (!_navMesh.hasPath)
        {
            _speed = 0f;
            _navMesh.SetDestination(_target.position);
        }
        else if (distance > _stopDistance)
            _navMesh.SetDestination(_target.position);
        else
            _speed = 0f;

        _navMesh.speed = _speed;
    }
}
