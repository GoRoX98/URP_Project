using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private float _walkSpeed = 2f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _sprintCD = 3f;
    private float _stamina;
    private float _timerCD = 0f;

    private float _currentSpeed;
    private float _currentJump = 0f;
    private bool _onGround = true;

    private PlayerInput _input;
    private InputAction _move;
    private InputAction _sprint;
    private InputAction _jump;
    private Vector3 _moveValue;

    public float Stamina => _stamina;

    private void Awake()
    {
        _stamina = _maxStamina;
        _currentSpeed = _walkSpeed;
        _input = GetComponent<PlayerInput>();
        _move = _input.actions["Move"];
        _sprint = _input.actions["Sprint"];
        _jump = _input.actions["Jump"];
        _onGround = _charController.isGrounded;
    }

    private void OnEnable()
    {
        _sprint.performed += _ => _currentSpeed *= 2;
        _sprint.canceled += _ => _currentSpeed = _walkSpeed;
        _jump.performed += Jump;
    }

    private void OnDisable()
    {
        _sprint.performed -= _ => _currentSpeed *= 2;
        _sprint.canceled -= _ => _currentSpeed = _walkSpeed;
        _jump.performed -= Jump;
    }

    private void Update()
    {
        if ((_currentSpeed > _walkSpeed && _stamina <= 0) || _timerCD > 0)
        {
            _currentSpeed = _walkSpeed;
            if (_timerCD <= 0)
                _timerCD = _sprintCD;
            else
                _timerCD -= Time.deltaTime;
        }

        _onGround = _charController.isGrounded;
        _moveValue = new Vector3(_move.ReadValue<Vector2>().x, _currentJump, _move.ReadValue<Vector2>().y);
        _charController.Move(_moveValue * _currentSpeed * Time.deltaTime);
        _animator.SetFloat("Speed", _charController.velocity.magnitude);
        _animator.SetBool("OnGround", _onGround);
        _animator.SetFloat("X", _moveValue.x);
        _animator.SetFloat("Z", _moveValue.z);
        
        if (_currentSpeed > _walkSpeed && _stamina > 0)
        {
            _stamina -= 10 * Time.deltaTime;
        }
        else if (_currentSpeed <= _walkSpeed && _stamina < _maxStamina)
        {
            _stamina += 10 * Time.deltaTime;
        }

        if (_currentJump > 0)
        {
            _currentJump -= 4 * Time.deltaTime;
            _currentJump = _currentJump < 0 ? 0 : _currentJump;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (_onGround)
        {
            _currentJump = _jumpForce;
            _animator.SetTrigger("Jump");
        }
    }
}
