using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BatData
{
    public int damage; 
    public float batHealth;
    public float batSpeed;
    public BoxCollider2D collisionObject;
    public Rigidbody2D body2D;
}

public class BatController : MonoBehaviour
{

    public BatData data = new BatData();
    public Vector2 trackedPosition;
    [SerializeField]private bool active = false;


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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active)
            return;

        if (collision != null)
        {
            if (collision.gameObject.layer == 6)
            {
                IPlayerDamage damage = collision.gameObject.GetComponent<IPlayerDamage>();
                damage.ApplyDamage(data.damage);
            }

            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(trackedPosition, 0.2f);
    }
}