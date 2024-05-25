using DG.Tweening;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class InteractModel : Model
{
    private IInteractable _interactable;
    private Transform _transform;
    private Vector3 _open, _close;
    private float _time = 2f;

    public InteractModel(IInteractable view, Transform transform) : base()
    {
        _interactable = view;
        _interactable.Interact += OnInteract;
        _transform = transform;
    }
    public InteractModel(IInteractable interactable, Transform transform, Vector3 open, Vector3 close) : this(interactable, transform)
    {
        _open = open;
        _close = close;
    }

    ~InteractModel()
    {
        _interactable.Interact -= OnInteract;
    }

    private InteractType OnInteract(InteractType type, object sender)
    {
        if (sender is not ICanInteract)
            return type;

        switch (type)
        {
            case InteractType.Open:
                _transform.DORotate(_open, _time, RotateMode.FastBeyond360);
                return InteractType.Close;
            case InteractType.Close:
                _transform.DORotate(_close, _time, RotateMode.FastBeyond360);
                return InteractType.Open;
            case InteractType.Destroy:
                Destroy();
                return type;
            default:
                return type;
        }
    }

    private async void Destroy()
    {
        await Task.Delay((int)_time * 1000);

        UnityEngine.Object.Destroy(_transform.gameObject);
    }
}

public enum InteractType
{
    Open,
    Close,
    Pickup,
    Drop,
    Destroy
}

public interface IInteractable
{
    public InteractType Type { get; }
    public event Func<InteractType, object, InteractType> Interact;
}