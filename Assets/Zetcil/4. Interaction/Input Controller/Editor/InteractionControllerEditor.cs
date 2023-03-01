using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Zetcil
{
    [CustomEditor(typeof(InteractionController)), CanEditMultipleObjects]
    public class InteractionControllerEditor : Editor
    {

        public SerializedProperty
            isEnabled,
            usingPositionVector,
            PositionVector,
            usingRotationVector,
            RotationVector,
            usingKeyboard,
            Keyboard,
            usingMouse,
            Mouse,
            usingTouch,
            Touch,
            usingHorizontal,
            Horizontal,
            usingVertical,
            Vertical,
            usingAnalogPad,
            AnalogPad,
            usingDirectionPad,
            DirectionPad,
            usingSteering,
            Steering,
            usingJumpButton,
            JumpButton,
            usingActionButton,
            ActionButton
            ;

        void OnEnable()
        {
            // Setup the SerializedProperties
            isEnabled = serializedObject.FindProperty("isEnabled");
            usingPositionVector = serializedObject.FindProperty("usingPositionVector");
            PositionVector = serializedObject.FindProperty("PositionVector");
            usingRotationVector = serializedObject.FindProperty("usingRotationVector");
            RotationVector = serializedObject.FindProperty("RotationVector");
            usingKeyboard = serializedObject.FindProperty("usingKeyboard");
            Keyboard = serializedObject.FindProperty("Keyboard");
            usingMouse = serializedObject.FindProperty("usingMouse");
            Mouse = serializedObject.FindProperty("Mouse");
            usingTouch = serializedObject.FindProperty("usingTouch");
            Touch = serializedObject.FindProperty("Touch");
            usingHorizontal = serializedObject.FindProperty("usingHorizontal");
            Horizontal = serializedObject.FindProperty("Horizontal");
            usingVertical = serializedObject.FindProperty("usingVertical");
            Vertical = serializedObject.FindProperty("Vertical");
            usingAnalogPad = serializedObject.FindProperty("usingAnalogPad");
            AnalogPad = serializedObject.FindProperty("AnalogPad");
            usingDirectionPad = serializedObject.FindProperty("usingDirectionPad");
            DirectionPad = serializedObject.FindProperty("DirectionPad");
            usingSteering = serializedObject.FindProperty("usingSteering");
            Steering = serializedObject.FindProperty("Steering");
            usingJumpButton = serializedObject.FindProperty("usingJumpButton");
            JumpButton = serializedObject.FindProperty("JumpButton");
            usingActionButton = serializedObject.FindProperty("usingActionButton");
            ActionButton = serializedObject.FindProperty("ActionButton");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(isEnabled);

            if (isEnabled.boolValue)
            {

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.LabelField("INPUT SECTION");
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                EditorGUILayout.PropertyField(usingKeyboard, true);
                if (usingKeyboard.boolValue)
                {
                    EditorGUILayout.PropertyField(Keyboard, true);
                }

                EditorGUILayout.PropertyField(usingMouse, true);
                if (usingMouse.boolValue)
                {
                    EditorGUILayout.PropertyField(Mouse, true);
                }

                EditorGUILayout.PropertyField(usingTouch, true);
                if (usingTouch.boolValue)
                {
                    EditorGUILayout.PropertyField(Touch, true);
                }

                EditorGUILayout.PropertyField(usingHorizontal, true);
                if (usingHorizontal.boolValue)
                {
                    EditorGUILayout.PropertyField(Horizontal, true);
                }

                EditorGUILayout.PropertyField(usingVertical, true);
                if (usingVertical.boolValue)
                {
                    EditorGUILayout.PropertyField(Vertical, true);
                }

                EditorGUILayout.PropertyField(usingDirectionPad, true);
                if (usingDirectionPad.boolValue)
                {
                    EditorGUILayout.PropertyField(DirectionPad, true);
                }

                EditorGUILayout.PropertyField(usingJumpButton, true);
                if (usingJumpButton.boolValue)
                {
                    EditorGUILayout.PropertyField(JumpButton, true);
                }

                EditorGUILayout.PropertyField(usingActionButton, true);
                if (usingActionButton.boolValue)
                {
                    EditorGUILayout.PropertyField(ActionButton, true);
                }

                EditorGUILayout.PropertyField(usingAnalogPad, true);
                if (usingAnalogPad.boolValue)
                {
                    EditorGUILayout.PropertyField(AnalogPad, true);
                }

                EditorGUILayout.PropertyField(usingSteering, true);
                if (usingSteering.boolValue)
                {
                    EditorGUILayout.PropertyField(Steering, true);
                }

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                EditorGUILayout.LabelField("OUTPUT SECTION");
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                EditorGUILayout.PropertyField(usingPositionVector, true);
                if (usingPositionVector.boolValue)
                {
                    EditorGUILayout.PropertyField(PositionVector, true);
                    if (PositionVector.objectReferenceValue == null)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }

                EditorGUILayout.PropertyField(usingRotationVector, true);
                if (usingRotationVector.boolValue)
                {
                    EditorGUILayout.PropertyField(RotationVector, true);
                    if (RotationVector.objectReferenceValue == null)
                    {
                        EditorGUILayout.HelpBox("Required Field(s) Null / None", MessageType.Error);
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Prefab Status: Disabled", MessageType.Error);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}