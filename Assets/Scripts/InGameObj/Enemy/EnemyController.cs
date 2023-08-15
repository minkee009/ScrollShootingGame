using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HitableObj myHit;
    public GameObject dropItem;
    public bool itemHasMovePreset;
    public CustomMovePreset itemMovePreset;

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
        if (itemHasMovePreset)
        {
            switch (itemMovePreset)
            {
                case CustomMovePreset.ToDownward:
                    _createdDropItem.AddComponent<MoveDownward>();
                    var move = _createdDropItem.GetComponent<MoveDownward>();
                    move.speed = 4.0f;
                    move.moveSharpness = 12f;
                    move.rb = _createdDropItem.GetComponent<Rigidbody>();
                    break;
                case CustomMovePreset.ToHorizontal:
                    _createdDropItem.AddComponent<MoveHorizontal>();
                    var move2 = _createdDropItem.GetComponent<MoveHorizontal>();
                    move2.speed = 2.0f;
                    move2.moveSharpness = 12f;
                    move2.rb = _createdDropItem.GetComponent<Rigidbody>();
                    break;
                case CustomMovePreset.ToPlayer:
                    _createdDropItem.AddComponent<MoveToPlayer>();
                    var move3 = _createdDropItem.GetComponent<MoveToPlayer>();
                    move3.speed = 2.0f;
                    move3.moveSharpness = 12f;
                    move3.rb = _createdDropItem.GetComponent<Rigidbody>();
                    break;

            }
        }
    }
}
