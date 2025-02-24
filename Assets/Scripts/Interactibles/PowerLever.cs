using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PowerLever : MonoBehaviour
{
    public float Value = 0;

    [Range(0f, 1f)][SerializeField] float _rigidity = 0.5f;
    [Range(0f, 1f)][SerializeField] float _rangeFrom0Snap = 0.1f;
    [Tooltip("Minimum and maximum distance")][SerializeField] Vector2 _distanceLimit;
    [Tooltip("Vertical = true | Horizontal = false")][SerializeField] bool _isVertical;
    Vector3? _hitMousePos = null;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Value);
    }

    private void OnMouseDrag()
    {
        _hitMousePos = Camera.main.WorldToScreenPoint(transform.position);

        if (_hitMousePos != null)
        {
            Vector3 dir = Input.mousePosition - (Vector3)_hitMousePos;

            if (_isVertical)
            {
                float sign = 1 * Mathf.Sign(dir.y);
                dir = new Vector3(0, 0, dir.y).normalized;
                dir = Vector3.forward * sign;
            }
            else
            {
                float sign = 1 * Mathf.Sign(dir.x);
                dir = new Vector3(0, 0, dir.x).normalized;
                dir = Vector3.forward * sign;
            }

            if (transform.localPosition.z >= _distanceLimit.x && transform.localPosition.z <= _distanceLimit.y)
            {
                transform.localPosition += dir * _rigidity * Time.deltaTime;

                if (transform.localPosition.z < _distanceLimit.x)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _distanceLimit.x);

                if (transform.localPosition.z > _distanceLimit.y)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _distanceLimit.y);

                Value = transform.localPosition.z;
            }
        }
    }

    private void OnMouseUp()
    {
        if (transform.localPosition.z < _rangeFrom0Snap && transform.localPosition.z > -_rangeFrom0Snap)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            Value = transform.localPosition.z;
        }

        _hitMousePos = null;
    }
}
