using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerInput _input;
    //private Vector3 lastDirection;

    public HeavyWeaponTypeData heavyWeapon;
    public NormalWeaponTypeData normalWeapon;
    public PlayerInputAlloc _inputNormal;
    public PlayerInputAlloc _inputHeavyAttack;
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
        Vector3 direction = PlayerController.instance.LastShotDirection;
        //Vector3 direction = GetAxis();
        direction.z = gameObject.transform.position.z;

        heavyWeaponHandler.UpdateTimer();
        normalWeaponHandler.UpdateTimer();

        //if (direction == Vector3.zero)
        //    direction = lastDirection;

        CheckBulletFire(direction);

        //if (direction != Vector3.zero)
        //{
        //    lastDirection = direction;
        //}
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

    private Vector2 GetAxis()
    {
        if (_input.leftKey.KeyDown() || _input.leftKey.Key())
        {
            return Vector2.left;
        }
        else if (_input.leftKey.KeyDown() || _input.leftKey.Key())
        {
            return Vector2.right;
        }

        return Vector2.right;
    }
}
