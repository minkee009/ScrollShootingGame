using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : CustomMove
{
    Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameManager.instance.playerTransform;
    }

    public override void ExcuteMove()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;
        var dir = (_playerTransform.position - transform.position).normalized;
        var targetVel = dir * speed * deltaTime;

        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVel, moveSharpness * deltaTime);

        transform.position += _currentVelocity;
        rb.Move(transform.position, transform.rotation);
    }
}
