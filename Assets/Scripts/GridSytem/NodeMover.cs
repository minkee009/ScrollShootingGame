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

    // Update is called once per frame
    void Update()
    {
        //휴지통 관리
        trashCan.GetWorldCorners(_trashCanCorners);
        _trashCanCorners[0] = gridSystem.mainCam.ScreenToWorldPoint(_trashCanCorners[0]);
        _trashCanCorners[2] = gridSystem.mainCam.ScreenToWorldPoint(_trashCanCorners[2]);

        _isOnDestroyZone = _trashCanCorners[0].x < gridSystem.WorldMousePos.x && _trashCanCorners[2].x > gridSystem.WorldMousePos.x
                                   && _trashCanCorners[0].y < gridSystem.WorldMousePos.y && _trashCanCorners[2].y > gridSystem.WorldMousePos.y;
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
            && gridSystem.IsWorldPosOnGrid(gridSystem.WorldMousePos)
            && Input.GetMouseButtonDown(0))
        {
            if (gridSystem.TryGetAttachedNodeInGrid(out _selectedNode, gridSystem.GetGridMapIndex(gridSystem.WorldMousePos)))
            {
                _lastNodePos = _selectedNode.position;
                _selectedNodePrefab = _selectedNode.attachedObject;
                _isSelected = true;
            }
        }

        if (_isSelected)
        {
            var pos = Vector3.zero;

            if (!gridSystem.TryGetWorldPosOnGrid(gridSystem.WorldMousePos, out pos))
            {
                pos = gridSystem.WorldMousePos;
            }

            _selectedNodePrefab.transform.position = pos;

            if (!Input.GetMouseButton(0))
            {
                _isSelected = false;

                var currentIsNodeObj = _selectedNodePrefab.TryGetComponent(out NodeObj currentNodeObj);
                var currentIsNodeProp = _selectedNodePrefab.TryGetComponent(out INodeProp currentNodeProp);

                if (gridSystem.IsWorldPosOnGrid(gridSystem.WorldMousePos)) //그리드 안
                {
                    var getNodeInfo = gridSystem.GetNodeInGrid(gridSystem.GetGridMapIndex(gridSystem.WorldMousePos));
                    

                    if (!getNodeInfo.isAttached) //겹침 없음
                    {
                        if (currentIsNodeObj)
                        {
                            gridSystem.TryDettachObjFromNode(_selectedNode, false);
                            gridSystem.TryAttachObjToNode(getNodeInfo, _selectedNodePrefab);
                            return;
                        }
                    }
                    else
                    {
                        var getNodeObjInfo = getNodeInfo.attachedObject.GetComponent<NodeObj>();

                        //겹치기
                        if (currentIsNodeObj
                            && getNodeObjInfo.combineAbleObj
                            && getNodeObjInfo.TryCombineOtherNodeObj(currentNodeObj))
                        {
                            gridSystem.TryDettachObjFromNode(_selectedNode);
                            _selectedNode = null;
                            Destroy(_selectedNodePrefab);
                            return;
                        }
                        if (!currentNodeObj
                            && currentIsNodeProp
                            && currentNodeProp.TryCombineOtherNodeObj(getNodeObjInfo))
                        {
                            _selectedNode = null;
                            Destroy(_selectedNodePrefab);
                            return;
                        }
                    }
                }
                else //그리드 밖
                {
                    if (_isOnDestroyZone) //삭제존
                    {
                        changeTrashcan.ChangeImage(true);

                        if (currentIsNodeObj && currentNodeObj.nonRemoveAbleObj)
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
