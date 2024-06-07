using UnityEngine;
using UnityEngine.AI;

public class NPCView : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedIncrase;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;
    public NavMeshAgent Agent => _agent;

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.speed);
    }
}
