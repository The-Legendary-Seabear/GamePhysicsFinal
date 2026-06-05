using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Mushroom hit by: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Car"))
        {
            audioSource.Play();
        }
    }
}
