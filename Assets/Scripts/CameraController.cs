using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;

    public Vector3 CameraPos;
    public float CameraChaseSpeed = 2f;

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
        targetPos.x = Mathf.Clamp(targetPos.x, -28, 28);
        targetPos.y = Mathf.Clamp(targetPos.y, -18, 18);
        currentPos = Vector3.Lerp(currentPos, targetPos, CameraChaseSpeed * Time.deltaTime);
        transform.position = currentPos;
    }
}
