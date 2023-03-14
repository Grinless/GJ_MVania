using System.Collections;
using UnityEngine;

/// <summary>
/// Class responsible for managing a player jump behaviour. 
/// </summary>
[RequireComponent(typeof(PlayerGroundChecker))]
public class PlayerJumpController : MonoBehaviour
{
    [SerializeField] private bool _grounded = true;

    PlayerInput _input;
    PlayerMovementData _data;
    Rigidbody2D _rigidbody;

    public PlayerGroundChecker _groundChecker;
    public float _startJumpTime;
    public float _jumpTime;
    public bool jumping = false;
    public bool jumpCancel = false;
    public float maxfallSpeed = 4;
    public float risingGravity = 1.5f;
    public float airTimeGravity = 0f;
    public float fallingGravity = 0.8f;
    public float airTime = 0.2f;

    private float JumpBase
    {
        get => _data.jumpRampData.baseValue;
    }

    private bool Grounded => GroundedCheck();

    public void Setup(ref PlayerInput input, ref PlayerMovementData data, Rigidbody2D body)
    {
        _input = input;
        _data = data;
        _rigidbody = body;
        _groundChecker = GetComponent<PlayerGroundChecker>();
    }

    private bool down;

    private void Update()
    {
        if (PlayerController.instance.Paused)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            down = true;

        if (Input.GetKeyUp(KeyCode.Space))
            down = false;
    }

    bool lastGroundedState;
    bool jumpSFXPlayed = false;

    void FixedUpdate()
    {
        if (PlayerController.instance.Paused)
            return;

        if (down && Grounded && !jumping)
        {
            if (!jumpSFXPlayed)
            {
                PlayJumpSFX();
                jumpSFXPlayed = true;
            }
            jumping = true;
            _groundChecker.enabled = false;
            _jumpTime = _startJumpTime;
            _rigidbody.velocity += Vector2.up * JumpBase;
            _rigidbody.gravityScale = risingGravity;
        }

        if (down && jumping)
        {
            _rigidbody.gravityScale = 1;
            if (_jumpTime > 0)
            {
                _rigidbody.velocity += Vector2.up * JumpBase;
                _jumpTime -= Time.deltaTime;
            }
            if (_jumpTime < 0 && _jumpTime > airTime)
            {
                _rigidbody.gravityScale = airTimeGravity;
            }
            else
            {
                jumping = false;
                _rigidbody.gravityScale += fallingGravity;
            }
        }

        if (!down)
        {
            jumpSFXPlayed = false;
            jumping = false;
            _rigidbody.gravityScale += 0.02f;
            _groundChecker.enabled = true; 
        }

        _rigidbody.velocity = new Vector2(
            _rigidbody.velocity.x,
            Mathf.Clamp(_rigidbody.velocity.y, -maxfallSpeed, JumpBase)
            );
    }

    private bool GroundedCheck()
    {
        lastGroundedState = _grounded;

        if (jumping)
        {
            _grounded = false;
        }
        else
        {
            _grounded = _groundChecker.Grounded;
            if (!lastGroundedState && _grounded)
            {
                _rigidbody.gravityScale = 1;
                AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Land);
            }
        }
        
        return _grounded;
    }

    //--AJ--
    private void PlayJumpSFX() => AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Jump);

    public bool CanJump() => (_input.jumpKey.KeyDown() && Grounded && !jumping) ? true : false;
}
