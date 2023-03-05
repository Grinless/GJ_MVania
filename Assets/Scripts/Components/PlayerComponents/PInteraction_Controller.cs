using UnityEngine;

/// <summary>
/// Component responsible for managing interactions initiated by the player. 
/// </summary>
public class PInteraction_Controller : MonoBehaviour
{
    private PlayerInputAlloc _input;
    private IInteractable lastInteraction;


    private void Start()
    {
        _input = gameObject.GetComponent<PlayerController>().input.interactionKey;
    }

    private void Update()
    {
        if (lastInteraction != null && _input.KeyRelease())
        {
#if DEBUG
            print("Interaction occured.");
            lastInteraction.OnInteraction();
#endif
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable;

        if (other != null)
        {
            interactable = other.GetComponent<IInteractable>();
            if(interactable != null)
            {
                lastInteraction = interactable;
            }
        }
        else
        {
            lastInteraction = null; 
        }
    }
}