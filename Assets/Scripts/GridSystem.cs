using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GridSystem : MonoBehaviour
{
    [Header("그리드 속성")]
    public Transform gridPivot;
    public int gridWidth;
    public int gridHeight;

    [Header("기타")]
    public Camera mainCam;

    public Vector3 CorrectMousePos
    {
        get { return mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 15f); }
    }

    public Node[,] GridMap { get; private set; }

    public void CreateGrid()
    {
        GridMap = new Node[gridWidth, gridHeight];

        for(int i = 0; i< gridWidth; i++)
        {
            for(int j = 0; j< gridHeight; j++)
            {
                Vector3 worldPos = new Vector3 (i , j , 0) + gridPivot.position;

                GridMap[i, j] = new Node(worldPos, false, null);
            }
        }
    }

    public void ResetAllObjPosInNode()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                var currentNode = GridMap[i, j];
                if (currentNode.isAttached)
                {
                    currentNode.attachedObject.transform.position = currentNode.position;
                }
            }
        }
    }

    /// <summary>
    /// 그리드 위에서의 마우스 포인트 위치를 얻습니다.
    /// </summary>
    /// <param name="mousePos"></param>
    /// <param name="onGridPos"></param>
    /// <returns></returns>
    public bool TryGetMousePosOnGrid(Vector3 mousePos, out Vector3 onGridPos)
    {
        onGridPos = Vector3.zero;

        if (IsMouseOnGrid(mousePos))
        {
            var gridIndex = GetGridMapIndex(mousePos);
            onGridPos = GridMap[gridIndex[0], gridIndex[1]].position;
            return true;
        }

        return false;
    }

    public bool TryGetMousePosOnGrid(Vector3 mousePos, out Vector3 onGridPos, out int[] gridIndex)
    {
        onGridPos = Vector3.zero;
        gridIndex = new int[2];

        if (IsMouseOnGrid(mousePos))
        {
            gridIndex = GetGridMapIndex(mousePos);
            onGridPos = GridMap[gridIndex[0], gridIndex[1]].position;
            return true;
        }

        return false;
    }


    /// <summary>
    /// 마우스 포인터의 위치가 그리드 위에 있는지 알아냅니다.
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public bool IsMouseOnGrid(Vector3 mousePos)
    {
        Vector3 minPos = gridPivot.position + new Vector3(-0.5f, -0.5f, 0);//gridMap[0, 0] + new Vector3(-0.5f,-0.5f,0);
        Vector3 maxPos = gridPivot.position + new Vector3(-0.5f + gridWidth, -0.5f + gridHeight, 0);//gridMap[gridWidth - 1, gridHeight - 1] + new Vector3(0.5f, 0.5f, 0);

        if ((mousePos.x >= minPos.x && mousePos.x <= maxPos.x)
            && (mousePos.y >= minPos.y && mousePos.y <= maxPos.y))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 그리드맵의 인덱스를 찾습니다.
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public int[] GetGridMapIndex(Vector3 mousePos)
    {
        mousePos.x -= (gridPivot.position.x - 0.5f);
        mousePos.y -= (gridPivot.position.y - 0.5f);

        var x = (int)Mathf.Clamp(mousePos.x, 0f, gridWidth - 1);
        var y = (int)Mathf.Clamp(mousePos.y, 0f, gridHeight - 1);

        int[] gridIndex = { x , y };

        return gridIndex;
    }

    /// <summary>
    /// 그리드 인덱스로 그리드맵의 노드를 찾습니다.
    /// </summary>
    /// <param name="gridIndex"></param>
    /// <returns></returns>
    public Node GetNodeInGrid(int[] gridIndex)
    {
        return GridMap[gridIndex[0],gridIndex[1]];
    }

    public bool TryGetAttachedNodeInGrid(ref Node node)
    {
        var currentNode = GetNodeInGrid(GetGridMapIndex(CorrectMousePos));

        if (currentNode.isAttached)
        {
            node = currentNode;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 그리드맵의 노드에 오브젝트를 붙입니다.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public bool TryAttachObjToNode(Node node, GameObject obj)
    {
        if (node.isAttached) return false;
        
        node.isAttached = true;
        node.attachedObject = obj;

        node.attachedObject.transform.position = node.position;
        return true;
    }

    /// <summary>
    /// 그리드맵의 노드에 오브젝트를 땝니다.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public bool TryDettachObjFromNode(Node node, bool destroy = true)
    {
        if (!node.isAttached) return false;

        if (destroy)
        {
            Destroy(node.attachedObject);
        }

        node.isAttached = false;
        node.attachedObject = null;
        return true;
    }

    private void OnDrawGizmos()
    {
        if (GridMap == null) return;

        Gizmos.color = Color.red;

        Vector3 minPos = new Vector3(-0.5f, -0.5f, 0) + gridPivot.position;
        Vector3 maxPos = gridPivot.position + new Vector3(gridWidth - 0.5f, gridHeight - 0.5f, 0);

        Gizmos.DrawSphere(minPos, 0.25f);
        Gizmos.DrawSphere(maxPos, 0.25f);

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector3 worldPos = new Vector3(i, j, 0) + gridPivot.position + Vector3.forward * 1.5f;

                //디버깅용 가시화
                Gizmos.color = Color.white;
                Gizmos.DrawCube(worldPos, Vector3.one);
            }
        }

        if(!TryGetMousePosOnGrid(mainCam.ScreenToWorldPoint(Input.mousePosition), out Vector3 onPos))
        {
            onPos = Vector3.one * 1080f;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(onPos, Vector3.one);
    }

}

public class Node
{
    public Vector3 position;
    public bool isAttached;
    public GameObject attachedObject;

    public Node (Vector3 position, bool isAttached, GameObject attachedObject)
    {
        this.position = position;
        this.isAttached = isAttached;
        this.attachedObject = attachedObject;
    }
}
