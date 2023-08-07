using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 사용자 입력(스페이스)를 받아 총알을 만들고 싶다.
public class PlayerFire : MonoBehaviour
{
    public Bullet myBullet;
    //public Transform EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var currentBullet = GameObject.Instantiate(myBullet);
            currentBullet.transform.position = transform.position;
            currentBullet.GetComponent<Rigidbody>().MovePosition(transform.position);
            currentBullet.tag = "Player";
            //currentBullet.dir = EnemyTransform != null ? (EnemyTransform.position - transform.position).normalized : Vector3.up;
            currentBullet.speed = 15f;
        }
    }
}
