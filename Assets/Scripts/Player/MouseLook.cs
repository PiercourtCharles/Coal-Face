using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Values :")]
    public Transform CamTargetPosOnHead;
    public Transform CamTargetPos;

    [Range(0, 200)]
    public float MouseSensitivity = 100f;

    [Range(0f, 1f)]
    [Tooltip("When changing pos")]
    public float CamSpeedTargetChange = 0.1f;
    public bool IsOnHead = true;

    [SerializeField] Transform _playerBody;
    float _xRotation = 0f;

    void Start()
    {
        _xRotation = transform.localRotation.x;
    }

    void Update()
    {
        if (PlayerManager.Instance.UiManager.IsGamePause)
            return;

        if (PlayerManager.Instance.Stats.IsDead)
            return;

        if (IsOnHead)
        {
            float mouseX = PlayerManager.Instance.PlayerInputs.Player.Look.ReadValue<Vector2>().x * MouseSensitivity / 10 * Time.deltaTime;
            float mouseY = PlayerManager.Instance.PlayerInputs.Player.Look.ReadValue<Vector2>().y * MouseSensitivity / 10 * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);

            transform.position = Vector3.Lerp(transform.position, CamTargetPosOnHead.position, CamSpeedTargetChange);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, CamTargetPos.position, CamSpeedTargetChange);
            transform.rotation = Quaternion.Lerp(transform.rotation, CamTargetPos.rotation, CamSpeedTargetChange);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Debug see front object if pb
    //private void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
    //    {
    //        Debug.Log(hit.transform.name);
    //    }
    //}
}
