using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public void Start()
    {
        GameManager.instance.Act_OnGameReset += ForceDestroy;
    }

    public void ForceDestroy()
    {
        if (!gameObject.activeSelf) return;

        if (gameObject.layer == 8)
        {
            BulletManager.instance.playerBulletPool.Add(gameObject);
            transform.parent = BulletManager.instance.playerBulletPoolParent;
        }
        else if (gameObject.layer == 9)
        {
            BulletManager.instance.enemyBulletPool.Add(gameObject);
            transform.parent = BulletManager.instance.enemyBulletPoolParent;
        }

        gameObject.SetActive(false);
        return;
    }

    public void OnDestroy()
    {
        GameManager.instance.Act_OnGameReset -= ForceDestroy;
    }
}
