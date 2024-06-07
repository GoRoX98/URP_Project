using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateSM
{
    private NavMeshAgent _navMesh;

    public IdleState(StateMachine machine, NavMeshAgent navMesh) : base(machine) 
    { 
        _navMesh = navMesh;
    }


    public override void Enter()
    {
        Debug.Log("[ENTER] Idle");
        _navMesh.speed = 0;
    }

    public override void Exit()
    {
        Debug.Log("[EXIT] Idle");
    }

    public override void Update()
    {
        Debug.Log("[UPDATE] Idle");
    }
}
