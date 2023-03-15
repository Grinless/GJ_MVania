using System.Collections;
using UnityEngine;

public class BulletDamageHandler : MonoBehaviour
{
    public float damage; 

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IEnemyDamage interf = collision.gameObject.GetComponent<IEnemyDamage>();

        if (interf != null)
        {
            interf.ApplyDamage(damage);
        }
    }
}
