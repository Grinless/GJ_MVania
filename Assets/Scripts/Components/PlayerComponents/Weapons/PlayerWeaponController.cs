using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInput _input;
    public HeavyWeaponTypeData heavyWeapon;
    public NormalWeaponTypeData normalWeapon;
    public WeaponHandler heavyWeaponHandler = new WeaponHandler();
    public WeaponHandler normalWeaponHandler = new WeaponHandler();

    bool HeavyAttack
    {
        get => _input.heavyAttackKey.Key();
    }

    bool NormalAttack
    {
        get => _input.normalAttackKey.Key();
    }

    void Start()
    {
        _input = GetComponent<PlayerController>().Input;
        normalWeapon = GetComponent<PlayerController>().normalWeapons[0];
    }

    void Update()
    {
        if (PlayerController.instance.Paused)
            return; 

        Vector3 direction = PlayerController.instance.LastShotDirection;
        direction.z = gameObject.transform.position.z;

        heavyWeaponHandler.UpdateTimer();
        normalWeaponHandler.UpdateTimer();

        CheckBulletFire(direction);
    }

    private void CheckBulletFire(Vector3 direction)
    {
        float _time;

        if (heavyWeapon != null && HeavyAttack && heavyWeaponHandler.Elapsed)
        {
            ((IFireWeapon)heavyWeapon).FireWeapon(
                            gameObject.transform.position,
                            direction, 
                            heavyWeapon.bulletDamage,
                            out _time);
            heavyWeaponHandler.SetTime(_time);
            return;
        }

        if (normalWeapon != null && NormalAttack && normalWeaponHandler.Elapsed)
        {
            if (!normalWeapon.collected)
                return;

            //--AJ--
            AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Shoot);

            ((IFireWeapon)normalWeapon).FireWeapon(
                gameObject.transform.position,
                direction,
                normalWeapon.bulletDamage,
                out _time);
            normalWeaponHandler.SetTime(_time);
            return;
        }
    }
}
