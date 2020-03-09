using UnityEngine;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// The settings class, only with languages currently, you can add other settings to this class like sound and so on
        /// </summary>
        [System.Serializable]
        public class SettingsData
        {
            public Languages _Language = Languages.English;

            [SerializeField, HideInInspector]
            private string[] _availableLanguages;
        }
    }
}