using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObj : NodeObj
{
    public override void CreateObj()
    {
        if (_isFirstCreation)
        {
            _myObject = Instantiate(activeObjPrefab);
            _myObject.transform.position = transform.position;
            _isFirstCreation = false;

            GameManager.instance.playerTransform = _myObject.transform;
        }
    }

    public override void ResetObj()
    {
        if (_myObject != null)
            Destroy(_myObject);

        _isFirstCreation = true;
        var gridIndex = GameManager.instance.gridSystem.GetGridMapIndex(transform.position);
        GameManager.instance.playerInitPos = new Vector2(gridIndex[0] + 1, gridIndex[1] + 1);
    }
}
