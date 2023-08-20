using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownward : CustomMove
{

    public override void ExcuteMove()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;
        var targetVel = Vector3.down * speed * deltaTime;

        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVel, moveSharpness * deltaTime);

        transform.position += _currentVelocity;
        rb.Move(transform.position, transform.rotation);
    }
}
