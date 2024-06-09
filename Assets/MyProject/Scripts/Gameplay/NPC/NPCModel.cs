using UnityEngine;
using UnityEngine.AI;

public class NPCModel : IMoveble
{
    private NavMeshAgent _navMesh;
    private StateMachine _machine;

    private float _timer = 0f;
    private float _speed = 0f;
    private float _maxSpeed;
    private float _speedIncrase;

    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;
    public StateSM State => _machine.CurrentState;

    public NPCModel(NavMeshAgent navMesh, float maxSpeed, float speedIncrase)
    {
        _navMesh = navMesh;
        _maxSpeed = maxSpeed;
        _speedIncrase = speedIncrase;
        _machine = new StateMachine();
        Init();
    }

    private void Init()
    {
        _machine.AddState(new IdleState(_machine, _navMesh));
        _machine.AddState(new MoveState(_machine, _navMesh, this));
        _machine.AddState(new FollowState(_machine, _navMesh, this));
        _machine.SetState<IdleState>();
    }

    public void Update()
    {
        if (_machine.CurrentState is IdleState && _timer > 3f)
        {
            _machine.SetState<MoveState>();
            _timer = 0f;
        }
        else if (_machine.CurrentState is IdleState)
            _timer += Time.deltaTime;

        _machine.Update();
    }

    public void Follow(bool follow, Transform target)
    {
        if (follow)
            _machine.SetState<FollowState>(target);
        else
            _machine.SetState<IdleState>();
    }
}
