using System;

#if TMP_ENABLED

using TMPro;
using UnityEngine;

#endif

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// This will be used to set the settings of a ui text, the text components should be set up in the SettingsManager in Resources
        /// </summary>
        [Serializable]
        public class TMPTextSettings
        {
#if TMP_ENABLED

            public Languages _Language;
            public TMP_FontAsset _TextFont;
            public FontStyles _FontStyle = FontStyles.Normal;
            public int _FontSize = 15;
            public float _LineSpacing = 1;
            public bool _RichText = false;
            public bool _AlignByGeometry = false;
            public bool _RaycastTarget = false;
            public TextOverflowModes _OverflowMode = TextOverflowModes.Truncate;
            public TextAlignmentOptions _TextAnchor = TextAlignmentOptions.Midline;
            public Color _Color = Color.black;

#endif
            public TMPTextSettings Clone()
            {
                return (TMPTextSettings)MemberwiseClone();
            }
        }
    }
}