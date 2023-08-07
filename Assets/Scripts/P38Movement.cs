using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P38Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 20f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var speedUp = Mathf.Max(0.2f, Input.GetAxis("Fire1")) * 5.0f; //> 0.5f ? 5.0f : 1.0f;

        transform.Translate(Vector3.forward * moveSpeed * speedUp * Time.deltaTime);
    }
}
