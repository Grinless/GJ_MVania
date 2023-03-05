[System.Serializable]
public class NormalWeaponTypeData : WeaponTypeData
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletSize;
    public float cooldown;

    protected override float GetCooldown() => cooldown;

    protected override float GetVelocity() => bulletSpeed;
}
