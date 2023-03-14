using System.Collections;
using UnityEngine;

public class RoomEnablerSwipSwap : MonoBehaviour
{
    public GameObject hubObj;
    public GameObject clone; 
    public bool startEnabled = false;
    public Collider2D[] enableTriggers;
    public Collider2D[] disableTriggers;

    private void Awake()
    {
        if (startEnabled)
        {
            hubObj.SetActive(true);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            hubObj.SetActive(true);
            SetState(disableTriggers, true);
            SetState(enableTriggers, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            hubObj.SetActive(false);
            SetState(disableTriggers, false);
            SetState(enableTriggers, true);
        }
    }

    private void SetState(Collider2D[] colliders, bool state)
    {
        foreach (Collider2D item in colliders)
        {
            item.enabled = state;
        }
    }
}