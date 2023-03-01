using UnityEngine;
using System.Collections;

public class MouseRotator : MonoBehaviour
{
    [Header("Settings")]
    public bool XAxis;
    public bool YAxis;
    public bool ZAxis;

    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    public void SetXAxis(bool aValue)
    {
        XAxis = aValue;
    }

    public void SetYAxis(bool aValue)
    {
        YAxis = aValue;
    }

    public void SetZAxis(bool aValue)
    {
        ZAxis = aValue;
    }

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            if (XAxis)
            {
                _rotation.x = (_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            }

            if (YAxis)
            {
                _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            }

            if (ZAxis)
            {
                _rotation.z = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            }

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

}