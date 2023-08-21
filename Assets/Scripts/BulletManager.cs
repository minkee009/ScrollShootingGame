using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;

    public GameObject playerBullet;
    public GameObject enemyBullet;

    public Transform playerBulletPoolParent;
    public Transform enemyBulletPoolParent;

    public List<GameObject> playerBulletPool;
    public List<GameObject> enemyBulletPool;

    public int playerBulletPoolSize = 100;
    public int enemyBulletPoolSize = 30;

    public void Awake()
    {
        instance = this;

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(instance);
            }
        }
    }

    public void Start()
    {
        playerBulletPool = new List<GameObject>();

        for(int i = 0; i < playerBulletPoolSize; i++)
        {
            var currentBullet = Instantiate(playerBullet);

            playerBulletPool.Add(currentBullet);

            currentBullet.SetActive(false);
            currentBullet.transform.parent = playerBulletPoolParent;
        }

        enemyBulletPool = new List<GameObject>();

        for(int i = 0; i < enemyBulletPoolSize; i++)
        {
            var currentBullet = Instantiate(enemyBullet);

            enemyBulletPool.Add(currentBullet);

            currentBullet.SetActive(false);
            currentBullet.transform.parent = enemyBulletPoolParent;
        }
    }
}
