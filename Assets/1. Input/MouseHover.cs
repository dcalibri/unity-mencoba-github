using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseHover : MonoBehaviour
{

    [Header("GUI Settings")]
    public string TextCaption = "Hello World";
    public GUISkin gUISkin;
    public Vector2 gUISize = new Vector2(300, 50);
    public Vector2 gUIOffset;

    [Header("Events Settings")]
    public bool usingEventSettings;
    public UnityEvent HoverEvent;
    public UnityEvent ExitEvent;
    bool isHover;

    // Start is called before the first frame update
    void Start()
    {
        isHover = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseExit()
    {
        isHover = false;
        if (usingEventSettings)
        {
            ExitEvent.Invoke();
        }
    }

    void OnMouseOver()
    {

        isHover = true;
        if (usingEventSettings)
        {
            HoverEvent.Invoke();
        }
    }

    void OnGUI()
    {
        if (isHover)
        {
            GUI.skin = gUISkin;
            GUI.Box(new Rect(1 + Input.mousePosition.x + gUIOffset.x, Screen.height - Input.mousePosition.y + gUIOffset.y, gUISize.x, gUISize.y), TextCaption);
        }
    }
}
