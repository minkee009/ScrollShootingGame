using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var playerFire = other.GetComponent<PlayerFire>();
        var playerHit = other.GetComponent<HitableObj>();

        playerFire.skillLevel = Mathf.Min(playerFire.skillLevel + 1, 3);
        playerHit.IncOrDecHp(1f);
        Destroy(gameObject);
    }
}
