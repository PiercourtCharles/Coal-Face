using UnityEngine;

public class FurnaseAlimentation : MonoBehaviour
{
    [SerializeField] Furnase _furnase;

    private void OnTriggerEnter(Collider other)
    {
        var fuel = other.GetComponent<Fuel>();

        if (fuel != null)
        {
            _furnase.Repair(fuel);
            Destroy(fuel.gameObject);
        }
    }
}
