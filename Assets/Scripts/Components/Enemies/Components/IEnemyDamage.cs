using UnityEngine;

public interface IEnemyDamage
{
    void ApplyDamage(float value);
}

public abstract class AIBase :MonoBehaviour, IEnemyDamage
{
    internal bool dieOnContact = false;
    internal bool dieOnGroundContact = false; 

    public abstract float Health { get; set; }
    public abstract int Damage { get; }

    public bool Active { 
        get =>  gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 && dieOnContact)
        {
            gameObject.GetComponent<IPlayerDamage>().ApplyDamage(Damage);
            if (dieOnContact)
                Active = false;
        } 

        if(collision.gameObject.layer == 8)
        {
            if (dieOnGroundContact)
                Active = false;
        }
    }

    public void ApplyDamage(float value)
    {
        Health -= value;

        if (Health <= 0)
            Active = false;
    }
}