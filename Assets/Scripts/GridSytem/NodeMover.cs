using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NodeMover : MonoBehaviour
{
    public GridSystem gridSystem;
    public RectTransform trashCan;
    public ChangeTrashCanImage changeTrashcan;

    bool _isSelected = false;
    Node _selectedNode;
    GameObject _selectedObject;

    Vector3 _lastNodePos;
    Vector3[] _trashCanCorners = new Vector3[4];

    private void Start()
    {
        trashCan.GetWorldCorners(_trashCanCorners);
        _trashCanCorners[0] = gridSystem.mainCam.ScreenToWorldPoint(_trashCanCorners[0]);
        _trashCanCorners[2] = gridSystem.mainCam.ScreenToWorldPoint(_trashCanCorners[2]);
    }

    // Update is called once per frame
    void Update()
    {
        var isGamePlaying = GameManager.instance.currentGameState == CurrentGameState.Play;
        trashCan.gameObject.SetActive(!isGamePlaying);
        if (isGamePlaying) return;

        if (!_isSelected 
            && gridSystem.IsGlobalPosOnGrid(gridSystem.CorrectMousePos) 
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
            gridSystem.isEditNodeMode = true;

            if (!gridSystem.TryGetGlobalPosOnGrid(gridSystem.CorrectMousePos, out pos))
            {
                pos = gridSystem.CorrectMousePos;
            }

            _selectedObject.transform.position = pos;

            if (!Input.GetMouseButton(0))
            {
                _isSelected = false;
                gridSystem.isEditNodeMode = false;
                if (gridSystem.IsGlobalPosOnGrid(gridSystem.CorrectMousePos))
                {
                    var getNodeInfo = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.CorrectMousePos));
                    if (!getNodeInfo.isAttached)
                    {
                        gridSystem.TryDettachObjFromNode(_selectedNode, false);
                        gridSystem.TryAttachObjToNode(getNodeInfo, _selectedObject);
                        //_selectedNode = null;
                        //_selectedObject = null;
                        return;
                    }
                    /*else //부착가능한 오브젝트
                    {
                        return;
                    }*/
                }
                else 
                {
                    bool isOnDestroyZone = _trashCanCorners[0].x < gridSystem.CorrectMousePos.x && _trashCanCorners[2].x > gridSystem.CorrectMousePos.x
                                   && _trashCanCorners[0].y < gridSystem.CorrectMousePos.y && _trashCanCorners[2].y > gridSystem.CorrectMousePos.y;


                    if (isOnDestroyZone) //삭제존
                    {
                        changeTrashcan.ChangeImage(true);
                        gridSystem.TryDettachObjFromNode(_selectedNode);
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
