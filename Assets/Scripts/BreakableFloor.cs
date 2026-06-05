using UnityEngine;

    public class BreakableFloor : MonoBehaviour
    {
        public FixedJoint joint;
        public float weakenRate = 500f;

        private bool weakening = false;

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Car"))
            {
                weakening = true;
            }
        }

        void Update()
        {
            if (weakening)
            {
                joint.breakForce -= weakenRate * Time.deltaTime;
                if (joint.breakForce <= 0)
                {
                    Destroy(joint);
                }
            }
        }
    }

