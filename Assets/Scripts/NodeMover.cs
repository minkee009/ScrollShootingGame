using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour
{
    bool _isSelected = false;
    Node _selectedNode;
    GameObject _selectedObject;

    Vector3 _lastNodePos;

    public GridSystem gridSystem;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == CurrentGameState.Play) return;

        if (!_isSelected 
            && gridSystem.IsMouseOnGrid(gridSystem.CorrectMousePos) 
            && Input.GetMouseButtonDown(0))
        {
            if(gridSystem.TryGetAttachedNodeInGrid(ref _selectedNode))
            {
                _lastNodePos = _selectedNode.position;
                _selectedObject = _selectedNode.attachedObject;
                _isSelected = true;
            }
        }

        if (_isSelected)
        {
            var pos = Vector3.zero;
            if (!gridSystem.TryGetMousePosOnGrid(gridSystem.CorrectMousePos, out pos))
            {
                pos = gridSystem.CorrectMousePos;
            }

            _selectedObject.transform.position = pos;

            if (!Input.GetMouseButton(0))
            {
                _isSelected = false;
                if (gridSystem.IsMouseOnGrid(gridSystem.CorrectMousePos))
                {
                    var getNodeInfo = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.CorrectMousePos));
                    if (!getNodeInfo.isAttached)
                    {
                        gridSystem.TryDettachObjFromNode(_selectedNode, false);
                        gridSystem.TryAttachObjToNode(getNodeInfo, _selectedObject);
                        _selectedNode = null;
                        _selectedObject = null;
                        return;
                    }
                }

                _selectedNode.attachedObject.transform.position = _lastNodePos;                
            }
        }
    }
}
