using System.Collections;
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
    public void OnInteraction()
    {
        throw new System.NotImplementedException();
    }

    internal abstract void OnInteractionOccured();
}
