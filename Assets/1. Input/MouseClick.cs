using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClick : MonoBehaviour
{
    [Header("Click Event")]
    public UnityEvent TrueClickEvent;
    public UnityEvent FalseClickEvent;
    bool ClickStatus;

    // Start is called before the first frame update
    void Start()
    {
        ClickStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        ClickStatus = !ClickStatus;
        if (ClickStatus)
        {
            TrueClickEvent?.Invoke();
        }
        else 
        {
            FalseClickEvent?.Invoke();
        }
    }

    void InvokeTrueEvent()
    {
        TrueClickEvent.Invoke();
    }

    void InvokeFalseEvent()
    {
        FalseClickEvent.Invoke();
    }
}
