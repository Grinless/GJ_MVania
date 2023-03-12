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
        _groundChecker = GetComponent<PlayerGroundChecker>();
    }

    private bool lastGrounded;

    private void Update()
    {
        lastGrounded = _grounded;

        if (!Grounded) 
            return; 

        if(!lastGrounded && Grounded)
        {
            //--AJ--
            AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Land);
        }

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

            //--AJ--
            AudioByJaime.AudioController.Instance.PlaySound(AudioByJaime.SoundEffectType.Jump);

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
