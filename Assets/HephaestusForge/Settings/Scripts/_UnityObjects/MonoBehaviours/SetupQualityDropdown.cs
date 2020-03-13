using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public sealed class SetupQualityDropdown : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private ReadOnlyQualitySettingValuesReference _qualitySettings;

#pragma warning restore 0649

    private void Awake()
    {
        var dropDown = GetComponent<Dropdown>();
        dropDown.ClearOptions();

        dropDown.AddOptions(QualitySettings.names.ToList());

        dropDown.value = (int)_qualitySettings.Value;
    }
}
