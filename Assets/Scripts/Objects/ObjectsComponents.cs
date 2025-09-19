using UnityEngine;

public class ObjectsComponents : MonoBehaviour
{
    public ObjectInteraction ObjInt = new ObjectInteraction();
    public ObjectInfos ObjectInfos;

    [SerializeField] Task _grabTask;
    [SerializeField] Task _dropTask;

    Transform _originalParent;
    Rigidbody _rb;

    private void Start()
    {
        _originalParent = transform.parent;
        _rb = GetComponent<Rigidbody>();
    }

    public virtual bool Use()
    {
        if (ObjectInfos.Type == ObjectInfos.ObjectType.Key)
        {
            Debug.Log("Key use");
            return true;
        }
        else if (ObjectInfos.Type == ObjectInfos.ObjectType.Tool)
        {
            Debug.Log("Tool use");
            return true;
        }
        else if (ObjectInfos.Type == ObjectInfos.ObjectType.Change)
        {
            Debug.Log("Change use");
            Destroy(this.gameObject);
            return true;
        }
        else
            return false;
    }

    public void Grab(Transform tf)
    {
        if (tf != null)
        {
            Destroy(_rb);

            transform.SetParent(tf);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            if (_grabTask != null)
                _grabTask.TaskComplete();
        }
        else
        {
            transform.SetParent(_originalParent);
            transform.localScale = Vector3.one;

            _rb = gameObject.AddComponent<Rigidbody>();
            _rb.AddForce(transform.forward * 10, ForceMode.Impulse);

            if (_dropTask != null)
                _dropTask.TaskComplete();
        }
    }
}
