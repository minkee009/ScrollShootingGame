using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreatorNodeObj : NodeObj
{
    ChildObjPreset _createObjPreset;
    ChildObjPreset _enemysChildPreset;

    public override bool TryCombineOtherNodeObj(NodeObj other)
    {
        var creator = activeObjPrefab.GetComponent<ObjectCreatorController>();

        switch (other.typeName)
        {
            default:
                return false;
            case "Enemy": 
            case "BonusItem":
            case "BotItem":
                _createObjPreset = other.GetChildObjPreset();
                break;
        }

        if(other.typeName == "Enemy")
        {
            var enemy = other as EnemyNodeObj;
            _enemysChildPreset = enemy.ItemObjPreset;
        }

        return true;
    }


    public override void CreateObj()
    {
        base.CreateObj();

        var createCon = _myObject.GetComponent<ObjectCreatorController>();
        createCon.createObjPreset = _createObjPreset;
        if(_enemysChildPreset != null) createCon.enemysItemChild = _enemysChildPreset;
    }
}
