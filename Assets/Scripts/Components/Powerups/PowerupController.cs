using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ApplyPowerUp();
        }
    }

    internal abstract void ApplyPowerUp();
}
