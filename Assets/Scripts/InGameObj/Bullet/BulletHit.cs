using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    public float hitDamage = 1.0f;

    HitableObj _hitObj;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out _hitObj))
        {
            _hitObj.Hit(-hitDamage, gameObject);



            Destroy(gameObject);
        }
    }
}
