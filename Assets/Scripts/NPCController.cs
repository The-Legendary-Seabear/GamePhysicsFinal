using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Rigidbody[] ragdollBodies;
    public CapsuleCollider mainCollider;
    public Animator animator;
    public AudioSource audioSource;
    private bool wasHit = false;
    void Awake()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == this.gameObject)
                continue;

            rb.isKinematic = true;
        }
    }

    public void EnableRagdoll(bool silent = false)
    {
        if (!wasHit && !silent)
        {
            ScoreManager.Instance.AddScore(100);
            audioSource.Play();
            GameManager.Instance.AddTime(4f);
        }
            wasHit = true;
        animator.enabled = false;
        mainCollider.enabled = false;


        foreach (Rigidbody rb in ragdollBodies)
        {
            if (rb.gameObject == this.gameObject)
                continue;

            rb.isKinematic = false;
        }


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
