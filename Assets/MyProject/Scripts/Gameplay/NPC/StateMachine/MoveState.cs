using UnityEngine;
using UnityEngine.AI;

public class MoveState : StateSM
{
    private NavMeshAgent _navMesh;
    private IMoveble _move;
    private float _speed = 0f;

    public MoveState(StateMachine machine, NavMeshAgent navMesh, IMoveble move) : base(machine)
    {
        _move = move;
        _navMesh = navMesh;
    }

    public override void Enter()
    {
        Debug.Log("[ENTER] MoveState");
        _navMesh.destination = TakeNewPath();
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] MoveState");
        _speed = 0f;
    }

    public override void Update()
    {
        if (_speed < _move.MaxSpeed)
            _speed += _move.SpeedIncrase * Time.deltaTime;

        _navMesh.speed = _speed;

        if (!_navMesh.hasPath)
            _machine.SetState<IdleState>();
    }

    private Vector3 TakeNewPath()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        int index = Random.Range(0, data.vertices.Length);
        return data.vertices[index];
    }
}

