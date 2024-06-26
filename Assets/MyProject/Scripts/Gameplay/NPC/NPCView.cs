using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPCView : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepAudio;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedIncrase;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private TextMeshPro _tmpName;

    private Transform _camera;

    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;
    public NavMeshAgent Agent => _agent;
    public event Action<Transform> Interact;

    private void Awake()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        _tmpName.transform.LookAt(_camera);

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