using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNode : MonoBehaviour
{
    public GameObject node;
    public Camera mainCam;
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
            var correctMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15f);
            
            if (gridSystem.TryGetMousePosOnGrid(correctMousePos,out Vector3 onGridPos))
            {
                _nativeNode.transform.position = onGridPos;
                var currentNode = gridSystem.GetNodeOnGrid(gridSystem.GetGridMapIndex(correctMousePos));

                if (Input.GetMouseButtonDown(0))
                {
                    if (!currentNode.isAttached)
                    {
                        gridSystem.AttachObjToNode(currentNode, _nativeNode);
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
                _nativeNode.transform.position = correctMousePos;
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
