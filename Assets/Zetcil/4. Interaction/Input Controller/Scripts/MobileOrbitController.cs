using UnityEngine;
using System.Collections;
using TechnomediaLabs;

namespace Zetcil
{
    public class MobileOrbitController : MonoBehaviour
    {
        public bool isEnabled;

        [Header("Main Settings")]
        public Transform target;
        private Rigidbody rigidbody;
        public VarVector3 TargetVector;

        [Header("Speed Settings")]
        public float xSpeed = 120.0f;
        public float ySpeed = 120.0f;

        [Header("Distance Settings")]
        public float distance = 5.0f;
        public float distanceMin = .5f;
        public float distanceMax = 15f;
        public float yMinLimit = -20f;
        public float yMaxLimit = 80f;

        float x = 0.0f;
        float y = 0.0f;

        // Use this for initialization
        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;

            rigidbody = GetComponent<Rigidbody>();

            // Make the rigid body not change rotation
            if (rigidbody != null)
            {
                rigidbody.freezeRotation = true;
            }

            x += TargetVector.CurrentValue.x;
            y -= TargetVector.CurrentValue.y;

            OrbitFunction();
        }

        public void ChangeTarget(Transform NewTarget)
        {
            target = NewTarget;
        }

        void OrbitFunction()
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }

        void LateUpdate()
        {
            if (target)
            {
                if (isEnabled)
                {
                    x = TargetVector.CurrentValue.x * xSpeed * distance * 0.02f;
                    y = TargetVector.CurrentValue.y * ySpeed * 0.02f;
                }

                OrbitFunction();
            }
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}