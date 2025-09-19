using System;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public float Angle = 0;

    [SerializeField] Transform _parentRotation;
    [Range(0f, 45f)][SerializeField] float _rangeFrom0Snap = 15;
    [Tooltip("Not used now")] [Range(0f, 100f)] [SerializeField] float _rigidity = 0.5f;
    [Tooltip("Minimum and maximum angle")] [SerializeField] Vector2 _angleLimit;
    [SerializeField] bool _isLockAngles = false;
    Vector3? _centerParentScreenPos = null;

    private void Start()
    {
        ChangeAngle(Angle);
    }

    private void OnMouseDown()
    {
        _centerParentScreenPos = Camera.main.WorldToScreenPoint(_parentRotation.position);
    }

    private void OnMouseDrag()
    {
        if (_centerParentScreenPos != null)
        {
            Vector3 centerCam = (Vector3)_centerParentScreenPos;
            Angle = (float)-Math.Atan2(centerCam.y - Input.mousePosition.y, centerCam.x - Input.mousePosition.x) * Mathf.Rad2Deg;    // gets angle between 2 points as degrees

            if (_isLockAngles && (Angle < _angleLimit.x || Angle > _angleLimit.y))
                return;
            else
            {
                ChangeAngle(Angle);
            }
        }
    }

    private void OnMouseUp()
    {
        if (Angle < _rangeFrom0Snap && Angle > -_rangeFrom0Snap)
            ChangeAngle(0);

        _centerParentScreenPos = null;
    }

    void ChangeAngle(float angle)
    {
        _parentRotation.rotation = Quaternion.Euler(_parentRotation.rotation.x, angle, _parentRotation.rotation.z);
        _parentRotation.localRotation = new Quaternion(0, _parentRotation.localRotation.y, 0, _parentRotation.localRotation.w);
        Angle = angle;
    }
}
