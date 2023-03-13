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
    internal bool repeatable = true;

    public void OnInteraction() => OnInteractionOccured(GetAttachedRecivers());

    internal abstract void OnInteractionOccured(IInteractionResult[] recivers);

    private IInteractionResult[] GetAttachedRecivers()
    {
        IInteractionResult[] recivers =
            gameObject.GetComponentsInParent<IInteractionResult>();

        return recivers;
    }
}
