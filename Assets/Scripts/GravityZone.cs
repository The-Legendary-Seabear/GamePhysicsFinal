using UnityEngine;


public class GravityZone : MonoBehaviour
{
    [Header("Gravity Settings")]
    public Vector3 zoneGravity = new Vector3(0, 5f, 0); // Reversed gravity

    private Vector3 _originalGravity;
    private int _bodiesInZone = 0; // Track count in case multiple objects enter

    private void Start()
    {
        // Make sure this object has a trigger collider
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        if (_bodiesInZone == 0)
        {
            _originalGravity = Physics.gravity;
            Physics.gravity = zoneGravity;
        }
        _bodiesInZone++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        _bodiesInZone--;
        if (_bodiesInZone <= 0)
        {
            _bodiesInZone = 0;
            Physics.gravity = _originalGravity;
        }
    }

    // Safety net: restore gravity if object is destroyed mid-overlap
    private void OnDestroy()
    {
        if (_bodiesInZone > 0)
            Physics.gravity = _originalGravity;
    }
}

