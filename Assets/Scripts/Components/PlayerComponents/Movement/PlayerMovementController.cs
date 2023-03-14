using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
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
        get { return _data.runRampData.shortJump / _data.runRampData.rampTime; }
    }

    float CurrentRampTime
    {
        get;
        set;
    }

    private void Start()
    {
        _data = PlayerController.instance.MovementData;
        _input = PlayerController.instance.Input;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerController.instance.Paused)
            return;

        _currentVelocity = Vector2.zero;

        //Detect directional inputs.
        if (EvaluateMovingState())
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

    private float CalculateSpeed()
    {
        float baseDirection = PlayerController.instance.MovementDirection.x;
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

    private bool EvaluateMovingState() => (_currentVelocity.x != 0);

    private bool RampingCheck() => (_isMoving && !_isRamping);
}
