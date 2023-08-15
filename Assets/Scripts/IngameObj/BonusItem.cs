using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public GameObject effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;

        var playerFire = other.GetComponent<PlayerFire>();
        var playerHit = other.GetComponent<HitableObj>();

        playerFire.skillLevel = Mathf.Min(playerFire.skillLevel + 1, 3);
        playerHit.IncOrDecHp(1f);

        var currentEffect = Instantiate(effect);
        currentEffect.transform.position = transform.position;

        Destroy(gameObject);
    }
}
