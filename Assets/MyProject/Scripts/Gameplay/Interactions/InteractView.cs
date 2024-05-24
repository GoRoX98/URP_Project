using System;
using UnityEngine;

public class InteractView : View, IInteractable
{
    [SerializeField] private InteractType _interactType;

    public InteractType Type => _interactType;
    public event Func<InteractType, System.Object, InteractType> Interact;

    private void Awake()
    {
        new InteractModel(this);
    }

    public void OnInteract(System.Object sender)
    {
        _interactType = (InteractType)Interact?.Invoke(_interactType, sender);
    }
}
