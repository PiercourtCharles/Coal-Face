using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Values :")]
    public Transform CamTargetPos;
    [Range(0, 200)]
    public float MouseSensitivity = 100f;
    public bool IsOnHead = true;

    [SerializeField] Transform _playerBody;

    Vector3 _originalLocalPosition;
    float _xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _originalLocalPosition = transform.localPosition;
        _xRotation = transform.localRotation.x;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGamePause && !GameManager.Instance.Player.Stats.IsDead)
        {
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }

        if (IsOnHead)
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalLocalPosition, 0.5f);
        else
            transform.position = Vector3.Lerp(transform.position, CamTargetPos.position, 0.5f);
    }
}
