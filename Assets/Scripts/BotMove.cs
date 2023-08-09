using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{
    public Transform Center;

    public float radius = 2.0f;
    public float speed = 2.0f;

    float angle = 0f;

    // Update is called once per frame
    void Update()
    {
        if (ReferenceEquals(Center, null)) return;
        angle += speed * Time.deltaTime;
        transform.position = Center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
    }

    public void InitPos()
    {
        transform.position = Center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
    }
}
