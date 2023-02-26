using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteraction();
}

public interface IInteractionResult
{
    void CompleteInteraction();
}

public abstract class Interacter : MonoBehaviour, IInteractable
{
    private const float _timeTillReset = 2.5f;
    private float _currentTime;
    private bool _active;
    internal bool repeatable = true;

    public void OnInteraction()
    {
        if (!_active)
            return; 

        OnInteractionOccured(GetAttachedRecivers());

        if (repeatable)
        {
            StartCoroutine(InteractionReset());
        }

        _active = false;
    }

    internal abstract void OnInteractionOccured(IInteractionResult[] recivers);

    private IInteractionResult[] GetAttachedRecivers()
    {
        IInteractionResult[] recivers =
            gameObject.GetComponentsInParent<IInteractionResult>();

        return recivers;
    }

    private IEnumerator InteractionReset()
    {
        _currentTime = 0;

        while (_currentTime < _timeTillReset)
        {
            _currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _active = true;
    }
}
