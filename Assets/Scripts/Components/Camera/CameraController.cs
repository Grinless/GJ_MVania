using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float TRANSITION_SPEED = 0.15f;

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
        Instance = this;

        if (playerInstance == null)
        {
            print("Player instance missing from camera controller. ");
            this.enabled = false;
        }
    }

    private Vector2 calculatedEntryPoint;
    private Vector2 calculatedTargetPoint;

    public void Update()
    {
        Bounds bounds;
        Vector2 playerPosition;
        Vector3 cameraPos;

        if(playerInstance == null)
        {
            playerInstance = PlayerController.instance.gameObject;
        }

        //Check bounds are assigned. 
        if (_currentRoomBoundary == null)
            return;

        //Set refs.
        bounds = _currentRoomBoundary.GetRoomBounds;
        playerPosition = playerInstance.transform.position;
        cameraPos = camera.transform.position;

        withinBoundary = CheckPlayerPosition(playerPosition, bounds);

        if (_currentRoomBoundary.followEnabled)
        {
            //Update the camerea position.
            if (withinBoundary)
            {
                camera.transform.position = UpdateCamera(playerPosition, cameraPos, bounds);
            }
        }
        else if (!_currentRoomBoundary.followEnabled && !_currentRoomBoundary.transitionBoundary)
        {
            camera.transform.position = UpdateCamera(_currentRoomBoundary.transform.position, cameraPos, bounds);
        }
        else if (_currentRoomBoundary.transitionBoundary)
        {
            camera.transform.position = Vector3.Lerp(cameraPos, UpdateCameraLockedY(playerPosition, cameraPos, bounds), Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        //Check bounds are assigned. 
        if (_currentRoomBoundary == null || playerInstance == null)
            return;

        //Perform camera transition. 
        GetBoundCollisionSide(
            playerInstance.transform.position,
            camera.transform.position,
            _currentRoomBoundary.GetRoomBounds
            );

        if (_currentRoomBoundary.transitionBoundary)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(calculatedEntryPoint, 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(calculatedTargetPoint, 0.5f);
        }
    }

    public void GetBoundCollisionSide(Vector2 playerPosition, Vector2 cameraPos, Bounds bounds)
    {
        if (playerPosition.x > bounds.center.x + bounds.size.x / 2)
        {
            calculatedEntryPoint = new Vector2(bounds.center.x + bounds.size.x / 2, bounds.center.y);
            calculatedTargetPoint = new Vector2(bounds.center.x - bounds.size.x / 2, bounds.center.y);
        }
        else if (playerPosition.x < bounds.center.x - bounds.size.x / 2)
        {
            calculatedEntryPoint = new Vector2(bounds.center.x - bounds.size.x / 2, bounds.center.y);
            calculatedTargetPoint = new Vector2(bounds.center.x + bounds.size.x / 2, bounds.center.y);
        }
    }

    public Vector3 UpdateCameraLockedY(Vector2 playerPosition, Vector3 cameraPos, Bounds bounds)
    {
        return new Vector3(
                Mathf.Clamp(playerPosition.x, bounds.min.x, bounds.max.x),
                cameraPos.y,
                -10
                );
    }

    public Vector3 UpdateCamera(Vector2 playerPosition, Vector3 cameraPosition, Bounds roomBounds)
    {
        return
            new Vector3(
                Mathf.Clamp(playerPosition.x, roomBounds.min.x, roomBounds.max.x),
                Mathf.Clamp(playerPosition.y, roomBounds.min.y, roomBounds.max.y),
                -10
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
