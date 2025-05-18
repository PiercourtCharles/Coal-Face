using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool IsLocked = false;
    public DoorInteraction DoorInt = new DoorInteraction();

    [SerializeField] Transform _door;
    [SerializeField] Vector2 _openAngle;
    [SerializeField] float _doorSpeed;
    [SerializeField] bool _isDoorOpen = false;

    Vector3 _actualTargetRot;
    float _timer = 0;

    private void Start()
    {
        _actualTargetRot = ChangeRot(_openAngle.x);
    }

    private void FixedUpdate()
    {
        _door.localRotation = Quaternion.Lerp(_door.localRotation, Quaternion.Euler(_actualTargetRot), _doorSpeed);
    }

    public void ChangeTarget()
    {
        _timer = 0;

        if (!IsLocked)
        {
            if (_actualTargetRot.y != _openAngle.y)
            {
                _actualTargetRot = ChangeRot(_openAngle.y);
                _isDoorOpen = true;
            }
            else
            {
                _actualTargetRot = ChangeRot(_openAngle.x);
                _isDoorOpen = false;
            }
        }
    }

    Vector3 ChangeRot(float value)
    {
        return new Vector3(_door.localRotation.x, value, _door.localRotation.z);
    }

    public bool IsDoorOpen()
    {
        return _isDoorOpen;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    float rayon = Mathf.Sqrt(Mathf.Pow(_door.position.x, 2) + Mathf.Pow(_door.position.y, 2));

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(_door.position, new Vector3(rayon * Mathf.Cos(_openAngle.x), 0, rayon * Mathf.Cos(_openAngle.x)));
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawRay(_door.position, new Vector3(rayon * Mathf.Cos(_openAngle.y), 0, rayon * Mathf.Cos(_openAngle.y)));
    //}
}
