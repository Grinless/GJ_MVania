using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss_MassData
{
    public float health;
    public float speed;
    public float smoothing = 0.01f;
    public float acceptibleDistX = 0.5f;
    public float acceptibleDistY = 0.5f;
    public bool killable;
    public bool active;
}

public class Boss_MassChase : MonoBehaviour
{
    public WaypointSystem waypointsSystem;
    public Boss_MassData data;

    private void Start() => waypointsSystem.Setup(gameObject);

    private void Update() => waypointsSystem.UpdateWaypoints(gameObject.transform.position);

    private void LateUpdate() => waypointsSystem.LateUpdate(gameObject, data.speed);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == 6)
            ((IPlayerDamage)PlayerController.instance).ApplyDamage(5);
    }

    #region Draw Points
    private void OnDrawGizmosSelected() => waypointsSystem.DrawGizmos();
    #endregion
}