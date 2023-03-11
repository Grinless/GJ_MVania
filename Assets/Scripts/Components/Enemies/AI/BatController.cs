using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatData
{
    public int damage; 
    public float health;
    public float batSpeed;
    public BoxCollider2D collisionObject;
    public Rigidbody2D body2D;
}

public class BatController : AIBase
{

    public BatData data = new BatData();
    public Vector2 trackedPosition;
    [SerializeField]private bool active = false;

    public override float Health {
        get => data.health; 
        set => data.health = value; 
    }
    public override int Damage { 
        get => data.damage; 
    }

    private void Awake()
    {
        dieOnContact = true;  
        dieOnGroundContact = true;
    }


    void Update()
    {
        if (!active)
            return;

        data.body2D.velocity = Vector2.down * data.batSpeed * Time.deltaTime;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered Detection: " + collision.gameObject.name);

        if (collision.gameObject.layer != 0)
        {
            active = true;
            data.body2D.gravityScale = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(trackedPosition, 0.2f);
    }
}