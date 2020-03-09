using UnityEditor;
using UnityEngine;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// This class is used to draw the settings manager
        /// </summary>
        [CustomEditor(typeof(Settings))]
        public class SettingsInspector : Editor
        {
            public const string TMP_ENABLED = "TMP_ENABLED";
            
            //The script reference
            private MonoScript _monoScript;

            //The target as a serialized object
            private SerializedObject _target;

            /// <summary>
            /// Called by unity when the settinga manager object is inspected, here the initial references are made
            /// </summary>
            private void OnEnable()
            {
                _target = new SerializedObject(target);
                _monoScript = MonoScript.FromScriptableObject((Settings)target);
            }

            /// <summary>
            /// Called by unity when the inspector is drawn
            /// </summary>
            public override void OnInspectorGUI()
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("Script", _monoScript, typeof(MonoScript), false);
                GUI.enabled = true;

                EditorGUI.BeginChangeCheck();

                SerializedProperty enabled = _target.FindProperty("_onlyForInspectorTMPEnabled");

                var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

                enabled.boolValue = defines.Contains(TMP_ENABLED);

                enabled.boolValue = EditorGUILayout.Toggle("Enable TMP", enabled.boolValue);

                if (EditorGUI.EndChangeCheck())
                {
                    if (enabled.boolValue)
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, $"{defines};{TMP_ENABLED}");
                    }
                    else
                    {
                        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, $"{defines.Replace(TMP_ENABLED, string.Empty)}");
                    }
                }

                EditorGUI.BeginChangeCheck();

                var settings = _target.FindProperty("_settings");
                EditorGUILayout.PropertyField(settings, true);

                var defaultSettings = _target.FindProperty("_textSettings");
                EditorGUILayout.PropertyField(defaultSettings, true);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                }

                _target.ApplyModifiedProperties();

                if (GUILayout.Button("Open folder for Settings file"))
                {
                    System.Diagnostics.Process.Start(Application.persistentDataPath);
                }
            }
        }
    }
}