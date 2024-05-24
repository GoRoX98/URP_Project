using System;
using UnityEngine;

public class InteractModel : Model
{
    private IInteractable _interactable;

    public InteractModel(InteractView view) : base(view)
    {
        _interactable = view;
        _interactable.Interact += OnInteract;
    }
    ~InteractModel()
    {
        _interactable.Interact -= OnInteract;
    }

    private InteractType OnInteract(InteractType type, System.Object sender)
    {
        if (sender is not ICanInteract)
            return type;

        Debug.Log("Interact succes");
        return type;
    }
}

public enum InteractType
{
    Open,
    Close,
    Pickup,
    Drop
}

public interface IInteractable
{
    public InteractType Type { get; }
    public event Func<InteractType, System.Object, InteractType> Interact;
}