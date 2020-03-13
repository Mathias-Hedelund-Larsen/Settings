using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace HephaestusForge
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

            [SerializeField, HideInInspector]
            private string _settingsPath;

            //Event that will be invoked when the chosen language change
            public event OnLanguageChange _OnLanguageChange;

            //Exposing the active language
            public Languages ActiveLanguage { get { return _settingsData._Language; } }

            #region SettingsData

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
            public bool FullScreen
            {
                get
                {
                    return _settingsData._FullScreen;
                }
                private set
                {
                    Screen.fullScreen = value;
                    _settingsData._FullScreen.Value = value;
                }
            }

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
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

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
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

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
            public float SoundEffectsVolume
            {
                get
                {
                    return _settingsData._SFXVol;
                }
                private set
                {
                    _settingsData._SFXVol.Value = value;
                    _gameMixer.SetFloat("SFXVol", Mathf.Log10(value) * 20);
                }
            }

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
            private int ResolutionIndex
            {
                set
                {
                    var resolution = Screen.resolutions[value];

                    Screen.SetResolution(resolution.width, resolution.height, FullScreen);

                    _settingsData._Resolution.Value = (SimpleResolution)resolution;
                }
            }

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
            private int GraphicsQualityIndex
            {
                set
                {
                    QualitySettings.SetQualityLevel(value);
                    _settingsData._GraphicsQuality.Value = (QualitySettingValues)value;
                }
            }
            #endregion

            private void Init()
            {
#if UNITY_EDITOR
                if (!File.Exists($"{Application.persistentDataPath}/EditorSettings.txt"))
                {
                    using (Stream stream = File.Open($"{Application.persistentDataPath}/EditorSettings.txt", FileMode.Create))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                        {
                            var settingsPacket = new Packet();
                            _settingsData._Resolution.Value = (SimpleResolution)Screen.currentResolution;
                            _settingsData._GraphicsQuality.Value = (QualitySettingValues)QualitySettings.GetQualityLevel();
                            settingsPacket.Write(_settingsData);

                            writer.Write(settingsPacket.ToArray());
                        }
                    }                    
                }
#endif
                _settingsPath = $"{Application.persistentDataPath}/Settings.txt";

                if (!File.Exists(_settingsPath))
                {
                    using (Stream stream = File.Open(_settingsPath, FileMode.Create))
                    {
                        using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                        {
                            var settingsPacket = new Packet();
                            _settingsData._FullScreen.Value = Screen.fullScreen;
                            _settingsData._Resolution.Value = (SimpleResolution)Screen.currentResolution;
                            _settingsData._GraphicsQuality.Value = (QualitySettingValues)QualitySettings.GetQualityLevel();                            
                            settingsPacket.Write(_settingsData);

                            writer.Write(settingsPacket.ToArray());
                        }
                    }
                }
                else
                {
                    LoadSettings();
                }
            }

            private void LoadSettings()
            {
                var settingsData = new Packet(File.ReadAllBytes(_settingsPath)).ReadSettingsData();

                FullScreen = settingsData._FullScreen;
                MasterVolume = settingsData._MasterVol;
                MusicVolume = settingsData._MusicVol;
                SoundEffectsVolume = settingsData._SFXVol;

                var resolutionsList = Screen.resolutions.ToList();

                ResolutionIndex = resolutionsList.IndexOf(resolutionsList.Find(r => _settingsData._Resolution.Value == r));
                GraphicsQualityIndex = (int)settingsData._GraphicsQuality.Value;
                ChangeLanguage(settingsData._Language);
            }

            /// <summary>
            /// Called from a UnityEvent in the editor
            /// </summary>
            private void SaveSettings()
            {
                using (Stream stream = File.Open(_settingsPath, FileMode.Create))
                {
                    using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8))
                    {
                        var settingsPacket = new Packet();

                        settingsPacket.Write(_settingsData);

                        writer.Write(settingsPacket.ToArray());
                    }
                }
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

            [UnityEditor.MenuItem("Assets/Create/HephaestusForge/Limited to one/Settings", false, 0)]
            private static void CreateInstance()
            {
                if (UnityEditor.AssetDatabase.FindAssets("t:Settings").Length == 0)
                {
                    var path = UnityEditor.AssetDatabase.GetAssetPath(UnityEditor.Selection.activeObject);

                    if (path.Length > 0)
                    {
                        var obj = CreateInstance<Settings>();                       

                        if (Directory.Exists(path))
                        {
                            UnityEditor.AssetDatabase.CreateAsset(obj, path + "/Settings.asset");

                            return;
                        }

                        var pathSplit = path.Split('/').ToList();
                        pathSplit.RemoveAt(pathSplit.Count - 1);
                        path = string.Join("/", pathSplit);

                        if (Directory.Exists(path))
                        {                            
                            UnityEditor.AssetDatabase.CreateAsset(obj, path + "/Settings.asset");

                            return;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("An instance of Settings already exists");
                }
            }

            private void ResetSettingsData()
            {
                var editorSettings = new Packet(File.ReadAllBytes($"{Application.persistentDataPath}/EditorSettings.txt")).ReadSettingsData();
                FullScreen = editorSettings._FullScreen;
                MasterVolume = editorSettings._MasterVol;
                MusicVolume = editorSettings._MusicVol;
                SoundEffectsVolume = editorSettings._SFXVol;

                var resolutionsList = Screen.resolutions.ToList();

                ResolutionIndex = resolutionsList.IndexOf(resolutionsList.Find(r => _settingsData._Resolution.Value == r));

                GraphicsQualityIndex = (int)editorSettings._GraphicsQuality.Value;
                _settingsData._Language = editorSettings._Language;
            }
#endif
        }
    }
}