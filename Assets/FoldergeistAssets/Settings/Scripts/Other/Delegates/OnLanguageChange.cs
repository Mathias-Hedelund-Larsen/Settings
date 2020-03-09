namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// Delegate to be invoked when the current language changes
        /// </summary>
        /// <param name="old">The old language of the localizationkit</param>
        /// <param name="current">The currently active language of the localizationkit</param>
        public delegate void OnLanguageChange(Languages old, Languages current);
    }
}