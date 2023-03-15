using UnityEngine;

public class Boss_Hitbox : MonoBehaviour
{
    public Collider2D hitboxCollider;
    public Boss_Mass_Fight bossController; 
    public bool startActive = false; 

    public bool HitboxEnabled
    {
        get => hitboxCollider.enabled;
        set => hitboxCollider.enabled = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            BulletDamageHandler dmgHandler = collision.gameObject.GetComponent<BulletDamageHandler>();
            bossController.ApplyDamage(dmgHandler.Damage);
        }
    }
}