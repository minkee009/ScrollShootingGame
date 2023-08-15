using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NodeMover : MonoBehaviour
{
    public GridSystem gridSystem;
    public RectTransform trashCan;
    public ChangeTrashCanImage changeTrashcan;

    bool _isSelected = false;
    bool _isOnDestroyZone = false;
    bool _wasOnDestroyZone = false;

    Node _selectedNode;
    GameObject _selectedNodePrefab;

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
        //휴지통 관리
        _isOnDestroyZone = _trashCanCorners[0].x < gridSystem.CorrectMousePos.x && _trashCanCorners[2].x > gridSystem.CorrectMousePos.x
                                   && _trashCanCorners[0].y < gridSystem.CorrectMousePos.y && _trashCanCorners[2].y > gridSystem.CorrectMousePos.y;
        if (_isSelected && _isOnDestroyZone != _wasOnDestroyZone)
        {
            changeTrashcan.ChangeImage(!_isOnDestroyZone);
        }
        _wasOnDestroyZone = _isOnDestroyZone;

        MoveNode();
    }

    public void SelectNodePrefab(GameObject nodePrefab)
    {
        _selectedNodePrefab = nodePrefab;
        _selectedNode = null;
        _isSelected = true;
    }

    public void MoveNode()
    {
        var isGamePlaying = GameManager.instance.currentGameState == CurrentGameState.Play;
        trashCan.gameObject.SetActive(!isGamePlaying);
        if (isGamePlaying) return;

        if (!_isSelected
            && gridSystem.IsGlobalPosOnGrid(gridSystem.CorrectMousePos)
            && Input.GetMouseButtonDown(0))
        {
            if (gridSystem.TryGetAttachedNodeInGrid(ref _selectedNode))
            {
                _lastNodePos = _selectedNode.position;
                _selectedNodePrefab = _selectedNode.attachedObject;
                _isSelected = true;
            }
        }

        if (_isSelected)
        {
            var pos = Vector3.zero;

            if (!gridSystem.TryGetGlobalPosOnGrid(gridSystem.CorrectMousePos, out pos))
            {
                pos = gridSystem.CorrectMousePos;
            }

            _selectedNodePrefab.transform.position = pos;

            if (!Input.GetMouseButton(0))
            {
                _isSelected = false;

                if (gridSystem.IsGlobalPosOnGrid(gridSystem.CorrectMousePos)) //그리드 안
                {
                    var getNodeInfo = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.CorrectMousePos));
                    

                    if (!getNodeInfo.isAttached) //겹침 없음
                    {
                        gridSystem.TryDettachObjFromNode(_selectedNode, false);
                        gridSystem.TryAttachObjToNode(getNodeInfo, _selectedNodePrefab);
                        //_selectedNode = null;
                        //_selectedObject = null;
                        return;
                    }
                    else
                    {
                        var currentNodeObj = _selectedNodePrefab.GetComponent<NodeObj>();
                        var currentNodeProp = _selectedNodePrefab.GetComponent<NodeProp>();
                        var getNodeObjInfo = getNodeInfo.attachedObject.GetComponent<NodeObj>();

                        //겹치기
                        if (currentNodeObj != null 
                            && getNodeObjInfo.combineAbleObj
                            && getNodeObjInfo.TryCombineOtherNodeObj(currentNodeObj.activeObjPrefab))
                        {
                            gridSystem.TryDettachObjFromNode(_selectedNode);
                            _selectedNode = null;
                            Destroy(_selectedNodePrefab);
                            return;
                        }
                        if (currentNodeProp != null)
                        {
                            return;
                        }
                    }
                }
                else //그리드 밖
                {
                    if (_isOnDestroyZone) //삭제존
                    {
                        changeTrashcan.ChangeImage(true);

                        if (_selectedNodePrefab.GetComponent<NodeObj>().nonRemoveAbleObj)
                        {
                            //실패 시
                            _selectedNode.attachedObject.transform.position = _lastNodePos;
                            return;
                        }

                        if (!gridSystem.TryDettachObjFromNode(_selectedNode))
                        {
                            Destroy(_selectedNodePrefab);
                        }
                        _selectedNode = null;
                        _selectedNodePrefab = null;
                        return;
                    }
                }


                //이동 불가 상태 / 혹은 무조건 삭제
                if (_selectedNode != null)
                    _selectedNode.attachedObject.transform.position = _lastNodePos;
                else
                {
                    Destroy(_selectedNodePrefab);
                    changeTrashcan.ChangeImage(true);
                }
            }
        }
    }
}
