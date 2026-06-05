using UnityEngine;

public class SpringTrap : MonoBehaviour
{
    public float springForce = 15000f;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Hit: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Car"))
        {
            Debug.Log("Car hit spring!");
            col.rigidbody.AddForce(Vector3.up * springForce, ForceMode.Impulse);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER DETECTED");
    }
}