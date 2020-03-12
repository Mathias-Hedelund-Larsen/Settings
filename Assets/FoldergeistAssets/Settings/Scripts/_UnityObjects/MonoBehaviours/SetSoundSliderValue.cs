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
            private ReadOnlyFloatReference _settingsVolume;
#pragma warning restore 0649

            private void Awake()
            {
                GetComponent<Slider>().value = _settingsVolume;
            }
        }
    }
}
