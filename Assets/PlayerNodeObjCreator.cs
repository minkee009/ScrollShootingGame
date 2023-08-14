using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObjCreator : NodeObjCreater
{
    public override void CreateNodeObj()
    {
        if (_isFirstCreation)
        {
            _nodeObj = Instantiate(activeObjPrefab);
            _nodeObj.transform.position = transform.position;
            _isFirstCreation = false;
            
            GameManager.instance.playerTransform = _nodeObj.transform;
        }
    }

    public override void ResetNodeObj()
    {
        if (_nodeObj != null)
            Destroy(_nodeObj);

        _isFirstCreation = true;
        var gridIndex = GameManager.instance.gridSystem.GetGridMapIndex(transform.position);
        GameManager.instance.playerInitPos = new Vector2(gridIndex[0] + 1, gridIndex[1] + 1);
    }
}
