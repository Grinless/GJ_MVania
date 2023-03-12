using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision!!!");
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Player Collision!!!");
            ApplyPowerUp();
        }
    }

    internal abstract void ApplyPowerUp();
}
