using UnityEngine;
using System.Collections;

namespace Zetcil
{
    public class MobileLookController : MonoBehaviour
    {

        public enum RotationAxes { TouchXY = 0, TouchX = 1, TouchY = 2 };

        public bool isEnabled;

        [Header("Vector Settings")]
        public VarVector3 TargetVector;
        public RotationAxes Axes;

        [Header("Speed Settings")]
        public float minimumX = -360F;
        public float maximumX = 360F;
        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationX = 0F;
        float rotationY = 0F;

        Quaternion originalRotation;

        void Update()
        {
                if (Axes == RotationAxes.TouchXY)
                {
                    // Read the mouse input axis
                    rotationX = TargetVector.CurrentValue.y;
                    rotationY = TargetVector.CurrentValue.x;

                    rotationX = ClampAngle(rotationX, minimumX, maximumX);
                    rotationY = ClampAngle(rotationY, minimumY, maximumY);

                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                    Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

                    transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                }
                else if (Axes == RotationAxes.TouchX)
                {
                    rotationY = TargetVector.CurrentValue.y;
                    rotationY = ClampAngle(rotationY, minimumY, maximumX);

                    Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.up);
                    transform.localRotation = originalRotation * yQuaternion;
                }
                else
                {
                    rotationX = TargetVector.CurrentValue.x;
                    rotationX = ClampAngle(rotationX, minimumX, maximumX);

                    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.right);
                    transform.localRotation = originalRotation * xQuaternion;
                }
        }

        void Start()
        {
            // Make the rigid body not change rotation
            originalRotation = transform.localRotation;
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