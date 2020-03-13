using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public sealed class SetupResolutionDropdown : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField]
    private ReadOnlySimpleResolutionReference _resolution;

#pragma warning restore 0649

    private void Awake()
    {
        var dropDown = GetComponent<Dropdown>();
        dropDown.ClearOptions();

        int activeResolutionIndex = 0;
        var resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(((SimpleResolution)resolutions[i]).ToString());

            if(_resolution.Value == resolutions[i])
            {
                activeResolutionIndex = i;
            }
        }

        dropDown.AddOptions(options);
        dropDown.value = activeResolutionIndex;
    }
}
