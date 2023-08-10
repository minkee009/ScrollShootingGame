using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public GameObject node;
    public GridSystem gridSystem;

    GameObject _nativeNode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_nativeNode != null)
        {
            if (gridSystem.TryGetMousePosOnGrid(gridSystem.correctMousePos,out Vector3 onGridPos))
            {
                _nativeNode.transform.position = onGridPos;
                var currentNode = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.correctMousePos));

                if (Input.GetMouseButtonDown(0))
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
                _nativeNode.transform.position = gridSystem.correctMousePos;
            }
        }
    }

    public void CreateNodeInRuntime()
    {
        if(_nativeNode  == null)
        {
            _nativeNode = Instantiate(node, Input.mousePosition + Vector3.forward * 15f, Quaternion.identity);
        }
    }
}
