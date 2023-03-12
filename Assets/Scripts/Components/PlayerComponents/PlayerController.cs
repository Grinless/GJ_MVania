using System.Collections.Generic;
using UnityEngine;
using AudioByJaime;

public class PlayerController : MonoBehaviour, IPlayerDamage, IPlayerHeal, IWeaponSetter
{
    PlayerMovementController _pMoveCont;
    PlayerJumpController _pJumpCont;

    public PlayerHealth health = new PlayerHealth();
    public IframeManager iframeManager = new IframeManager();
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    public List<HeavyWeaponTypeData> heavyWeapons = new List<HeavyWeaponTypeData>();
    public List<NormalWeaponTypeData> normalWeapons = new List<NormalWeaponTypeData>();

    public static PlayerController instance;

    public PlayerHealth PlayerHealthData
    {
        get { return health; }
    }

    public PlayerMovementData MovementData
    {
        get { return movementData; }
    }

    public PlayerInput Input
    {
        get { return input; }
    }

    public void Awake()
    {
        instance = this;
        _pMoveCont = gameObject.AddComponent<PlayerMovementController>();
        _pJumpCont = gameObject.GetComponent<PlayerJumpController>();
    }

    public void Start()
    {
        _pJumpCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );
    }

    private void Update()
    {
        iframeManager.UpdateIframes();
    }

    public void DamagePlayer(int value)
    {
        print("Applying Damage! " + value);
        health.current -= value;

        //--AJ--
        AudioController.Instance.PlaySound(SoundEffectType.Hurt);

        if (health.current <= 0 && health.currentHealthTanks > 0)
        {
            health.current = health.max;
            health.currentHealthTanks--;
        }

        iframeManager.ActivateIframes();
    }

    #region Health Management.
    void IPlayerDamage.ApplyDamage(int value) => DamagePlayer(value);

    void IPlayerHeal.ApplyFullHealth()
    {
        health.current = health.max;
        health.currentHealthTanks = health.healthTanksUnlocked;
    }

    void IPlayerHeal.ApplyHealth(int value)
    {
        float current = health.current;
        current += value;

        if(current > health.max && health.currentHealthTanks < health.healthTanksUnlocked)
        {
            current -= health.max;
            health.currentHealthTanks++;
        }

        health.current = current;
        
    }

    #endregion

    #region Weapon Unlocking. 
    void IWeaponSetter.SetNormalWeaponActive(int id)
    {
        foreach (NormalWeaponTypeData data in normalWeapons)
        {
            if(data.weaponID == id)
            {
                data.collected = true;
            }
        }
    }

    void IWeaponSetter.SetHeavyWeaponActive(int id)
    {
        foreach (HeavyWeaponTypeData data in heavyWeapons)
        {
            if (data.weaponID == id)
            {
                data.collected = true;
            }
        }
    }
    #endregion
}

public interface IWeaponSetter
{
    void SetNormalWeaponActive(int id);
    void SetHeavyWeaponActive(int id);
}