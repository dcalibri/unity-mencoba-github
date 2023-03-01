using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TechnomediaLabs;

namespace Zetcil
{
    public class InteractionController : MonoBehaviour
    {
        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 };
        public enum CAccelerationType { None, Flat, Accelerate }
        public enum CMovementAxis { None, XY, XZ }

        [Space(10)]
        public bool isEnabled;

        [Header("Output Position Vector")]
        public bool usingPositionVector;
        public VarVector3 PositionVector;

        [Header("Output Rotation Vector")]
        public bool usingRotationVector;
        public VarVector3 RotationVector;

        [System.Serializable]
        public class CKeyboardSetting
        {
            public CMovementAxis MovementAxis;
            public CAccelerationType AccelerationType;

            [Header("Primary Button")]
            public KeyCode UpArrow;
            public KeyCode LeftArrow;
            public KeyCode DownArrow;
            public KeyCode RightArrow;

            [Header("Alternative Button")]
            public KeyCode AltUpArrow;
            public KeyCode AltLeftArrow;
            public KeyCode AltDownArrow;
            public KeyCode AltRightArrow;
        }

        [System.Serializable]
        public class CMouseSetting
        {
            [Header("Cursor Settings")]
            public bool CursorLocked;
            public bool CursorVisible;

            [Header("Main Settings")]
            public RotationAxes axes = RotationAxes.MouseXAndY;
            public KeyCode LookKey;

            [Header("Speed Settings")]
            public float sensitivityX = 15F;
            public float sensitivityY = 15F;
            public float minimumX = -360F;
            public float maximumX = 360F;
            public float minimumY = -60F;
            public float maximumY = 60F;

            public float rotationX = 0F;
            public float rotationY = 0F;
            public Quaternion originalRotation;
        }

        [System.Serializable]
        public class CTouchSetting
        {
            [Header("Screen Settings")]
            public float ScreenLimit;

            [Header("Speed Settings")]
            public float speedHorizontal = 2.0f;
            public float speedVertical = 2.0f;

            [Header("Angle Settings")]
            public float MinAngle = -30f;
            public float MaxAngle = 30f;
            public float yaw = 0.0f;
            public float pitch = 0.0f;
            public float roll = 0.0f;
            public bool inversePitch;
            public bool blockUI;
        }

        [System.Serializable]
        public class CAxis2Horizontal
        {
            [Header("Axis Button")]
            public AxisInputUI LeftButton;
            public AxisInputUI RightButton;
        }

        [System.Serializable]
        public class CAxis2Vertical
        {
            [Header("Axis Button")]
            public AxisInputUI UpButton;
            public AxisInputUI DownButton;
        }

        [System.Serializable]
        public class CDirectionPad
        {
            [Header("DirectionPad Button")]
            public CMovementAxis MovementAxis; 
            public Dpad PadButton;
        }

        [System.Serializable]
        public class CAnalogPad
        {
            [Header("AnalogPad Button")]
            public CMovementAxis MovementAxis;
            public Analog AnalogButton;
        }

        [System.Serializable]
        public class CSteeringWheel
        {
            [Header("SteeringWheel Button")]
            public SteeringWheel Wheel;
        }

        [System.Serializable]
        public class CActionButton
        {
            [Header("Axis Button")]
            public AxisInputUI ActionButton;
            public VarBoolean ActionStatus;
            public float CoolDown;
        }

        [Header("Input Keyboard Settings")]
        public bool usingKeyboard;
        public CKeyboardSetting Keyboard;

        [Header("Input Mouse Settings")]
        public bool usingMouse;
        public CMouseSetting Mouse;

        [Header("Input Touch Settings")]
        public bool usingTouch;
        public CTouchSetting Touch;

        [Header("Input HorizontalPad Settings")]
        public bool usingHorizontal;
        public CAxis2Horizontal Horizontal;

        [Header("Input VerticalPad Settings")]
        public bool usingVertical;
        public CAxis2Vertical Vertical;

        [Header("Input DirectionPad Settings")]
        public bool usingDirectionPad;
        public CDirectionPad DirectionPad;

        [Header("Input Jump Button Settings")]
        public bool usingJumpButton;
        public AxisInputUI JumpButton;

        [Header("Input Action Button Settings")]
        public bool usingActionButton;
        public List<CActionButton> ActionButton;

        [Header("Input AnalogPad Settings")]
        public bool usingAnalogPad;
        public CAnalogPad AnalogPad;

        [Header("Input Steering Settings")]
        public bool usingSteering;
        public CSteeringWheel Steering;

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        // Start is called before the first frame update
        void Start()
        {
            Mouse.originalRotation = transform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            if (usingKeyboard)
            {
                PositionVector.CurrentValue.x = 0;
                PositionVector.CurrentValue.y = 0;
                PositionVector.CurrentValue.z = 0;
                if (Input.GetKey(Keyboard.RightArrow) || Input.GetKey(Keyboard.AltRightArrow))
                {
                    if (Keyboard.AccelerationType == CAccelerationType.Flat)
                    {
                        PositionVector.CurrentValue.x = 1;
                    }
                    else
                    {
                        PositionVector.CurrentValue.x = Input.GetAxis("Horizontal");
                    }
                }
                if (Input.GetKey(Keyboard.LeftArrow) || Input.GetKey(Keyboard.AltLeftArrow))
                {
                    if (Keyboard.AccelerationType == CAccelerationType.Flat)
                    {
                        PositionVector.CurrentValue.x = -1;
                    }
                    else
                    {
                        PositionVector.CurrentValue.x = Input.GetAxis("Horizontal");
                    }
                }

                if (Keyboard.MovementAxis == CMovementAxis.XY)
                {
                    if (Input.GetKey(Keyboard.UpArrow) || Input.GetKey(Keyboard.AltUpArrow))
                    {
                        if (Keyboard.AccelerationType == CAccelerationType.Flat)
                        {
                            PositionVector.CurrentValue.y = 1;
                        }
                        else
                        {
                            PositionVector.CurrentValue.y = Input.GetAxis("Vertical");
                        }
                    }
                    if (Input.GetKey(Keyboard.DownArrow) || Input.GetKey(Keyboard.AltDownArrow))
                    {
                        if (Keyboard.AccelerationType == CAccelerationType.Flat)
                        {
                            PositionVector.CurrentValue.y = -1;
                        }
                        else
                        {
                            PositionVector.CurrentValue.y = Input.GetAxis("Vertical");
                        }
                    }
                }

                if (Keyboard.MovementAxis == CMovementAxis.XZ)
                {
                    if (Input.GetKey(Keyboard.UpArrow) || Input.GetKey(Keyboard.AltUpArrow))
                    {
                        if (Keyboard.AccelerationType == CAccelerationType.Flat)
                        {
                            PositionVector.CurrentValue.z = 1;
                        }
                        else
                        {
                            PositionVector.CurrentValue.z = Input.GetAxis("Vertical");
                        }
                    }
                    if (Input.GetKey(Keyboard.DownArrow) || Input.GetKey(Keyboard.AltDownArrow))
                    {
                        if (Keyboard.AccelerationType == CAccelerationType.Flat)
                        {
                            PositionVector.CurrentValue.z = -1;
                        }
                        else
                        {
                            PositionVector.CurrentValue.z = Input.GetAxis("Vertical");
                        }
                    }
                }
            }

            if (usingMouse)
            {
                if (isEnabled && Input.GetKey(Mouse.LookKey) || Mouse.LookKey == KeyCode.None)
                {
                    if (Mouse.CursorLocked)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                    } else
                    {
                        Cursor.lockState = CursorLockMode.None;
                    }

                    if (Mouse.CursorVisible)
                    {
                        Cursor.visible = true;
                    }
                    else
                    {
                        Cursor.visible = false;
                    }

                    if (Mouse.axes == RotationAxes.MouseXAndY)
                    {
                        // Read the mouse input axis
                        Mouse.rotationX += Input.GetAxis("Mouse X") * Mouse.sensitivityX;
                        Mouse.rotationY += Input.GetAxis("Mouse Y") * Mouse.sensitivityY;

                        Mouse.rotationX = ClampAngle(Mouse.rotationX, Mouse.minimumX, Mouse.maximumX);
                        Mouse.rotationY = ClampAngle(Mouse.rotationY, Mouse.minimumY, Mouse.maximumY);

                        Quaternion xQuaternion = Quaternion.AngleAxis(Mouse.rotationX, Vector3.up);
                        Quaternion yQuaternion = Quaternion.AngleAxis(Mouse.rotationY, -Vector3.right);

                        RotationVector.CurrentValue = (Mouse.originalRotation * xQuaternion * yQuaternion).eulerAngles;
                    }
                    else if (Mouse.axes == RotationAxes.MouseX)
                    {
                        Mouse.rotationX += Input.GetAxis("Mouse X") * Mouse.sensitivityX;
                        Mouse.rotationX = ClampAngle(Mouse.rotationX, Mouse.minimumX, Mouse.maximumX);

                        Quaternion xQuaternion = Quaternion.AngleAxis(Mouse.rotationX, Vector3.up);
                        RotationVector.CurrentValue = (Mouse.originalRotation * xQuaternion).eulerAngles;
                    }
                    else
                    {
                        Mouse.rotationY += Input.GetAxis("Mouse Y") * Mouse.sensitivityY;
                        Mouse.rotationY = ClampAngle(Mouse.rotationY, Mouse.minimumY, Mouse.maximumY);

                        Quaternion yQuaternion = Quaternion.AngleAxis(-Mouse.rotationY, Vector3.right);
                        RotationVector.CurrentValue = (Mouse.originalRotation * yQuaternion).eulerAngles;
                    }
                }
            }

            if (usingTouch)
            {
                if (isEnabled)
                {
                    bool canTouch = true;
                    if (Touch.blockUI)
                    {
                        foreach (Touch touch in Input.touches)
                        {
                            int id = touch.fingerId;
                            if (EventSystem.current.IsPointerOverGameObject(id))
                            {
                                if (touch.position.x > Touch.ScreenLimit)
                                {
                                    canTouch = false;
                                }
                            }
                        }
                    }

                    //Check count touchesxAngle
                    if (Input.touchCount > 0 && canTouch)
                    {
                        if (Input.touchCount == 1 && Input.GetTouch(0).position.x > Screen.width * Touch.ScreenLimit)
                        {
                            int inverse = 1;
                            if (Touch.inversePitch) inverse = -1;
                            Touch.yaw += Input.GetTouch(0).deltaPosition.x * Touch.speedHorizontal * inverse * Time.deltaTime;
                            Touch.pitch -= Input.GetTouch(0).deltaPosition.y * Touch.speedVertical * inverse * Time.deltaTime;
                            Touch.pitch = Mathf.Clamp(Touch.pitch, Touch.MinAngle, Touch.MaxAngle);
                            RotationVector.CurrentValue = new Vector3(Touch.pitch, Touch.yaw, 0.0f);
                        }
                        if (Input.touchCount == 2 && Input.GetTouch(0).position.x > Screen.width * Touch.ScreenLimit)
                        {
                            int inverse = 1;
                            if (Touch.inversePitch) inverse = -1;
                            Touch.yaw += Input.GetTouch(0).deltaPosition.x * Touch.speedHorizontal * inverse * Time.deltaTime;
                            Touch.pitch -= Input.GetTouch(0).deltaPosition.y * Touch.speedVertical * inverse * Time.deltaTime;
                            Touch.pitch = Mathf.Clamp(Touch.pitch, Touch.MinAngle, Touch.MaxAngle);
                            RotationVector.CurrentValue = new Vector3(Touch.pitch, Touch.yaw, 0.0f);
                        }
                        if (Input.touchCount == 2 && Input.GetTouch(1).position.x > Screen.width * Touch.ScreenLimit)
                        {
                            int inverse = 1;
                            if (Touch.inversePitch) inverse = -1;
                            Touch.yaw += Input.GetTouch(1).deltaPosition.x * Touch.speedHorizontal * inverse * Time.deltaTime;
                            Touch.pitch -= Input.GetTouch(1).deltaPosition.y * Touch.speedVertical * inverse * Time.deltaTime;
                            Touch.pitch = Mathf.Clamp(Touch.pitch, Touch.MinAngle, Touch.MaxAngle);
                            RotationVector.CurrentValue = new Vector3(Touch.pitch, Touch.yaw, 0.0f);
                        }
                    }
                }
            }

            if (usingHorizontal)
            {
                PositionVector.CurrentValue.x = 0;
               if (Horizontal.LeftButton.isPress)
               {
                    PositionVector.CurrentValue.x = Horizontal.LeftButton.axis.value;
               }
               if (Horizontal.RightButton.isPress)
               {
                    PositionVector.CurrentValue.x = Horizontal.RightButton.axis.value;
               }
           }

           if (usingVertical)
           {
                PositionVector.CurrentValue.y = 0;
               if (Vertical.UpButton.isPress)
               {
                    PositionVector.CurrentValue.y = Vertical.UpButton.axis.value;
               }
               if (Vertical.DownButton.isPress)
               {
                    PositionVector.CurrentValue.y = Vertical.DownButton.axis.value;
               }
           }

           if (usingDirectionPad)
           {
                PositionVector.CurrentValue.x = 0;
                PositionVector.CurrentValue.y = 0;
                PositionVector.CurrentValue.z = 0;
                if (DirectionPad.PadButton.isPress)
                {
                    PositionVector.CurrentValue.x = DirectionPad.PadButton.xAxis.value;
                    if (DirectionPad.MovementAxis == CMovementAxis.XZ)
                    {
                        PositionVector.CurrentValue.z = DirectionPad.PadButton.yAxis.value;
                    }
                    else
                    {
                        PositionVector.CurrentValue.y = DirectionPad.PadButton.yAxis.value;
                    }
                }
           }

           if (usingAnalogPad)
           {
               PositionVector.CurrentValue.x = 0;
               PositionVector.CurrentValue.y = 0;
               PositionVector.CurrentValue.z = 0;
               if (AnalogPad.AnalogButton.isPress)
               {
                    PositionVector.CurrentValue.x = AnalogPad.AnalogButton.xAxis.value;
                    if (AnalogPad.MovementAxis == CMovementAxis.XZ)
                    {
                        PositionVector.CurrentValue.z = AnalogPad.AnalogButton.yAxis.value;
                    }
                    else
                    {
                        PositionVector.CurrentValue.y = AnalogPad.AnalogButton.yAxis.value;
                    }
               }
           }

           if (usingSteering)
           {
               PositionVector.CurrentValue.x = 0;
               PositionVector.CurrentValue.y = 0;
               if (Steering.Wheel.isPress)
               {
                    PositionVector.CurrentValue.x = Steering.Wheel.axis.value;
               }
           }

           if (usingJumpButton)
           {
               if (JumpButton.isPress)
               {
                    PositionVector.CurrentValue.y = JumpButton.axis.value;
               }
           }

           if (usingActionButton)
           {
               for (int i = 0; i < ActionButton.Count; i++)
               {
                   ActionButton[i].ActionStatus.CurrentValue = ActionButton[i].ActionButton.isPress;
               }
           }
        }
    }
}
