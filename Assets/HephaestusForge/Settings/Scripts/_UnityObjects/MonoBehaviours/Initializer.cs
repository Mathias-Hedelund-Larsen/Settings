using HephaestusForge.UnityEventMethodTargeting;
using UnityEngine;
using UnityEngine.Events;

namespace HephaestusForge
{
    namespace SettingsManagement
    {
        public sealed class Initializer : MonoBehaviour
        {
#pragma warning disable 0649

            [SerializeField, EventMethodTarget]
            private UnityEvent _onAwake;

#pragma warning restore 0649

            private void Awake()
            {

#if UNITY_EDITOR
                Debug.Log("Letting InitializeSettings run the initializations");
#else
                _onAwake.Invoke();
#endif

            }
        }
    }
}
