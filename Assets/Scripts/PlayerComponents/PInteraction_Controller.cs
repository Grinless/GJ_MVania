using UnityEngine;

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
            print("Interaction occured.");
            lastInteraction.OnInteraction();
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