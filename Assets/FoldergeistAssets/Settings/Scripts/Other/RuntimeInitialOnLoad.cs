using System.Reflection;
using UnityEngine;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        public class RuntimeInitialOnLoad
        {
            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
            private static void LoadSettings()
            {
                var settings = (Settings)typeof(Object).GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                     .Invoke(null, new object[] { subjectStateID });

                settings.GetType().GetMethod("Init", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(settings, null);
            }
        }
    }
}
