using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10.0f;

    public LayerMask reflectMask;
    public bool reflect = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        var speedMag = speed * deltaTime;

        if (reflect && Physics.Raycast(new Ray(transform.position, transform.up), out RaycastHit hit, speedMag + 0.02f, reflectMask, QueryTriggerInteraction.Ignore))
        {
            speedMag -= hit.distance - 0.02f;

            transform.position += (hit.distance - 0.02f) * transform.up;
            rb.MovePosition(transform.position + (hit.distance - 0.02f) * transform.up);

            transform.up = Vector3.Reflect(transform.up, hit.normal);
            rb.MoveRotation(transform.rotation);

            transform.position += speedMag * transform.up;
            rb.MovePosition(transform.position + speedMag * transform.up);

            reflect = false;
            return;
        }

        transform.position += transform.up * speed * deltaTime;
        rb.MovePosition(transform.position);
    }
}
