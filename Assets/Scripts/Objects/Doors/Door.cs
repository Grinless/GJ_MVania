using System.Collections;
using System.Collections.Generic;
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
    public GameObject collision;

    public List<IRoomAccess> roomInit = new List<IRoomAccess>(); 
    
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
            collision.SetActive(false); 
        }

        if (doorType == DoorKeyType.PLAYER)
        {
            if (collision.gameObject.layer == 6)
                collision.SetActive(false);
        }
    }
}

public interface IRoomAccess
{
    void AwakenRoom();

    void DeactivateRoomInstance();

    void PrepareRoomInstance();
}

public class RoomHandler : MonoBehaviour, IRoomAccess
{
    public GameObject roomParent; 
    private GameObject _currentCopy;
    private bool _resetRoom = false;

    private bool RoomActive
    {
        get { return _currentCopy.activeSelf; }
        set { _currentCopy.SetActive(value); }
    }

    public bool ResetRoom
    {
        get { return _resetRoom; }
        set { _resetRoom = value; }
    }

    void IRoomAccess.PrepareRoomInstance()
    {
        if (_currentCopy == null || _resetRoom)
        {
            GameObject.Destroy(_currentCopy);
            _currentCopy = Instantiate(roomParent);
        }

    }

    void IRoomAccess.DeactivateRoomInstance()
    {
        RoomActive = false;
    }

    void IRoomAccess.AwakenRoom()
    {
        RoomActive = true;
    }
    
}
