using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera;
    private RoomBoundary _lastRoomBoundary;
    [SerializeField] private RoomBoundary _currentRoomBoundary;
    public GameObject playerInstance;

    public bool withinBoundary = false;

    public static CameraController Instance
    {
        get;
        private set;
    }

    public Vector2 GetPosition
    {
        get { return camera.transform.position; }
    }

    public RoomBoundary RoomBoundary
    {
        set => _currentRoomBoundary = value;
    }

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        if (playerInstance == null)
        {
            print("Player instance missing from camera controller. ");
            this.enabled = false;
        }
    }

    public void Update()
    {
        Bounds bounds;
        Vector2 playerPosition;
        Vector3 cameraPos;

        //Check bounds are assigned. 
        if (_currentRoomBoundary == null)
            return;

        //Set refs.
        bounds = _currentRoomBoundary.GetRoomBounds;
        playerPosition = playerInstance.transform.position;
        cameraPos = camera.transform.position;

        withinBoundary = CheckPlayerPosition(playerPosition, bounds);

        //Update the camerea position.
        if (withinBoundary)
        {
            camera.transform.position = UpdateCamera(playerPosition, cameraPos, bounds);
        }
    }

    public Vector3 UpdateCamera(Vector2 playerPosition, Vector3 cameraPosition, Bounds roomBounds)
    {
        return
            new Vector3(
                Mathf.Clamp(playerPosition.x, roomBounds.min.x, roomBounds.max.x),
                Mathf.Clamp(playerPosition.y, roomBounds.min.y, roomBounds.max.y),
                cameraPosition.z
            );
    }

    private bool CheckPlayerPosition(Vector2 playerPosition, Bounds roomBounds)
    {

        if (playerPosition.x < roomBounds.max.x &&
            playerPosition.x > roomBounds.min.x &&
            playerPosition.y < roomBounds.max.y &&
            playerPosition.y > roomBounds.min.y)
        {
            return true;
        }

        return false;
    }
}
