﻿using System.Collections;
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
    [SerializeField] private bool active = false;

    public override float Health
    {
        get => data.health;
        set => data.health = value;
    }

    public override int Damage => data.damage;

    void Update()
    {
        if (!active)
            return;

        data.body2D.velocity = data.batSpeed * Time.deltaTime * Vector2.down;
    }

    public override void PlayerDamageEvent(GameObject player)
    {
        player.GetComponent<IPlayerDamage>().ApplyDamage(Damage);
        Active = false;
    }

    public override void Trigger(GameObject collisionObj, bool first, bool player)
    {
        if (first && player)
        {
            active = true;
            data.body2D.gravityScale = 1;

            //--AJ--
            AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.BatSwoop);
        }
    }

    public override void Collision(GameObject collisionObj, bool first, bool player)
    {
        if(player)
            PlayerDamageEvent(collisionObj);
    }

    internal override void OnDeath()
    {
        //--AJ--
        AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.BatDie);
        base.OnDeath();
    }
}