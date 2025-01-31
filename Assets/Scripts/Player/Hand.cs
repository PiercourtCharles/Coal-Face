using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject ObjectInHand = null;
    public Transform HandTransform;

    [SerializeField] Transform _armMeshTransform;
    [SerializeField] Transform _handTransform;
    [SerializeField] float _lerpDelay = 0.9f;

    private void Start()
    {
        HandTransform = _handTransform;
    }

    private void Update()
    {
        _armMeshTransform.position = Vector3.Lerp(_armMeshTransform.position, transform.position, _lerpDelay);
        _armMeshTransform.rotation = Quaternion.Lerp(_armMeshTransform.rotation, transform.rotation, _lerpDelay);
    }
}
