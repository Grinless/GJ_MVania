using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioByJaime;

public class PowerupRoomEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ///Enable the Upgrade room audio. 
        //--AJ--
        AudioController.Instance.EnterUpgradeRoom();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ///Disable the Upgrade room audio. 
        //--AJ--
        AudioController.Instance.ExitUpgradeRoom();
    }
}
