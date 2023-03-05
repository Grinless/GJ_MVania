using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoundary : MonoBehaviour
{
    private BoxCollider2D _boundsCollider;
    public bool followEnabled = true;
    public bool transitionBoundary = false;
    public float offsetFromPlayY = 0; 

    public Bounds GetRoomBounds
    {
        get => _boundsCollider.bounds; 
    }

    public bool FollowEnabled
    {
        get => followEnabled;
    }

    private void Awake()
    {
        if(_boundsCollider == null)
            _boundsCollider = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            CameraController.Instance.RoomBoundary = this;
        }
    }
}
