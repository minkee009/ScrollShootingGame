using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public GameObject node;
    public GridSystem gridSystem;

    private bool readyToCreate;

    GameObject _nativeNode;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.currentGameState == CurrentGameState.Play) return;

        if (readyToCreate && Input.GetMouseButtonDown(0))
        {
            CreateNodeInRuntime();
        }

        if(_nativeNode != null)
        {
            if (gridSystem.TryGetMousePosOnGrid(gridSystem.CorrectMousePos,out Vector3 onGridPos))
            {
                _nativeNode.transform.position = onGridPos;
                var currentNode = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.CorrectMousePos));

                if (!Input.GetMouseButton(0))
                {
                    if (!currentNode.isAttached)
                    {
                        gridSystem.TryAttachObjToNode(currentNode, _nativeNode);
                        _nativeNode = null;
                    }
                    else
                    {
                        Destroy(_nativeNode);
                    }
                }
            }
            else
            {
                _nativeNode.transform.position = gridSystem.CorrectMousePos;
                if (Input.GetMouseButtonUp(0))
                {
                    Destroy(_nativeNode);
                }
            }
        }
    }

    void CreateNodeInRuntime()
    {
        if(_nativeNode  == null)
        {
            _nativeNode = Instantiate(node, Input.mousePosition + Vector3.forward * 15f, Quaternion.identity);
        }
    }

    public void OnMouseEvent()
    {
        readyToCreate = true;
    }

    public void ExitMouseEvent()
    {
        readyToCreate = false;
    }
}
