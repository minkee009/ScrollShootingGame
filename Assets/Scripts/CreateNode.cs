using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public GameObject node;
    public GridSystem gridSystem;

    bool _readyToCreate;
    GameObject _currentNode;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == CurrentGameState.Play) return;

        if (_readyToCreate && Input.GetMouseButtonDown(0))
        {
            CreateNodeInRuntime();
        }

        if(_currentNode != null)
        {
            if (gridSystem.TryGetGlobalPosOnGrid(gridSystem.CorrectMousePos,out Vector3 onGridPos))
            {
                _currentNode.transform.position = onGridPos;
                var currentNode = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.CorrectMousePos));

                if (!Input.GetMouseButton(0))
                {
                    if (!currentNode.isAttached)
                    {
                        gridSystem.TryAttachObjToNode(currentNode, _currentNode);
                        _currentNode = null;
                    }
                    else
                    {
                        Destroy(_currentNode);
                    }
                }
            }
            else
            {
                _currentNode.transform.position = gridSystem.CorrectMousePos;
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(_currentNode);
                }
            }
        }
    }

    void CreateNodeInRuntime()
    {
        if(_currentNode == null)
        {
            _currentNode = Instantiate(node, Input.mousePosition + Vector3.forward * 15f, Quaternion.identity);
        }
    }

    public void OnMouseEvent()
    {
        _readyToCreate = true;
    }

    public void ExitMouseEvent()
    {
        _readyToCreate = false;
    }
}
