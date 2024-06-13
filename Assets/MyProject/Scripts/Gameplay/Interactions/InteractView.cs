using System;
using UnityEngine;

public class InteractView : View, IInteractable
{
    [SerializeField] private InteractType _interactType;
    [SerializeField] private Vector3 _openRotate;
    [SerializeField] private Vector3 _closeRotate;
    [SerializeField] private AudioSource _audio;

    public InteractType Type => _interactType;
    public event Func<InteractType, object, InteractType> Interact;

    private void Awake()
    {
        new InteractModel(this, transform, _openRotate, _closeRotate);
    }

    public void OnInteract(object sender)
    {
        _interactType = (InteractType)Interact?.Invoke(_interactType, sender);
        if (_audio != null && _audio.clip != null)
            _audio.Play();
    }
}
