using UnityEngine;
using UnityEngine.UI;

namespace FoldergeistAssets
{
    namespace SettingsManagement
    {
        [RequireComponent(typeof(Slider))]
        public sealed class SetSoundSliderValue : MonoBehaviour
        {
#pragma warning disable 0649
            [SerializeField]
            private Settings _settings;
#pragma warning restore 0649

            private void Awake()
            {
                GetComponent<Slider>().value = _settings.GetMasterVolume();
            }
        }
    }
}
