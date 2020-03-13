using System;

namespace HephaestusForge
{
    namespace SettingsManagement
    {
        /// <summary>
        /// A collection of settings for a specific setup, need one for each language
        /// </summary>
        [Serializable]
        public class TextSettingsCollection
        {
#if UNITY_EDITOR
            public string _CollectionName = "Default";
#endif

            public TextSettings[] _LanguageTextSettings;

#if TMP_ENABLED
            public TMPTextSettings[] _LanguageTMPTextSettings;
#endif
        }
    }
}