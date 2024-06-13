using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : View
{
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private float _interactZoneRadius = 1f;
    [SerializeField] private Transform _backpack;

    private PlayerInput _input;
    private InputAction _interact;
    private new PlayerPresenter _presenter;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _interact = _input.actions["Interact"];
        PlayerModel model = new PlayerModel(GetComponent<Animator>(), transform, _backpack);
        _presenter = new(model, this);
    }

    private void OnEnable()
    {
        _interact.performed += Interact;
    }

    private void OnDisable()
    {
        _interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext input)
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, _interactZoneRadius, _interactLayer);

        if (objects.Length > 0 )
        {
            _presenter.OnInteract(objects);
        }
    }
}
