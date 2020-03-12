using UnityEngine;
using UnityEngine.Audio;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// In this class the settings for language and ui texts are set
        /// </summary>
        public sealed class Settings : ScriptableObject
        {
#pragma warning disable 0649

#if UNITY_EDITOR

            [SerializeField]
            private bool _onlyForInspectorTMPEnabled;

#endif

            [SerializeField]
            private AudioMixer _gameMixer;

            //The active and chosen language
            [SerializeField]
            private SettingsData _settingsData = new SettingsData();

            [SerializeField]
            private TextSettingsCollection[] _textSettings = new TextSettingsCollection[0];                        

#pragma warning restore 0649

            //Event that will be invoked when the chosen language change
            public event OnLanguageChange _OnLanguageChange;

            //Exposing the active language
            public Languages ActiveLanguage { get { return _settingsData._Language; } }

            public float MasterVolume
            {
                get
                {
                    return _settingsData._MasterVol; 
                }
                private set
                {
                    _settingsData._MasterVol.Value = value;
                    _gameMixer.SetFloat("MasterVol", Mathf.Log10(value) * 20);
                }
            }

            public float MusicVolume
            {
                get
                {
                    return _settingsData._MusicVol;
                }
                private set
                {
                    _settingsData._MusicVol.Value = value;
                    _gameMixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
                }
            }

            public float SoundEffectsVolume
            {
                get
                {
                    return _settingsData._SFXVol;
                }
                private set
                {
                    _settingsData._SFXVol.Value += value;
                    _gameMixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
                }
            }

            private void Init()
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                Debug.Log("Settings initializing");
#endif
            }

            /// <summary>
            /// Changing the current language to a new one, then invoking the event
            /// </summary>
            /// <param name="newLanguage">Value of the new language</param>
            public void ChangeLanguage(Languages newLanguage)
            {
                var oldLanguage = _settingsData._Language;
                _settingsData._Language = newLanguage;

                _OnLanguageChange?.Invoke(oldLanguage, newLanguage);
            }            

#if TMP_ENABLED
            /// <summary>
            /// Getting the settings for a ui text by the index
            /// </summary>
            /// <param name="index">The index of the settings</param>
            /// <returns></returns>
            public TMPTextSettings GetSettings(int index)
            {
                TMPTextSettings textSettings = _textSettings[index]._LanguageTMPTextSettings.Find(t => t._Language == Languages.All, out bool succes);

                for (int i = 0; i < _textSettings[index]._LanguageTMPTextSettings.Length; i++)
                {
                    if (_textSettings[index]._LanguageTMPTextSettings[i]._Language == _settings._Language)
                    {
                        return _textSettings[index]._LanguageTMPTextSettings[i];
                    }
                }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (!succes)
                {
                    Debug.LogError("Couldn't find the needed text settings for language: " + _settings._Language);
                }
#endif

                return textSettings;
            }
#else

            /// <summary>
            /// Getting the settings for a ui text by the index
            /// </summary>
            /// <param name="index">The index of the settings</param>
            /// <returns></returns>
            public TextSettings GetSettings(int index)
            {
                TextSettings textSettings = _textSettings[index]._LanguageTextSettings.Find(t => t._Language == Languages.All, out bool succes);

                for (int i = 0; i < _textSettings[index]._LanguageTextSettings.Length; i++)
                {
                    if (_textSettings[index]._LanguageTextSettings[i]._Language == _settingsData._Language)
                    {
                        return _textSettings[index]._LanguageTextSettings[i];
                    }
                }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (!succes)
                {
                    Debug.LogError("Couldn't find the needed text settings for language: " + _settingsData._Language);
                }
#endif

                return textSettings;
            }
#endif

#if UNITY_EDITOR

            [UnityEditor.MenuItem("Assets/Create/FoldergeistAssets/Limited to one/Settings", false, 0)]
            private static void CreateInstance()
            {
                if (UnityEditor.AssetDatabase.FindAssets("t:Settings").Length == 0)
                {
                    var path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);

                    if (path.Length > 0)
                    {
                        if (System.IO.Directory.Exists(path))
                        {
                            UnityEditor.AssetDatabase.CreateAsset(CreateInstance<Settings>(), path + "/Settings.asset");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("An instance of Settings already exists");
                }
            }
#endif
        }
    }
}