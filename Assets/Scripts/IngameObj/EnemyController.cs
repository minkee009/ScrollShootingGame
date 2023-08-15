using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HitableObj myHit;
    public GameObject dropItem;

    GameObject _createdDropItem;
    HitableObj _hitObj;

    private void Start()
    {
        GameManager.instance.Act_OnGameReset += DestroyDropItemOnReset;
        myHit = GetComponent<HitableObj>();
        myHit.OnHit += SetAttackScoreOnHit;
        myHit.OnDie += SetDestroyScoreOnDie;
        myHit.OnDie += CreateDropItem;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out _hitObj))
        {
            _hitObj.Hit(-1, gameObject);
            myHit.IncOrDecHp(-10);
        }
    }

    public void SetAttackScoreOnHit()
    {
        GameManager.instance.attackScore += 10;
    }

    public void SetDestroyScoreOnDie()
    {
        GameManager.instance.destroyScore += 100;
    }

    public void DestroyDropItemOnReset()
    {
        Destroy(_createdDropItem);
    }

    public void CreateDropItem()
    {
        if (dropItem == null) return;
        _createdDropItem = Instantiate(dropItem);
        _createdDropItem.transform.position = transform.position;
    }
}
