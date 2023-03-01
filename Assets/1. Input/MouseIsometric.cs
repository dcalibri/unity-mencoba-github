using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseIsometric : MonoBehaviour
{
    [Header("Main Settings")]
    Camera TargetCamera;
    public float MoveSpeed = 5;
    public float LookSpeed = 500;
    public float PanSpeed = 3;
    public float ScrollSpeed = 50;

    [Header("Mouse Settings")]
    KeyCode MouseLookButton = KeyCode.Mouse1;
    int MousePanButton = 2;
    private float X;
    private float Y;
    Vector3 lastPosition;

    RaycastHit RaycastResult;

    [Header("Distance Setting")]
    public float distance = 5f;
    public float minDistance = 1f; //Min distance of the camera from the target
    public float maxDistance = 10f;
    public int yMinLimit = 10; //Lowest vertical angle in respect with the target.
    public int yMaxLimit = 80;
    public float mouseXSpeed = 1000.0f;
    public float mouseYSpeed = 1000.0f;
    private float x = 0.0f;
    private float y = 0.0f;

    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

    void StartPosition()
    {
        const float minX = 0.0f;
        const float maxX = 360.0f;
        const float minY = -90.0f;
        const float maxY = 90.0f;

        X += Input.GetAxis("Mouse X") * (LookSpeed * Time.deltaTime);
        if (X < minX) X += maxX;
        else if (X > maxX) X -= maxX;
        Y -= Input.GetAxis("Mouse Y") * (LookSpeed * Time.deltaTime);
        if (Y < minY) Y = minY;
        else if (Y > maxY) Y = maxY;

        Y = TargetCamera.transform.eulerAngles.x;
        X = TargetCamera.transform.eulerAngles.y;
        TargetCamera.transform.rotation = Quaternion.Euler(Y, X, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetCamera = GetComponent<Camera>();
        Vector3 euler = TargetCamera.transform.rotation.eulerAngles;
        X = euler.x;
        Y = euler.y;
        StartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //-- MOVE BUTTON
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            TargetCamera.transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * 0.1f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            TargetCamera.transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            TargetCamera.transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed * 0.1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TargetCamera.transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            TargetCamera.transform.Translate(Vector3.back * Time.deltaTime * MoveSpeed * 0.1f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            TargetCamera.transform.Translate(Vector3.back * Time.deltaTime * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            TargetCamera.transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed * 0.1f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TargetCamera.transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        }


        //-- LOOK BUTTON
        if ((Input.GetKey(MouseLookButton)))
        {
            const float minX = 0.0f;
            const float maxX = 360.0f;
            const float minY = -90.0f;
            const float maxY = 90.0f;

            X += Input.GetAxis("Mouse X") * (LookSpeed * Time.deltaTime);
            if (X < minX) X += maxX;
            else if (X > maxX) X -= maxX;
            Y -= Input.GetAxis("Mouse Y") * (LookSpeed * Time.deltaTime);
            if (Y < minY) Y = minY;
            else if (Y > maxY) Y = maxY;

            TargetCamera.transform.rotation = Quaternion.Euler(Y, X, 0.0f);
        }

        //-- PAN BUTTON
        if (Input.GetMouseButtonDown(MousePanButton))
        {
            lastPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(MousePanButton))
        {
            Vector3 delta = Input.mousePosition - lastPosition;
            TargetCamera.transform.Translate(-delta.x * PanSpeed * Time.deltaTime, -delta.y * PanSpeed * Time.deltaTime, 0);
            lastPosition = Input.mousePosition;
        }

        //-- ZOOM BUTTON
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (TargetCamera.orthographic)
            {
                if (TargetCamera.orthographicSize > 10)
                {
                    TargetCamera.orthographicSize -= 1 * ScrollSpeed;
                }
            }
            else
            {
                TargetCamera.transform.Translate(Vector3.forward * ScrollSpeed * Time.deltaTime);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (TargetCamera.orthographic)
            {
                TargetCamera.orthographicSize += 1 * ScrollSpeed;
            }
            else
            {
                TargetCamera.transform.Translate(Vector3.back * ScrollSpeed * Time.deltaTime);
            }
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
