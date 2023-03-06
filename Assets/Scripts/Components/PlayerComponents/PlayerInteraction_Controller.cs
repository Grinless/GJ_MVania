using UnityEngine;

/// <summary>
/// Component responsible for managing interactions initiated by the player. 
/// </summary>
public class PlayerInteraction_Controller : MonoBehaviour
{
    /// <summary>
    /// A reference to the input manager key allocation for interactions. 
    /// </summary>
    private PlayerInputAlloc _input;

    /// <summary>
    /// The last IInteractable detected by the player. 
    /// </summary>
    private IInteractable _lastInteraction;

    private void Start()
    {
        //Store input reference. 
        _input = gameObject.GetComponent<PlayerController>().input.interactionKey;
    }

    private void Update()
    {
        //Check if there is a stored interactable & whether expected input occured. 
        if (_lastInteraction != null && _input.KeyRelease())
        {
#if DEBUG
            print("Interaction occured.");
#endif
            //Fire the interaction. 
            _lastInteraction.OnInteraction();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable;

        //Check if other object has an IInteractable. 
        if (other != null)
        {
            interactable = other.GetComponent<IInteractable>();
            if(interactable != null)
            {
                //If so store. 
                _lastInteraction = interactable;
            }
        }
        else //Clear the last interaction. 
        {
            _lastInteraction = null; 
        }
    }
}