using UnityEngine;

public class BeltRef : MonoBehaviour
{
    public Belt Belt;
    [SerializeField] Vector3 _direction;
    [SerializeField] float _speed;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        transform.localPosition += _direction.normalized * _speed * Time.deltaTime;
    }
}
