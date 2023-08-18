using CartoonFX;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EnemyNodeObj : NodeObj
{
    EnemyController _enemy;
    
    ChildObjPreset _ItemObjPreset;

    public override bool TryCombineOtherNodeObj(NodeObj other)
    {
        switch (other.typeName)
        {
            default:
                return false;
            case "BonusItem":
            case "BotItem":
                _ItemObjPreset = other.GetChildObjPreset();
                return true;
        }
    }

    public override void CreateObj()
    {
        base.CreateObj();
        if (_myObject == null) return;

        _enemy = _myObject.GetComponent<EnemyController>();

        if (_ItemObjPreset != null)
        {
            _ItemObjPreset.pivotTransform = _myObject.transform;
            _enemy.itemPreset = _ItemObjPreset;
            _enemy.GetComponent<HitableObj>().OnDie += _enemy.itemPreset.CreateInPlay;
        }
    }
}
