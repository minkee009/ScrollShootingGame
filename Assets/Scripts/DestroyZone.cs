using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.currentGameState != CurrentGameState.Play 
            || other.CompareTag("Missile")) return;

        if (other.gameObject.layer == 8)
        {
            BulletManager.instance.playerBulletPool.Add(other.gameObject);
            other.transform.parent = BulletManager.instance.playerBulletPoolParent;
            other.gameObject.SetActive(false);
            return;
        }
        if (other.gameObject.layer == 9)
        {
            BulletManager.instance.enemyBulletPool.Add(other.gameObject);
            other.transform.parent = BulletManager.instance.enemyBulletPoolParent;
            other.gameObject.SetActive(false);
            return;
        }

        Destroy(other.gameObject);
    }
}
