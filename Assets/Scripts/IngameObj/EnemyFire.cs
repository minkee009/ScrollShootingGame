using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public BulletMove bullet;

    float _fireTimer = 0;

    // Update is called once per frame
    void Update()
    {
        _fireTimer += Time.deltaTime * GameManager.instance.inGameTimeSpeed;
        if (_fireTimer > 0.7)
        {
            Fire();
            _fireTimer = 0;
        }
    }

    private void Fire()
    {
        if (GameManager.instance.playerTransform == null) return;
        var currentBullet = Instantiate(bullet);

        currentBullet.transform.position = transform.position;
        currentBullet.transform.up = (GameManager.instance.playerTransform.position - transform.position).normalized;

        currentBullet.GetComponent<Rigidbody>().Move(currentBullet.transform.position, currentBullet.transform.rotation);
    }
}
