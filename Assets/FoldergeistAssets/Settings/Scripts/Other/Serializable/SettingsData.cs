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
            public float _MasterVol;
            public float _MusicVol;
            public float _SFXVol;
            public Languages _Language = Languages.English;
        }
    }
}