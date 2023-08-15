using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemyNodeObj : NodeObj
{
    EnemyController _enemy;
    BotItem _botItem;
    BonusItem _bonusItem;

    public override bool TryCombineOtherNodeObj(GameObject other)
    {
        //보너스 아이템
        if (other.TryGetComponent(out _bonusItem))
        {
            _botItem = null;
            return true;
        }

        //드론봇 아이템
        if (other.TryGetComponent(out _botItem))
        {
            _bonusItem = null;
            return true;
        }

        return false;
    }

    public override void CreateObj()
    {
        base.CreateObj();
        if (_myObject == null) return;

        _enemy = _myObject.GetComponent<EnemyController>();
        if(_bonusItem != null)
        {
            _enemy.dropItem = _bonusItem.gameObject;
        }
        if(_botItem != null)
        {
            _enemy.dropItem = _botItem.gameObject;
        }
    }
}
