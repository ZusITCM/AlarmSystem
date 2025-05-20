using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public event Action CrookEntered;
    public event Action CrookExited;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Crook>(out _))
            CrookEntered?.Invoke();
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<Crook>(out _))
            CrookExited?.Invoke();
    }
}