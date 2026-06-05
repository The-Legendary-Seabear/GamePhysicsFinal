using UnityEngine;

public class CollapsibleBuilding : MonoBehaviour
{
    [Header("Collapse Settings")]
    [SerializeField] float jointBreakForce = 800f;
    [SerializeField] float jointBreakTorque = 800f;
    [SerializeField] float impactForceMultiplier = 60f;

    private Rigidbody[] chunks;
    private Collider hullCollider;
    private bool hasCollapsed = false;

    void Start()
    {
        chunks = GetComponentsInChildren<Rigidbody>();
        hullCollider = GetComponent<Collider>();

        foreach (var rb in chunks)
        {
            rb.isKinematic = true;
        }

        BuildJoints();
    }

    void BuildJoints()
    {
        foreach (var rb in chunks)
        {
            // Find nearby chunks to connect to
            foreach (var other in chunks)
            {
                if (other == rb) continue;

                float dist = Vector3.Distance(rb.transform.position, other.transform.position);

                // Only connect chunks that are close neighbors
                if (dist < 3f)
                {
                    FixedJoint joint = rb.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = other;
                    joint.breakForce = jointBreakForce;
                    joint.breakTorque = jointBreakTorque;
                    joint.enableCollision = false;
                }
            }
        }
    }

    public void Collapse(Vector3 hitPoint, float incomingForce)
    {
        if (hasCollapsed) return;
        hasCollapsed = true;

        if (hullCollider) hullCollider.enabled = false;

        foreach (var rb in chunks)
        {
            rb.isKinematic = false;
        }

        // Apply force only to chunks near the hit point — the joints handle the rest
        foreach (var rb in chunks)
        {
            float dist = Vector3.Distance(rb.transform.position, hitPoint);
            if (dist < 5f)
            {
                Vector3 direction = (rb.transform.position - hitPoint).normalized;
                rb.AddForce(direction * incomingForce * impactForceMultiplier, ForceMode.Impulse);
            }
        }

        Destroy(gameObject, 10f);
    }
}