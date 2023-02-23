using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerHealth
{
    public float health;
    public float min;
    public float max;
}

[System.Serializable]
public class PlayerMovementData
{
    public float runSpeedCurrent;
    public RampData runRampData = new RampData();
    public RampData jumpRampData = new RampData();
}

[System.Serializable]
public class RampData
{
    public float baseValue;
    public float rampTime;
    public float rampBonus;
    public float rampDecayRate;
}

[System.Serializable]
public class PlayerInput
{
    public PlayerInputAlloc leftKey = new PlayerInputAlloc();
    public PlayerInputAlloc rightKey = new PlayerInputAlloc();
    public PlayerInputAlloc jumpKey = new PlayerInputAlloc();
    public PlayerInputAlloc shootKey = new PlayerInputAlloc();
    public PlayerInputAlloc missileKey = new PlayerInputAlloc();
}

[System.Serializable]
public class PlayerInputAlloc
{
    public KeyCode keyA, keyB;

    public bool Key()
    {
        if (Input.GetKey(keyA))
        {
            return true;
        }

        if (Input.GetKey(keyB))
        {
            return true;
        }

        return false;
    }

    public bool KeyDown()
    {
        if (Input.GetKeyDown(keyA))
        {
            return true;
        }

        if (Input.GetKeyDown(keyB))
        {
            return true;
        }

        return false;
    }

    public bool KeyRelease()
    {
        if (Input.GetKeyUp(keyA))
        {
            return true;
        }

        if (Input.GetKeyUp(keyB))
        {
            return true;
        }

        return false;
    }
}

public class PlayerController : MonoBehaviour
{
    public PlayerHealth health = new PlayerHealth();
    public PlayerMovementData movementData = new PlayerMovementData();
    public PlayerInput input = new PlayerInput();
    PMovementController _pMoveCont;
    PJumpController _pJumpCont;

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
        _pMoveCont = gameObject.AddComponent<PMovementController>();
        _pJumpCont = gameObject.AddComponent<PJumpController>();
    }

    public void Start()
    {
        _pMoveCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );

        _pJumpCont.Setup(
            ref input,
            ref movementData,
            gameObject.GetComponent<Rigidbody2D>()
            );
    }
}

public class PMovementController : MonoBehaviour
{
    PlayerInput _input;
    PlayerMovementData _data;
    Rigidbody2D _rigidbody;

    Vector2 _currentVelocity = Vector2.zero;
    bool _isMoving = false;
    bool _isRamping = false;
    float _currentRunRamp = 0f;

    float RampBonus
    {
        get { return _data.runRampData.rampBonus / _data.runRampData.rampTime; }
    }

    float CurrentRampTime
    {
        get;
        set;
    }

    public void Setup(ref PlayerInput input, ref PlayerMovementData data, Rigidbody2D body2D)
    {
        _rigidbody = body2D;
        _input = input;
        _data = data;
    }

    private void Update()
    {
        _currentVelocity = Vector2.zero;

        //Detect directional inputs.
        if (EvaluateMovingState(_currentVelocity))
            _isMoving = true;
        else
            _isRamping = _isMoving = false;

        if (RampingCheck())
            _isRamping = true;

        if (_isRamping)
        {
            StartCoroutine(BuildRamp());
        }

        _rigidbody.velocity = (new Vector2(CalculateSpeed(), _rigidbody.velocity.y));
    }

    private float GetBaseSpeed()
    {
        float val = 0;

        if (_input.rightKey.Key())
            val = 1;
        if (_input.leftKey.Key())
            val = -1;

        return val;
    }

    private float CalculateSpeed()
    {
        float baseDirection = GetBaseSpeed();
        float additive = (RampBonus * CurrentRampTime);
        return baseDirection * (_data.runRampData.baseValue + additive);
    }

    private IEnumerator BuildRamp()
    {
        while (CurrentRampTime < _data.runRampData.rampTime)
        {
            CurrentRampTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (CurrentRampTime > _data.runRampData.rampTime)
            {
                CurrentRampTime = _data.runRampData.rampTime;
            }
        }
    }

    private bool EvaluateMovingState(Vector2 direction) => (_currentVelocity.x != 0);

    private bool RampingCheck() => (_isMoving && !_isRamping);
}

/// <summary>
/// Class responsible for managing a player jump behaviour. 
/// </summary>
public class PJumpController : MonoBehaviour
{
    [SerializeField] private bool _grounded;

    PlayerInput _input;
    PlayerMovementData _data;
    Rigidbody2D _rigidbody;

    public GroundChecker _groundChecker;
    public float jumpKeyHeldTime;
    public float jumpDistanceCurrent;
    public bool jumpPrepare = false;
    public bool jumping = false;

    private float JumpBonus
    {
        get
        {
            return _data.jumpRampData.rampBonus / _data.jumpRampData.rampTime;
        }
    }

    private float HeldTime
    {
        get { return jumpKeyHeldTime; }
        set
        {
            jumpKeyHeldTime = value;
            if (value > _data.runRampData.rampTime)
            {
                jumpKeyHeldTime = _data.runRampData.rampTime;
            }
        }
    }

    private bool Grounded => _grounded = _groundChecker.Grounded;

    public void Setup(ref PlayerInput input, ref PlayerMovementData data, Rigidbody2D body)
    {
        _input = input;
        _data = data;
        _rigidbody = body;
        _groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        if (!Grounded)
            return; 

        if (_input.jumpKey.KeyDown() && !jumpPrepare)
        {
            jumpPrepare = true;

            StartCoroutine(PressTimer());
        }

        if (CanJump())
        {
            StopCoroutine(PressTimer());
            jumpDistanceCurrent += JumpBonus * HeldTime;
            _rigidbody.AddForce(new Vector2(0, jumpDistanceCurrent), ForceMode2D.Impulse);
            ResetJumpState();
        }
    }

    private IEnumerator PressTimer()
    {
        //Reset values. 
        jumpDistanceCurrent = 0;
        HeldTime = 0;
        jumpDistanceCurrent = _data.jumpRampData.baseValue;

        //Time input.
        while (HeldTime < _data.jumpRampData.rampTime)
        {
            HeldTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public bool CanJump() => (
        _input.jumpKey.KeyRelease() && jumpPrepare && Grounded && !jumping) ? true : false;

    public void ResetJumpState()
    {
        jumpDistanceCurrent = 0;
        jumpPrepare = jumping = false;
    }
}
