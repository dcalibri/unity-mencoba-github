using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zetcil
{
    public class HoverController : MonoBehaviour
    {
        [Space(10)]
        public bool isEnabled;

        [Header("Material Settings")]
        public bool usingMaterialSettings;
        public MeshRenderer TargetMaterial;
        public Material NormalMaterial;
        public Material HighlightMaterial;
        bool isHover; 

        [Header("GUI Settings")]
        public bool usingGUISettings;
        public VarString TextCaption;
        public GUISkin gUISkin;
        public Vector2 gUISize;
        public Vector2 gUIOffset;

        [Header("Events Settings")]
        public bool usingEventSettings;
        public UnityEvent HoverEvent;
        public UnityEvent ExitEvent;

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
            if (usingMaterialSettings)
            {
                TargetMaterial.GetComponent<Renderer>().material = NormalMaterial;
            }
            if (usingEventSettings)
            {
                ExitEvent.Invoke();
            }
        }

        void OnMouseOver()
        {
            
            isHover = true;
            if (usingMaterialSettings)
            {
                TargetMaterial.GetComponent<Renderer>().material = HighlightMaterial;
            }
            if (usingEventSettings)
            {
                HoverEvent.Invoke();
            }
        }

        void OnGUI()
        {
            if (isHover)
            {
                if (usingGUISettings)
                {
                    GUI.skin = gUISkin;
                    GUI.Box(new Rect(1 + Input.mousePosition.x + gUIOffset.x, Screen.height - Input.mousePosition.y + gUIOffset.y, gUISize.x, gUISize.y), TextCaption.CurrentValue);
                }
            }
        }
    }
}
