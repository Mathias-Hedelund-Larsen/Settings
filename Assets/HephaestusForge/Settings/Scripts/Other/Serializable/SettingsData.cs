namespace HephaestusForge
{
    namespace SettingsManagement
    {
        /// <summary>
        /// The settings class, only with languages currently, you can add other settings to this class like sound and so on
        /// </summary>
        [System.Serializable]
        public class SettingsData
        {
            public BooleanReference _FullScreen;
            public FloatReference _MusicVol;
            public FloatReference _MasterVol;
            public FloatReference _SFXVol;
            public SimpleResolutionReference _Resolution;
            public QualitySettingValuesReference _GraphicsQuality;
            public Languages _Language = Languages.English;
        }
    }
}