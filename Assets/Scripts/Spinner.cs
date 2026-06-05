using UnityEngine;

using UnityEngine;

public class SpinningCross : MonoBehaviour
{
    [Header("Spin Settings")]
    public float spinSpeed = 90f;        // degrees per second
    public Vector3 spinAxis = Vector3.up; // Y = flat spin, Z = wheel spin

    [Header("Optional Bobbing")]
    public bool bob = false;
    public float bobHeight = 0.5f;
    public float bobSpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Spin
        transform.Rotate(spinAxis * spinSpeed * Time.deltaTime);

        // Optional bobbing up/down
        if (bob)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
            transform.position = new Vector3(startPos.x, newY, startPos.z);
        }
    }
}
