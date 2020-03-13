using HephaestusForge.UnityEventMethodTargeting;
using UnityEngine;
using UnityEngine.Events;

public sealed class OnDestroyed : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField, EventMethodTarget]
    private UnityEvent _onDestroy;

#pragma warning restore 0649

    private void OnDestroy()
    {
        _onDestroy.Invoke();   
    }
}
