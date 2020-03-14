using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HephaestusForge
{
    namespace SettingsManagement
    {
        [InitializeOnLoad]
        public static class InitializeSettings
        {
            private static string[] _enumNames;

            static InitializeSettings()
            {
                EditorApplication.playModeStateChanged += LoadSettings;

                _enumNames = Enum.GetNames(typeof(QualitySettingValues));

                EditorApplication.update -= CheckQualitySettings;
                EditorApplication.update += CheckQualitySettings;
            }

            private static void CheckQualitySettings()
            {
                var names = QualitySettings.names.Select(n => n.Replace(" ", "")).ToArray();
                if (!_enumNames.SequenceEqual(names))
                {
                    using (StreamWriter writer = new StreamWriter($"{Application.dataPath}/HephaestusForge/Settings/Scripts/Other/Serializable/Enums/QualitySettingValues.cs"))
                    {
                        writer.WriteLine("public enum QualitySettingValues");
                        writer.WriteLine("{");

                        for (int i = 0; i < QualitySettings.names.Length; i++)
                        {
                            writer.WriteLine($"    {QualitySettings.names[i]} = {i},");
                        }

                        writer.WriteLine("}");
                    }
                }
            }

            private static void LoadSettings(PlayModeStateChange state)
            {
                if (state == PlayModeStateChange.ExitingEditMode)
                {
                    var settings = AssetDatabase.LoadAssetAtPath<Settings>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:Settings")[0]));

                    if (settings)
                    {
                        settings.GetType().GetMethod("Init", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(settings, null);
                    }
                }
                else if(state == PlayModeStateChange.EnteredEditMode)
                {
                    var settings = AssetDatabase.LoadAssetAtPath<Settings>(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:Settings")[0]));

                    if (settings)
                    {
                        settings.GetType().GetMethod("ResetSettingsData", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(settings, null);
                    }                    
                }
            }
        }
    }
}
