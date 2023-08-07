using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P38CamController : MonoBehaviour
{
    public float smoothTime = 0.3f;

    Transform p38Transform;
    Vector3 lastPlayerPos;

    private void Awake()
    {
        p38Transform = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerPos = p38Transform.position;
    }

    void LateUpdate()
    {
        var veloctiy = (p38Transform.position - lastPlayerPos) / Time.deltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, p38Transform.position,ref veloctiy, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, p38Transform.rotation, 6f * Time.deltaTime);

        lastPlayerPos = p38Transform.position;
    }
}
