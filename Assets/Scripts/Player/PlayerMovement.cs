using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool IsParalysed = false;

    [Header("Values :")]
    [SerializeField] CharacterController _controller;

    [Tooltip("Walking speed")]
    [SerializeField] float _speed = 12f;

    [Tooltip("Running speed")]
    [SerializeField] float _runSpeed = 3f;

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
    bool _isGrounded;
    bool _isMoving = false;
    bool _isRunning = false;

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

        //Run
        if (PlayerManager.Instance.PlayerInputs.Player.Run.ReadValue<float>() != 0)
        {
            _controller.Move(move * (_speed + _runSpeed) * Time.deltaTime);
            _isRunning = true;
        }
        else
        {
            _controller.Move(move * _speed * Time.deltaTime);
            _isRunning = false;
        }

        _charaAnimator.SetBool("_isRunning", _isRunning);

        //Jump
        if (PlayerManager.Instance.PlayerInputs.Player.Jump.ReadValue<float>() != 0 && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            _charaAnimator.SetTrigger("IsJumping");
        }

        _velocity.y += _gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundDistance);
    }
}
