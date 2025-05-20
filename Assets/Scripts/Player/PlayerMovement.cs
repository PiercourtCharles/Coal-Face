using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    public bool IsParalysed = false;

    [Header("Values :")]
    [SerializeField] CharacterController _controller;

    [Tooltip("Walking speed")]
    [SerializeField] float _speed = 12f;

    [Tooltip("Running speed")]
    [SerializeField] float _runSpeed = 3f;

    [Tooltip("Crouching speed")]
    [SerializeField] float _crouchSpeed = 2f;
    [SerializeField] Vector3 _crouchCenterCollider = new Vector3(0,-0.5f,0);
    [SerializeField] float _crouchHeightCollider = 1;

    [Tooltip("Gravity / fall speed")]
    [SerializeField] float _gravity = -15; //-9.81f;

    [Tooltip("Jump force")]
    [SerializeField] float _jumpForce = 3f;
    [SerializeField] Animator _charaAnimator;

    [Header("Ground :")]
    [Tooltip("Radius check")]
    [SerializeField] float _groundDistance = 0.4f;

    [Tooltip("Position to check if grounded")]
    [SerializeField] Transform _groundCheck;

    [Tooltip("Layer of ground checking")]
    [SerializeField] LayerMask _groundMask;

    Vector3 _velocity;
    Vector3 _initialHeightPos;
    float _initialHeight;
    bool _isGrounded;
    bool _isMoving = false;
    bool _isRunning = false;
    bool _isCrouching = false;

    private void Start()
    {
        _initialHeight = _controller.height;
        _initialHeightPos = _controller.center;
    }

    void Update()
    {
        if (PlayerManager.Instance.UiManager.IsGamePause)
            return;

        if (PlayerManager.Instance.Stats.IsDead || IsParalysed)
            return;

        //Ground check
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        //Move
        float x = PlayerManager.Instance.PlayerInputs.Player.Move.ReadValue<Vector2>().x;
        float z = PlayerManager.Instance.PlayerInputs.Player.Move.ReadValue<Vector2>().y;

        Vector3 move = transform.right * x + transform.forward * z;

        if (move.magnitude > 0)
            _isMoving = true;
        else
            _isMoving = false;

        _charaAnimator.SetBool("IsWalking", _isMoving);

        float additionnalMoveSpeed = 1;

        //Run
        if (PlayerManager.Instance.PlayerInputs.Player.Run.ReadValue<float>() != 0)
        {
            additionnalMoveSpeed = _runSpeed;
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }

        _charaAnimator.SetBool("IsRunning", _isRunning);

        //Crouch
        if (PlayerManager.Instance.PlayerInputs.Player.Crouch.ReadValue<float>() != 0)
        {
            _controller.height = _crouchHeightCollider;
            _controller.center = _crouchCenterCollider;

            additionnalMoveSpeed = -_crouchSpeed;
            _isCrouching = true;
        }
        else
        {
            _controller.height = _initialHeight;
            _controller.center = _initialHeightPos;

            _isCrouching = false;
        }

        _charaAnimator.SetBool("IsCrouching", _isCrouching);

        //Jump
        if (PlayerManager.Instance.PlayerInputs.Player.Jump.ReadValue<float>() != 0 && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            _charaAnimator.SetTrigger("IsJumping");
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity + move * (_speed + additionnalMoveSpeed) * Time.deltaTime);

        //Debug.Log((new Vector3(_velocity.x, 0, _velocity.z) + move * (_speed + additionnalMoveSpeed)).magnitude);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
    }
}
