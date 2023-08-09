using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Vector3[] bezierCurve3P = new Vector3[3];
    public Transform EnemyTransform;
    public float speed = 3.0f;
    float posTimer = 0;
    Rigidbody rb;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 8f);
        bezierCurve3P[0] = transform.position;
        bezierCurve3P[1] = transform.position + Vector3.right * Random.Range(-5f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        posTimer += Time.deltaTime;
        if (EnemyTransform != null)
        {
            //경로 생성
            bezierCurve3P[2] = EnemyTransform.position;
            var targetPos = CalculateBezier(bezierCurve3P, posTimer / 1f);
            var targetDir = targetPos - transform.position;

            transform.up = targetDir.normalized;
            rb.MoveRotation(transform.rotation);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 15f);
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        rb.MovePosition(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            var g = Instantiate(explosionEffect);
            g.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

    public Vector3 CalculateBezier(Vector3[] Positions, float t)
    {
        var lerpP1 = Vector3.Lerp(Positions[0], Positions[1], t);
        var lerpP2 = Vector3.Lerp(Positions[1], Positions[2], t);

        return Vector3.Lerp(lerpP1, lerpP2, t);
    }
}
