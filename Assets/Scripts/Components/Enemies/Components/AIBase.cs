using UnityEngine;

public abstract class AIBase : MonoBehaviour, IEnemyDamage
{
    private bool firstCollision = true;
    private bool firstTrigger = true;

    public abstract float Health { get; set; }
    public abstract int Damage { get; }

    public bool Active
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (firstCollision)
        {
            Collision(obj, firstCollision, CollisionIsPlayer(obj));
            firstCollision = false;
        }
        else
            Collision(obj, firstCollision, CollisionIsPlayer(obj));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;

        Trigger(obj, firstTrigger, CollisionIsPlayer(obj));
    }

    public void ApplyDamage(float value)
    {
        Health -= value;

        if (Health <= 0)
            OnDeath();
    }

    public abstract void Trigger(GameObject collisionObj, bool first, bool player);

    public abstract void Collision(GameObject collisionObj, bool first, bool player);

    public abstract void PlayerDamageEvent(GameObject player);

    internal virtual void OnDeath() => Active = false;

    private bool CollisionIsPlayer(GameObject obj) => obj.layer == 6;
}