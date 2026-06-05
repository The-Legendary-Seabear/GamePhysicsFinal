using UnityEngine;


public class StickyMudZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CarController car = other.GetComponentInParent<CarController>();
        if (car != null) car.EnterMud();
    }

    private void OnTriggerExit(Collider other)
    {
        CarController car = other.GetComponentInParent<CarController>();
        if (car != null) car.ExitMud();
    }
}