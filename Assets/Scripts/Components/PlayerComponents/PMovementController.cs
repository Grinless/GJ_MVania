using System.Collections;
using UnityEngine;

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
