using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemyNodeObj : NodeObj
{
    EnemyController _enemy;
    BotItem _botItem;
    BonusItem _bonusItem;

    bool _itemHasMovePreset;
    CustomMovePreset _itemMovePreset;

    public override bool TryCombineOtherNodeObj(NodeObj other)
    {
        if(other.TryGetComponent(out CustomMoveProperty comp))
        {
            _itemHasMovePreset = true;
            _itemMovePreset = comp.preset;
        }

        //보너스 아이템
        if (other.activeObjPrefab.TryGetComponent(out _bonusItem))
        {
            _botItem = null;
            return true;
        }

        //드론봇 아이템
        if (other.activeObjPrefab.TryGetComponent(out _botItem))
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

        _enemy.itemHasMovePreset = _itemHasMovePreset;
        _enemy.itemMovePreset = _itemMovePreset;
    }
}
