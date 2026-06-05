using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CarController;
public class CarController : MonoBehaviour
{
    [System.Serializable]
    public struct Wheel
    {
        public WheelCollider collider;
        public Transform transform;
    }
    [System.Serializable]
    public struct Axle
    {
        public Wheel leftWheel;
        public Wheel rightWheel;
        public bool isMotor;
        public bool isSteering;
    }
    [SerializeField] Axle[] axles;
    [SerializeField] float maxMotorTorque;
    [SerializeField] float maxSteeringAngle;

    private bool inMud = false;
    [Header("Mud Zone Settings")]
    [SerializeField] float mudTorqueMultiplier = 0.2f;
    [SerializeField] float mudBrakeTorque = 200f;
    [SerializeField] Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {


        float motor = Input.GetAxis("Vertical") * maxMotorTorque;//<input vertical axis * max motor torque>
        float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;//<input horizontal axis * max steering angle>

        if (inMud)
            motor *= mudTorqueMultiplier;

        foreach (Axle axle in axles)
        {
            if (axle.isSteering)
            {
                axle.leftWheel.collider.steerAngle = steering;
                axle.rightWheel.collider.steerAngle = steering;//< set axle right wheel collider steer angle>
            }
            if (axle.isMotor)
            {
                axle.leftWheel.collider.motorTorque = motor;
                axle.rightWheel.collider.motorTorque = motor;//<set axle right wheel collider motor torque>
            }

            if (inMud)
            {
                bool accelerating = Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
                float resistance = accelerating ? mudBrakeTorque * 0.5f : mudBrakeTorque;
                axle.leftWheel.collider.brakeTorque = resistance;
                axle.rightWheel.collider.brakeTorque = resistance;
            }
            else
            {
                axle.leftWheel.collider.brakeTorque = 0f;
            }

            UpdateWheelTransform(axle.leftWheel);
            UpdateWheelTransform(axle.rightWheel);
        }
    }

    public void UpdateWheelTransform(Wheel wheel)
    {
        wheel.collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        wheel.transform.position = position;
        wheel.transform.rotation = rotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        NPCController npc = collision.gameObject.GetComponentInParent<NPCController>();

        if (npc != null)
        {
            npc.EnableRagdoll();
        }

        CollapsibleBuilding building = collision.gameObject.GetComponentInParent<CollapsibleBuilding>();
        if (building != null)
        {
            float speed = collision.relativeVelocity.magnitude;
            if (speed > 5f) // minimum speed threshold
            {
                Vector3 hitPoint = collision.contacts[0].point;
                float force = speed * 80f;
                building.Collapse(hitPoint, force);
            }
        }
    }

    public void EnterMud()
    {
        inMud = true;
        if (rb != null) rb.linearDamping = 4f;
    }

    public void ExitMud()
    {
        inMud = false;
        if (rb != null) rb.linearDamping = 0f;

        foreach (Axle axle in axles)
        {
            axle.leftWheel.collider.brakeTorque = 0f;
            axle.rightWheel.collider.brakeTorque = 0f;
        }
    }
}