using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;

    public Vector3 CameraPos;
    public float CameraChaseSpeed = 4f;

    Vector3 targetPos;
    Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = PlayerTransform.position + CameraPos;
        currentPos = Vector3.Lerp(currentPos, targetPos, CameraChaseSpeed * Time.deltaTime);
        transform.position = currentPos;
    }
}
