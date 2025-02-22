using System;
using System.Linq;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] Transform _parentRotation;
    [Range(0f, 100f)] [SerializeField] float _rigidity = 0.5f;
    [Tooltip("Minimum and maximum angle")] [SerializeField] Vector2 _angleLimit;
    [SerializeField] bool _isLockAngles = false;
    Vector3? _centerParentScreenPos = null;
    Quaternion _initialRotOffset;

    private void Start()
    {
        _initialRotOffset = _parentRotation.rotation;
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
            var angle = (float)-Math.Atan2(centerCam.y - Input.mousePosition.y, centerCam.x - Input.mousePosition.x) * Mathf.Rad2Deg;    // gets angle between 2 points as degrees

            if (_isLockAngles && (angle < _angleLimit.x || angle > _angleLimit.y))
            {
                return;
            }
            else
            {
                _parentRotation.rotation = Quaternion.Euler(_parentRotation.rotation.x, angle, _parentRotation.rotation.z);
                _parentRotation.localRotation = new Quaternion(0, _parentRotation.localRotation.y, 0, _parentRotation.localRotation.w);
            }
        }
    }

    private void OnMouseUp()
    {
        _centerParentScreenPos = null;
    }
}
