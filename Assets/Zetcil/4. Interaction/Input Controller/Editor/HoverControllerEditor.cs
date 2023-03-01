using UnityEditor;
using UnityEngine;

namespace Zetcil
{
	[CustomEditor(typeof(HoverController)), CanEditMultipleObjects]
	public class HoverControllerEditor : Editor
	{
		public SerializedProperty
		   isEnabled,
		   TextCaption,
		   usingMaterialSettings,
		   TargetMaterial,
		   NormalMaterial,
		   HighlightMaterial,
		   usingGUISettings,
		   gUISkin,
		   gUISize,
		   gUIOffset,
		   usingEventSettings,
		   HoverEvent,
		   ExitEvent
		;

		void OnEnable()

		{
			isEnabled = serializedObject.FindProperty("isEnabled");
			TextCaption = serializedObject.FindProperty("TextCaption");
			usingMaterialSettings = serializedObject.FindProperty("usingMaterialSettings");
			TargetMaterial = serializedObject.FindProperty("TargetMaterial");
			NormalMaterial = serializedObject.FindProperty("NormalMaterial");
			HighlightMaterial = serializedObject.FindProperty("HighlightMaterial");
			usingGUISettings = serializedObject.FindProperty("usingGUISettings");
			gUISkin = serializedObject.FindProperty("gUISkin");
			gUISize = serializedObject.FindProperty("gUISize");
			gUIOffset = serializedObject.FindProperty("gUIOffset");
			usingEventSettings = serializedObject.FindProperty("usingEventSettings");
			HoverEvent = serializedObject.FindProperty("HoverEvent");
			ExitEvent = serializedObject.FindProperty("ExitEvent");
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			EditorGUILayout.PropertyField(isEnabled);
			if (isEnabled.boolValue)
			{
				EditorGUILayout.PropertyField(usingMaterialSettings, true);
				if (usingMaterialSettings.boolValue)
				{
					EditorGUILayout.PropertyField(TargetMaterial, true);
					EditorGUILayout.PropertyField(NormalMaterial, true);
					EditorGUILayout.PropertyField(HighlightMaterial, true);
				}
				EditorGUILayout.PropertyField(usingGUISettings, true);
				if (usingGUISettings.boolValue)
				{
					EditorGUILayout.PropertyField(TextCaption, true);
					EditorGUILayout.PropertyField(gUISkin, true);
					EditorGUILayout.PropertyField(gUISize, true);
					EditorGUILayout.PropertyField(gUIOffset, true);
				}
				EditorGUILayout.PropertyField(usingEventSettings, true);
				if (usingEventSettings.boolValue)
				{
					EditorGUILayout.PropertyField(HoverEvent, true);
					EditorGUILayout.PropertyField(ExitEvent, true);
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