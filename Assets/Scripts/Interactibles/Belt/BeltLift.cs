using UnityEngine;

public class BeltLift : MonoBehaviour
{
    public Interactible Lever;

    [SerializeField] Vector3 _openRot;
    [SerializeField] Vector3 _closeRot;
    [SerializeField] float _liftSpeed;
    [SerializeField] bool _isLift = false;

    Vector3 _actualTargetPos;
    Vector3 _actualTargetRot;

    private void Start()
    {
        _actualTargetRot = _closeRot;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        ChangeTarget(Lever.IsActive);
    }

    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_actualTargetRot), _liftSpeed);
    }

    public void ChangeTarget(bool value)
    {
        if (value)
        {
            _actualTargetRot = _openRot;
            _isLift = true;
        }
        else
        {
            _actualTargetRot = _closeRot;
            _isLift = false;
        }
    }
}
