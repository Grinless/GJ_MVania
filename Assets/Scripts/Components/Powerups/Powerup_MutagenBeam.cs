using AudioByJaime;
using System.Collections;
using UnityEngine; 

public class Powerup_MutagenBeam : PowerupController
{
    public int weaponID;
    public DialogueDisplay dialogueDisplay;
    public GameObject massChaseStartup;
    public AlarmActivation alarmActivation;

    internal override void ApplyPowerUp()
    {
        ///Set the weapon active within the player data. 
        ((IWeaponSetter)PlayerController.instance).SetNormalWeaponActive(weaponID);

        ///Play the upgraded Fanfair. 
        //--AJ--
        AudioController.Instance.StartAudioCoroutine(AudioCoroutineType.UPGRADE_FANFAIR);

        ///Set the player immobile. 
        PlayerController.instance.PausePlayer(true);

        dialogueDisplay.ShowDialogue("WEAPON COLLECTED: MUTAGEN BEAM. \n PRESS SHIFT + DIRECTION TO FIRE.", true, 0.8f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(DisplayTimer()); 
    }

    private IEnumerator DisplayTimer()
    {
        print("Beginning timer!");
        while (dialogueDisplay.Completing)
        {
            yield return new WaitForEndOfFrame();
        }

        print("dialogue displayed.");
        dialogueDisplay.HideDialogue();
        
        PlayerController.instance.PausePlayer(false);
        massChaseStartup.SetActive(true);
        alarmActivation.EnableAlarms();
        Destroy(gameObject);
    }
}