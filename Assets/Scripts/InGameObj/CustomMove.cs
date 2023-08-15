using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float moveSharpness;

    protected Vector3 _currentVelocity;

    public virtual void Update()
    {
        if (GameManager.instance.currentGameState != CurrentGameState.Play) return;
        ExcuteMove();
    }

    public virtual void ExcuteMove()
    {
        
    }
}
