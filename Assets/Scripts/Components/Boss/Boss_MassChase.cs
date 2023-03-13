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

    private void Awake()
    {
        waypointsSystem.Setup(gameObject);
        data.active = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IPlayerDamage dmg; 
        if (collision.gameObject.layer == 6)
            ((IPlayerDamage) collision.gameObject.GetComponent<PlayerController>()).ApplyDamage(5);
    }

    #region Draw Points
    private void OnDrawGizmosSelected() => waypointsSystem.DrawGizmos();
    #endregion
}