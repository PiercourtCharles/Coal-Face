using UnityEngine;

public class PowerLever : MonoBehaviour
{
    public float Value = 0;

    [Range(0f, 1f)] [SerializeField] float _rigidity = 0.5f;
    [Tooltip("Minimum and maximum distance")] [SerializeField] Vector2 _distanceLimit;
    [Tooltip("Vertical = true | Horizontal = false")] [SerializeField] bool _isVertical;
    Vector3? _hitMousePos = null;

    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Value);
    }

    private void OnMouseDown()
    {
        _hitMousePos = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDrag()
    {
        if (_hitMousePos != null)
        {
            Vector3 dir = Input.mousePosition - (Vector3)_hitMousePos;
            Debug.Log(dir);

            if (_isVertical)
                dir = new Vector3(0, 0, dir.y * Mathf.Sign(dir.y)).normalized;
            else
                dir = new Vector3(0, 0, dir.x * Mathf.Sign(dir.y)).normalized;

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
        _hitMousePos = null;
    }
}
