using AudioByJaime;

public class Powerup_Peashooter : PowerupController
{
    public int weaponID;
    public DialogueDisplay dialogueDisplay; 

    internal override void ApplyPowerUp()
    {
        ///Set the weapon active within the player data. 
        ((IWeaponSetter)PlayerController.instance).SetNormalWeaponActive(weaponID);

        ///Play the upgraded Fanfair. 
        //--AJ--
        AudioController.Instance.StartAudioCoroutine(AudioCoroutineType.UPGRADE_FANFAIR);

        dialogueDisplay.ShowDialogue("WEAPON COLLECTED: MUTAGEN BEAM. \n PRESS SHIFT + DIRECTION TO FIRE.", true);
        
        gameObject.SetActive(false);
    }
}