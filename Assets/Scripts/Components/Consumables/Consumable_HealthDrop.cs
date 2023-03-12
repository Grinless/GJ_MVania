using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine; 


public enum HealthRefilSize : int
{
    SMALL = 10, 
    MEDIUM = 25, 
    LARGE = 50
}

public class Consumable_HealthDrop : MonoBehaviour 
{
    public HealthRefilSize healthRefilSize = HealthRefilSize.SMALL;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            ((IPlayerHeal)PlayerController.instance).ApplyHealth((int)healthRefilSize);
            gameObject.SetActive(false);
        }
    }
}
