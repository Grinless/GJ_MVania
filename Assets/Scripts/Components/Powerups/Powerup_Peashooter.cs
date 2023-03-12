using AudioByJaime;

public class Powerup_Peashooter : PowerupController
{
    public int weaponID; 

    internal override void ApplyPowerUp()
    {
        ((IWeaponSetter)PlayerController.instance).SetNormalWeaponActive(weaponID);

        //--AJ--
        AudioController.Instance.StartAudioCoroutine(AudioCoroutineType.UPGRADE_FANFAIR);
        print("Powerup Application");
        
        gameObject.SetActive(false);
    }
}