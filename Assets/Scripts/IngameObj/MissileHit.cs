using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileHit : MonoBehaviour
{
    public float hitDamage = 2.0f;

    public MissileController myController;
    public GameObject hitEffect;

    HitableObj _hitObj;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _hitObj))
        {
            _hitObj.Hit(-hitDamage, gameObject, false);
            var currentEffect = Instantiate(hitEffect);
            currentEffect.transform.position = transform.position;
            myController.SoftDestroy();
        }
    }
}
