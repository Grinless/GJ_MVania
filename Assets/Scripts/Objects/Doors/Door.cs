using System.Collections;
using UnityEngine;

public enum DoorKeyType
{
    NONE, 
    PLAYER
}

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    MultiRayDetector _detector;
    public DoorKeyType doorType = DoorKeyType.NONE; 
    public BoxCollider2D collision; 
    private void Start()
    {
        _detector = gameObject.GetComponent<MultiRayDetector>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResolveDoorOpening();
    }

    private void ResolveDoorOpening()
    {
        if(doorType == DoorKeyType.NONE)
        {
            collision.enabled = false; 
        }

        if (doorType == DoorKeyType.PLAYER)
        {
            if (collision.gameObject.layer == 6)
                collision.enabled = false;
        }
    }
}


