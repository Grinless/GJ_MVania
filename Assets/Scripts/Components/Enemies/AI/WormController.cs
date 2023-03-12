using System.Collections;
using UnityEngine;


/// <summary>
/// Enum used for tracking FSM states. 
/// </summary>
public enum WormState
{
    PATROL,
    CHASE
}

/// <summary>
/// Worm data class. 
/// </summary>
[System.Serializable]
public class WormData
{
    public float health;
    public int damage;
    public float speed;
    public float enragedSpeed;
    public float agressionTime;
    public WormState state = WormState.PATROL;
    public LayerMask layerMask = new LayerMask();
}

/// <summary>
/// Controller utilizing FSM behaviour. 
/// </summary>
public class WormController : AIBase
{
    [SerializeField] private Rigidbody2D _body2D;
    public WormData data = new WormData();
    private Vector2 _left = Vector2.left;
    private Vector2 _right = Vector2.right;
    private Vector2 _currentDir = Vector2.left;
    private float _currentEnrageTimer = 0;
    public bool initalRight = false;

    int currentBreak = 0;
    int remainingStumbleFrames = 0;
    const float stumbleSpeedReduction = 40f; 
    const int STUMBLE_FRAMES = 30;
    const int STUMBLE_BREAK = 280;

    public override float Health
    {
        get => data.health;
        set => data.health = value;
    }

    public override int Damage
    {
        get => data.damage;
    }

    void Start()
    {
        _body2D = gameObject.GetComponent<Rigidbody2D>();
        if (initalRight)
            _currentDir = _right;
    }

    void Update()
    {
        UpdateTimer();
        UpdateState();
    }

    private void UpdateState()
    {
        _body2D.velocity = Vector2.zero;
        bool collidedWithPlayer = GroundCheck(6);

        //Update the facing direction of the entity. 
        _currentDir = UpdateDirection(_currentDir, GroundCheck(8));

        //Update the FSM if the player is encountered. 
        if (collidedWithPlayer)
        {
            Debug.Log("Collision with player occured.");
            //Update the FSM's current state. 
            data.state = WormState.CHASE;

            //Set the timer. 
            _currentEnrageTimer = data.agressionTime;
        }

        //Update the set state. 
        if (data.state == WormState.PATROL)
            State_Patrol();
        if (data.state == WormState.CHASE)
            State_Chase();
    }

    private void UpdateTimer()
    {
        //Update the current timer. 
        if (_currentEnrageTimer > 0)
            _currentEnrageTimer -= Time.deltaTime;

        if (_currentEnrageTimer <= 0)
            data.state = WormState.PATROL; //If the timer is reset... 
    }

    /// <summary>
    /// Function used to generate random worm stumble chance. 
    /// </summary>
    /// <returns></returns>
    private bool RandStumble()
    {
        float randStumbleChance = UnityEngine.Random.Range(-50, 50);

        //If the sum is less than 0.
        if (randStumbleChance < 0)
        {
            return true; //Stumble. 
        }
        return false;
    }

    #region Stumble Calculation Methods. 
    private bool CalculateStumble()
    {
        if (currentBreak <= 0 && currentBreak <=0 && RandStumble())
        {
            currentBreak = STUMBLE_BREAK; //Generate stumble. 
            remainingStumbleFrames = STUMBLE_FRAMES;
            return true;
        }
        else if (currentBreak > 0)
            currentBreak--;
        return false;
    }
    #endregion

    private void State_Patrol()
    {
        if (CalculateStumble())
        {
            //--AJ--
            AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.WormMove);
            return;
        }
        else
        {
            if (remainingStumbleFrames > 0)
            {
                _body2D.velocity = _currentDir * (data.speed - stumbleSpeedReduction) * Time.deltaTime;
                remainingStumbleFrames--;
            }
            else
            {
                _body2D.velocity = _currentDir * data.speed * Time.deltaTime;

            }
        }
    }

    private void State_Chase() => _body2D.velocity = _currentDir * data.enragedSpeed * Time.deltaTime;

    private Vector2 UpdateDirection(Vector2 currentDirection, bool groundCollision)
    {
        if (groundCollision)
            return (currentDirection == _left) ? _right : _left;

        return currentDirection;
    }

    RaycastHit2D hit2D;

    private Vector2 StartPos => gameObject.transform.position;

    private Vector2 EndPos => _currentDir.normalized;

    private bool GroundCheck(int layerToCheckFor)
    {
        hit2D = Physics2D.Raycast(StartPos, EndPos, 1, data.layerMask);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.gameObject.layer == layerToCheckFor)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(StartPos, EndPos * 1);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hit2D.point, 0.2f);
    }

    public override void Trigger(GameObject collisionObj, bool first, bool player)
    {

    }

    public override void Collision(GameObject collisionObj, bool first, bool player)
    {
        if (player)
            PlayerDamageEvent(collisionObj);
    }

    public override void PlayerDamageEvent(GameObject player)
    {
        print("Worm Hit!!!");
        player.GetComponent<IPlayerDamage>().ApplyDamage(Damage);
    }

    internal override void OnDeath()
    {
        DropOnDeath dod;

        //--AJ--
        AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.WormDie);
        if ((dod = GetComponent<DropOnDeath>()) != null)
        {
            dod.Activate();
        }
        base.OnDeath();
    }
}