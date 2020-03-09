using FoldergeistAssets.SettingsManagement;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Settings))]

public class SettingsPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        bool enabled = GUI.enabled;
        GUI.enabled = false;
        property.objectReferenceValue = AssetDatabase.LoadAssetAtPath<Settings>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:Settings")[0]));
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = enabled;
    }
}
