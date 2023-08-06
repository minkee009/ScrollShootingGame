using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P38Controller : MonoBehaviour
{
    public float rotationSpeed = 60.0f;

    // Update is called once per frame
    void Update()
    {
        float pitch = Input.GetAxis("Vertical");
        float roll = Input.GetAxis("Horizontal");

        pitch = Mathf.Clamp(pitch, -1, 1);
        roll = Mathf.Clamp(roll, -1, 1);

        Vector3 Rotation = new Vector3(pitch, 0, -roll);

        transform.Rotate(Rotation * rotationSpeed * Time.deltaTime);
    }
}
