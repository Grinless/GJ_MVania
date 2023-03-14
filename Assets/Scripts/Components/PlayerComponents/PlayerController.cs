using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioByJaime;

public class PlayerController : MonoBehaviour, IPlayerDamage, IPlayerHeal, IWeaponSetter
{
    private PlayerJumpController _pJumpCont;
    private bool _paused = false; 
    public PlayerHealth health = new PlayerHealth();
    public IframeManager iframeManager = new IframeManager();
    public PlayerWorldData worldData = new PlayerWorldData(); 
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    public List<HeavyWeaponTypeData> heavyWeapons = new List<HeavyWeaponTypeData>();
    public List<NormalWeaponTypeData> normalWeapons = new List<NormalWeaponTypeData>();
    public static PlayerController instance;
    public Rigidbody2D body2D;

    #region Properties. 
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

    public bool Paused => _paused; 
    #endregion

    public void Awake()
    {
        instance = this;
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
        if (iframeManager.IframesActive)
            return; 
        health.current -= value;

        PlayHurtSFX();

        if(health.current <= 0)
        {
            if (health.currentHealthTanks > 0)
            {
                health.current = health.max;
                health.currentHealthTanks--;
            }
            else
            {
                //Do death implementation.
                StartCoroutine(Death());
            }
        }

        iframeManager.ActivateIframes();
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
        int currentBuildIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        //TODO: Add death animation. 
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentBuildIndex);
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

    Rigidbody2D bd2D; 

    #region UI Functions. 
    public void PausePlayer(bool state)
    {
        _paused = state;

        if (bd2D == null)
            bd2D = gameObject.GetComponent<Rigidbody2D>();
        bd2D.isKinematic = state;

        if (state)
        {
            bd2D.velocity = Vector2.zero;
            bd2D.angularVelocity = 0; 
        }
    }
    #endregion

    #region Audio Calls
    private void PlayHurtSFX() =>
        AudioController.Instance.PlaySound(SoundEffectType.Hurt); //--AJ--
    #endregion

    public void ApplyForce(Vector2 direction, float amount, int frames)
    {
        StartCoroutine(AddForce(direction, amount, frames));
    }

    private IEnumerator AddForce(Vector2 direction, float amount, int frames)
    {
        int _currentFrame = frames; 

        while(_currentFrame > 0)
        {
            _currentFrame--;
            body2D.velocity += (direction * amount);
            yield return new WaitForEndOfFrame();
        }
    }

}

public interface IWeaponSetter
{
    void SetNormalWeaponActive(int id);
    void SetHeavyWeaponActive(int id);
}