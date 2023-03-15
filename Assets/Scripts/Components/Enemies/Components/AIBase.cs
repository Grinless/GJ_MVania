using UnityEngine;

public abstract class AIBase : MonoBehaviour, IEnemyDamage
{
    internal bool firstCollision = true;
    internal bool firstTrigger = true;

    public abstract float Health { get; set; }
    public abstract int Damage { get; }

    public bool Active
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision.gameObject, firstCollision, CollisionIsPlayer(collision.gameObject));
        if(firstCollision)
            firstCollision = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Trigger(collision.gameObject, firstTrigger, CollisionIsPlayer(collision.gameObject));
        if (firstTrigger)
            firstTrigger = false;
    }

    public void ApplyDamage(float value)
    {
        Health -= value;

        if (Health <= 0)
            OnDeath();
    }

    /// <summary>
    /// Modified trigger event notification. 
    /// </summary>
    /// <param name="collisionObj"> The Game Object collided with. </param>
    /// <param name="first"> Whether it was the first trigger event. </param>
    /// <param name="player"> Whether the collided object was the player. </param>
    public abstract void Trigger(GameObject collisionObj, bool first, bool player);

    /// <summary>
    /// Modified collision event notification. 
    /// </summary>
    /// <param name="collisionObj"> The Game Object collided with. </param>
    /// <param name="first"> Whether it was the first collision event. </param>
    /// <param name="player"> Whether the collided object was the player. </param>
    public abstract void Collision(GameObject collisionObj, bool first, bool player);

    public abstract void PlayerDamageEvent(GameObject player);

    internal virtual void OnDeath() => Active = false;

    private bool CollisionIsPlayer(GameObject obj) => obj.layer == 6;
}