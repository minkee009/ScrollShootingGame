using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 아래방향으로 이동한다 
public class Enemy : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 dir = Vector3.down;
    public Transform playerTransform;
    public Bullet enemyBullet;
    public GameObject deathEffect;
    public GameObject Item;

    public int hp = 3;
    float chaseMode = 0;
    float incount = 0;

    // Start is called before the first frame update
    void Start()
    {
        chaseMode = Random.Range(0, 10);

        if (Random.Range(0, 10) <= 3 && playerTransform != null)
        {
            dir = (playerTransform.position - transform.position).normalized;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        incount += Time.deltaTime;
        if(incount > 0.7)
        {
            Fire();
            incount = 0;
        }

        if(chaseMode <= 1 && playerTransform != null)
        {
            dir = (playerTransform.position - transform.position).normalized;
        }

        transform.position += dir * speed * Time.deltaTime;
    }

    public void Fire()
    {
        var bullet = Instantiate(enemyBullet);
        bullet.tag = "Enemy";
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody>().MovePosition(transform.position);
        bullet.transform.up = (playerTransform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().MoveRotation(bullet.transform.rotation);
        bullet.speed = 8.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            hp--;

            if (hp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        if(Random.Range(0,10) > 6)
        {
            var dropItem = Instantiate(Item);
            dropItem.transform.position = transform.position;
        }
        var effect = Instantiate(deathEffect);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
