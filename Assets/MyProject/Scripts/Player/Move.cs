using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepAudio;
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
    private float _groundTimeReaction = 1f;
    private float _groundTimer = 0f;

    private PlayerInput _input;
    private InputAction _move;
    private InputAction _sprint;
    private InputAction _jump;
    private InputAction _rotate;
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
        _rotate = _input.actions["Rotation"];
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
        float x = _rotate.ReadValue<float>();
        transform.Rotate(Vector3.up, x);
        

        if ((_currentSpeed > _walkSpeed && _stamina <= 0) || _timerCD > 0)
        {
            _currentSpeed = _walkSpeed;
            if (_timerCD <= 0)
                _timerCD = _sprintCD;
            else
                _timerCD -= Time.deltaTime;
        }

        _onGround = IsOnGround();
        _moveValue = new Vector3(0, _currentJump, 0) + _move.ReadValue<Vector2>().x * transform.right + _move.ReadValue<Vector2>().y * transform.forward;
        _charController.Move(_moveValue * _currentSpeed * Time.deltaTime);
        
        UpdateAnimator();
        UpdateStamina();

        if (_currentJump > 0)
        {
            _currentJump -= 4 * Time.deltaTime;
            _currentJump = _currentJump < 0 ? 0 : _currentJump;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Boost")
        {
            StartCoroutine(SpeedUpCoroutine());
            Destroy(col.gameObject);
        }
    }

    private IEnumerator SpeedUpCoroutine()
    {
        float oldSpeed = _walkSpeed;
        _walkSpeed *= 2;
        _currentSpeed = _walkSpeed;

        yield return new WaitForSeconds(3);

        _walkSpeed = oldSpeed;
    }

    private bool IsOnGround()
    {
        if (!_charController.isGrounded)
            _groundTimer += Time.deltaTime;
        else
            _groundTimer = 0f;

        if (_groundTimer < _groundTimeReaction)
            return true;
        else
            return false;
    }

    private void UpdateAnimator()
    {
        _animator.SetFloat("Speed", _charController.velocity.magnitude);
        _animator.SetBool("OnGround", _onGround);
        _animator.SetFloat("X", _move.ReadValue<Vector2>().x);
        _animator.SetFloat("Z", _move.ReadValue<Vector2>().y);
    }

    private void UpdateStamina()
    {
        if (_currentSpeed > _walkSpeed && _stamina > 0)
        {
            _stamina -= 10 * Time.deltaTime;
        }
        else if (_currentSpeed <= _walkSpeed && _stamina < _maxStamina)
        {
            _stamina += 10 * Time.deltaTime;
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

    private void OnWalk()
    {
        _footstepAudio.Play();
    }
}
