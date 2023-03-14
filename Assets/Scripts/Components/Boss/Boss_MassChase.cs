using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss_MassData
{
    public float health;
    public float speed;
    public float smoothing = 0.01f;
    public bool killable;
    public bool active;
}

public class Boss_MassChase : MonoBehaviour
{
    public WaypointSystem waypointsSystem;
    public Boss_MassData data;
    public Rigidbody2D body;
    public LayerMask layerMask = new LayerMask();

    private void Awake()
    {
        waypointsSystem.Setup(gameObject);
        data.active = true;
        body.includeLayers = layerMask;
    }

    private void Update()
    {
        if (data.active)
            waypointsSystem.UpdateWaypoints(gameObject.transform.position);
    }

    private void LateUpdate()
    {
        if (data.active)
        {
            waypointsSystem.LateUpdate(gameObject, data.speed);
        }
        if(waypointsSystem.pathEnded)
            gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D trigger)
    {
        IPlayerDamage dmg;
        if (trigger.gameObject.layer == 6)
        {
            ((IPlayerDamage)trigger.gameObject.GetComponent<PlayerController>()).ApplyDamage(5);
            trigger.gameObject.GetComponent<PlayerController>().ApplyForce(Vector2.right, 30, 10);
        }
    }

    #region Draw Points
    private void OnDrawGizmosSelected() => waypointsSystem.DrawGizmos();
    #endregion
}