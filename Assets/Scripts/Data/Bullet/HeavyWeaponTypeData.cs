using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeavyWeaponTypeData : WeaponTypeData
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletSize;
    public float cooldown;

    protected override float GetCooldown() => cooldown;

    protected override float GetVelocity() => bulletSpeed;
}

public class ConeCaster
{
    private Transform _trans;
    private BatData _data;
    RaycastHit2D hit;

    public ConeCaster(ref BatData data, Transform trans)
    {
        _trans = trans;
        _data = data;
    }

    public void OnGizmosDraw()
    {
        Vector2 init = _trans.position;
        Vector2 centerPos = Vector3.down * 10;

        //Gizmos.DrawLine(_trans.position, centerPos);
        Gizmos.DrawLine(init, hit.point);
    }


    public void CheckCollision()
    {
        Vector2 init = _trans.position;
        Vector2 centerPos = Vector3.down * 10;

        //RaycastHit2D hit = Physics2D.Raycast(init, centerPos.normalized.Scale(10), 10);

        if (hit.collider != null)
        {
            if(hit.collider.gameObject.GetComponent<PlayerController>())
                Debug.Log("Detection hit: " + hit.collider.gameObject.name);
        }
    }
}