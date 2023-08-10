using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject gridNode;
    public Transform gridPivot;

    public int gridWidth;
    public int gridHeight;

    public Camera mainCam;

    public Node[,] gridMap;
    //public Transform debugPos;
    //public Transform debugPos2;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private void OnDrawGizmos()
    {
        if (gridMap == null) return;

        Gizmos.color = Color.red;

        Vector3 minPos = new Vector3(-0.5f, -0.5f, 0) + gridPivot.position;
        Vector3 maxPos = gridPivot.position + new Vector3(gridWidth - 0.5f,gridHeight - 0.5f, 0);

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

        TryGetMousePosOnGrid(mainCam.ScreenToWorldPoint(Input.mousePosition),out Vector3 onPos);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(onPos, Vector3.one);
    }

    void CreateGrid()
    {
        gridMap = new Node[gridWidth, gridHeight];

        for(int i = 0; i< gridWidth; i++)
        {
            for(int j = 0; j< gridHeight; j++)
            {
                Vector3 worldPos = new Vector3 (i , j , 0) + gridPivot.position;
                
                //디버깅용 가시화
                /*var g = Instantiate(gridNode,transform);
                g.transform.position = worldPos;*/

                gridMap[i, j] = new Node()
                {
                    position = worldPos,
                    isAttached = false,
                    attachedObject = null
                };
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
            var whs = GetGridMapIndex(mousePos);
            onGridPos = gridMap[whs[0], whs[1]].position;
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
            onGridPos = gridMap[gridIndex[0], gridIndex[1]].position;
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

        var w = (int)Mathf.Clamp(mousePos.x, 0f, gridWidth - 1);
        var h = (int)Mathf.Clamp(mousePos.y, 0f, gridHeight - 1);

        int[] gridIndex = new int[2];
        gridIndex[0] = w;
        gridIndex[1] = h;

        return gridIndex;
    }

    /// <summary>
    /// 그리드 인덱스로 그리드맵의 노드를 찾습니다.
    /// </summary>
    /// <param name="gridIndex"></param>
    /// <returns></returns>
    public Node GetNodeOnGrid(int[] gridIndex)
    {
        return gridMap[gridIndex[0],gridIndex[1]];
    }

    /// <summary>
    /// 그리드맵의 노드에 오브젝트를 붙입니다.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public void AttachObjToNode(Node node, GameObject obj)
    {
        if (node.isAttached) return;
        
        node.isAttached = true;
        node.attachedObject = obj;

        node.attachedObject.transform.position = node.position;
    }

    /// <summary>
    /// 그리드맵의 노드에 오브젝트를 땝니다.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public void DettachObjToNode(Node node, bool destroy = true)
    {
        if (!node.isAttached) return;

        if (destroy)
        {
            Destroy(node.attachedObject);
        }

        node.isAttached = false;
        node.attachedObject = null;
    }
}

public class Node
{
    public bool isAttached;
    public Vector3 position;
    public GameObject attachedObject;
}
