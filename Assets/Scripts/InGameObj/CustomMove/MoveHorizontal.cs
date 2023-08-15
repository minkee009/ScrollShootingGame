using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : CustomMove
{
    float _moveTimer = 1f;
    float _waitTimer = 2f;
    float _minus = 1f;
    float _waitSpeed = 1f;

    public override void ExcuteMove()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        if(_waitTimer > 2f)
        {
            _moveTimer += deltaTime;
            _waitSpeed = 1f;
        }
        
        if(_moveTimer > 2f)
        {
            _minus = -_minus;
            _moveTimer = 0f;
            _waitTimer = 0f;
            _waitSpeed = 0f;
        }
        _waitTimer += deltaTime;

        var targetVel = Vector3.right * speed * _waitSpeed * _minus * deltaTime;

        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVel, moveSharpness * deltaTime);

        transform.position += _currentVelocity;

        rb.Move(transform.position, transform.rotation);
    }
}
