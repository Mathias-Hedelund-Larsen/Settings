using System;
using UnityEngine;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        /// <summary>
        /// This will be used to set the settings of a ui text, the text components should be set up in the SettingsManager in Resources
        /// </summary>
        [Serializable]
        public class TextSettings
        {
            public Languages _Language;
            public Font _TextFont;
            public FontStyle _FontStyle = FontStyle.Normal;
            public int _FontSize = 15;
            public float _LineSpacing = 1;
            public bool _SupportRichText = true;
            public TextAnchor _TextAnchor = TextAnchor.MiddleCenter;
            public bool _AlignByGeometry = false;
            public HorizontalWrapMode _Horizontal = HorizontalWrapMode.Overflow;
            public VerticalWrapMode _Vertical = VerticalWrapMode.Overflow;
            public Color _Color = Color.black;

            public TextSettings Clone()
            {
                return (TextSettings)MemberwiseClone();
            }
        }
    }
}