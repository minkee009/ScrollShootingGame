using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


// ��ǥ: ���� ���� ���ư���.
// ������ �ʿ��ϴ�.
// �ӵ��� �ʿ��ϴ�.

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    //public Vector3 dir = Vector3.up;
    public bool reflect = false;
    public LayerMask reflectMask;
    public GameObject explosionEffect;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up * speed * Time.deltaTime, Color.red);

        var speedMag = speed * Time.deltaTime;

        //4���� -> 4����(��ǥ��) * 3���� ����
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
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            var g = Instantiate(explosionEffect);
            g.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
