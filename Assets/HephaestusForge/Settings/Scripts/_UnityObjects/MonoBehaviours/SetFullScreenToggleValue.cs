using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public sealed class SetFullScreenToggleValue : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private ReadOnlyBooleanReference _fullScreen;

#pragma warning restore 0649

    private void Awake()
    {
        GetComponent<Toggle>().isOn = _fullScreen;
    }
}
