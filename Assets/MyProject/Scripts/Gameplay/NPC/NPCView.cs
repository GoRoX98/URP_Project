using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCView : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepAudio;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedIncrase;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;
    public NavMeshAgent Agent => _agent;
    public event Action<Transform> Interact;

    private void Update()
    {
        _animator.SetFloat("Speed", _agent.speed);
    }
    
    public void OnInteract(Transform target)
    {
        Interact?.Invoke(target);
    }

    private void OnWalk()
    {
        _footstepAudio.Play();
    }
}