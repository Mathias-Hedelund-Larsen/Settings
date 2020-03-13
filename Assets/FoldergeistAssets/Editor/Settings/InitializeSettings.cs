using System.Reflection;
using UnityEditor;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        [InitializeOnLoad]
        public static class InitializeSettings
        {
            static InitializeSettings()
            {
                EditorApplication.playModeStateChanged += LoadSettings;
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
                else if(state == PlayModeStateChange.ExitingPlayMode)
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
