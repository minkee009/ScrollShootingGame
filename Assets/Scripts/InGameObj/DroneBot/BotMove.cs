using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public Transform Center;

    public float radius = 2.0f;
    public float speed = 2.0f;

    float _angle = 0f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState != CurrentGameState.Play 
            || ReferenceEquals(Center, null)) return;
        _angle += speed * Time.deltaTime * GameManager.instance.inGameTimeSpeed;
        var targetPos = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), -0.2f) * radius + Center.position;
        transform.position = Vector3.Lerp(transform.position,targetPos, 6f * Time.deltaTime);
    }

    public void InitPos()
    {
        transform.position = Center.position + (Vector3.forward * 0.8f);
    }
}
