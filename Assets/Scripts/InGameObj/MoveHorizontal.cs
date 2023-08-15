using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : CustomMove
{

    public float x;

    public override void ExcuteMove()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        x += deltaTime;

        var targetVel = Vector3.right * 0.3f * Mathf.Sin(x);

        _currentVelocity = Vector3.Lerp(_currentVelocity, targetVel, moveSharpness * deltaTime);

        transform.position += _currentVelocity;

        rb.Move(transform.position, transform.rotation);
    }
}
