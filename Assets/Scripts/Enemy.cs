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
    public GameObject botItem;

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
            GameManager.instance.attackScore += 10;
            hp--;
            if (other.tag == "Missile") hp--;

            if (hp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        var randomNum = Random.Range(0, 10);
        if (randomNum > 6)
        {
            randomNum = Random.Range(0, 10);
            var dropItem = Instantiate(randomNum > 5 ? Item : botItem);
            dropItem.transform.position = transform.position;
        }
        var effect = Instantiate(deathEffect);
        effect.transform.position = transform.position;
        GameManager.instance.destroyScore += 100;
        Destroy(gameObject);
    }
}
