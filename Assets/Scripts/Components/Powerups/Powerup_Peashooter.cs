public class Powerup_Peashooter : PowerupController
{
    public int weaponID; 

    internal override void ApplyPowerUp()
    {
        ((IWeaponSetter)PlayerController.instance).SetNormalWeaponActive(weaponID);
        gameObject.SetActive(false);
    }
}