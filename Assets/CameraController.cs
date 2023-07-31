using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;

    public Vector3 targetPos;
    public Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPos = PlayerTransform.position + new Vector3(0,2 ,-10);
        currentPos = Vector3.Lerp(currentPos, targetPos, 4f * Time.deltaTime);
        transform.position = currentPos;
    }
}
