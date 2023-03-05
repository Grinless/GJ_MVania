using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class HeavyWeaponTypeData : WeaponTypeData
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletSize;
    public float cooldown;

    protected override float GetCooldown() => cooldown;

    protected override float GetVelocity() => bulletSpeed;
}
