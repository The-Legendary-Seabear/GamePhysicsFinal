using UnityEngine;

public class ExplosionBarrel : MonoBehaviour
{
    public float explosionForce = 50000f;
    public float explosionRadius = 5f;
    public AudioClip explosionSound;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Car"))
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = hit.GetComponentInParent<Rigidbody>();
                }
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);
                }
            }
            Destroy(gameObject);
        }
    }
}
