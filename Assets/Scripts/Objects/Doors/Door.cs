using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorKeyType
{
    NONE,
    PLAYER
}

public enum DoorState
{
    LOCKED,
    OPEN
}

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    MultiRayDetector _detector;
    private DoorState _state = DoorState.LOCKED;
    public DoorKeyType doorType = DoorKeyType.NONE;
    public GameObject collision;
    public BoxCollider2D entryDetection;
    public BoxCollider2D exitDetection;
    public BoxCollider2D exitDetection2;

    public List<IRoomAccess> roomInit = new List<IRoomAccess>();

    private bool SetEntryTriggerState
    {
        set => entryDetection.enabled = value;
        get => entryDetection.enabled;
    }

    private bool SetExitTriggerState {
        set => exitDetection.enabled = exitDetection2.enabled = value;
    }


    private void SwapTriggerState()
    {
        bool state = SetEntryTriggerState;

        SetEntryTriggerState = !state;
        SetExitTriggerState = state;
    }

    private void Start()
    {
        _detector = gameObject.GetComponent<MultiRayDetector>();
        SetExitTriggerState = false;
        SetEntryTriggerState = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_state == DoorState.LOCKED)
            ResolveDoorOpening(collision);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (_state == DoorState.OPEN)
                ResetDoor();
        }
    }

    private void ResolveDoorOpening(Collider2D collision)
    {

        if (doorType == DoorKeyType.NONE)
            OpenDoor();

        if (doorType == DoorKeyType.PLAYER)
        {
            if (collision.gameObject.layer == 6)
                OpenDoor();
        }
    }

    private void OpenDoor()
    {
        //--AJ--
        AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.DoorOpen);
        collision.SetActive(false);
        SwapTriggerState();
        _state = DoorState.OPEN;
    }

    private void ResetDoor()
    {
        //--AJ--
        AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.DoorClose);
        collision.SetActive(true);
        _state = DoorState.LOCKED;
        SwapTriggerState();
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
