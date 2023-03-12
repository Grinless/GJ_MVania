using System.Collections.Generic;
using UnityEngine;
using AudioByJaime;

public class PlayerController : MonoBehaviour, IPlayerDamage, IPlayerHeal, IWeaponSetter
{
    private PlayerJumpController _pJumpCont;
    public PlayerHealth health = new PlayerHealth();
    public IframeManager iframeManager = new IframeManager();
    public PlayerWorldData worldData = new PlayerWorldData(); 
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    public List<HeavyWeaponTypeData> heavyWeapons = new List<HeavyWeaponTypeData>();
    public List<NormalWeaponTypeData> normalWeapons = new List<NormalWeaponTypeData>();

    public static PlayerController instance;

    Vector2 _lastShotDirection = Vector2.right;

    public Vector2 LastShotDirection
    {
        get
        {
            Vector2 vec = MovementDirection;
            return (vec != Vector2.zero) ? vec : _lastShotDirection;
        }
    }

    public Vector2 MovementDirection
    {
        get
        {
            if (input.rightKey.Key())
                return Vector2.right;
            if (input.leftKey.Key())
                return Vector2.left;

            return Vector2.zero;
        }
    }

    public PlayerHealth PlayerHealthData => health;

    public PlayerMovementData MovementData => movementData;

    public PlayerInput Input => input; 

    public void Awake()
    {
        instance = this;
        gameObject.AddComponent<PlayerMovementController>();
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

    #region Health Management.
    void IPlayerDamage.ApplyDamage(int value)
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

    void IPlayerHeal.ApplyFullHealth()
    {
        health.current = health.max;
        health.currentHealthTanks = health.healthTanksUnlocked;
    }

    void IPlayerHeal.ApplyHealth(int value)
    {
        float current = health.current;
        current += value;

        if (current > health.max && health.currentHealthTanks < health.healthTanksUnlocked)
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
            if (data.weaponID == id)
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

    public bool CheckBeamUnlockedByID(int id)
    {
        foreach (NormalWeaponTypeData item in normalWeapons)
        {
            if(item.weaponID == id && item.collected)
                return true;
        }
        return false;
    }
    #endregion
}

public interface IWeaponSetter
{
    void SetNormalWeaponActive(int id);
    void SetHeavyWeaponActive(int id);
}