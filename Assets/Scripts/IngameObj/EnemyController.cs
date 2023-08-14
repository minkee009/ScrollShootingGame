using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HitableObj myHit;

    HitableObj _hitObj;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out _hitObj))
        {
            _hitObj.Hit(-1, gameObject);
            myHit.IncOrDecHp(-10);
        }
    }
}
