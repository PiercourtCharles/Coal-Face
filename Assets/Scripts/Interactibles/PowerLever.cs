using UnityEngine;

public class PowerLever : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] float _rigidity = 0.5f;
    [Tooltip("Minimum and maximum distance")] [SerializeField] Vector2 _distanceLimit;
    [Tooltip("Vertical = true | Horizontal = false")] [SerializeField] bool _isVertical;
    Vector3? _hitMousePos = null;

    private void OnMouseDown()
    {
        _hitMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (_hitMousePos != null)
        {
            Vector3 dir = (Vector3)_hitMousePos - Input.mousePosition;

            if (_isVertical)
                dir = new Vector3(0, 0, -dir.y).normalized;
            else
                dir = new Vector3(0, 0, dir.x).normalized;

            if (transform.localPosition.z > _distanceLimit.x && transform.localPosition.z < _distanceLimit.y)
                transform.localPosition += dir * _rigidity * Time.deltaTime;

        }
    }

    private void OnMouseUp()
    {
        _hitMousePos = null;
    }
}
