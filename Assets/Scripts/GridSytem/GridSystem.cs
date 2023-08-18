using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements.Experimental;

public class GridSystem : MonoBehaviour
{
    [Header("�׸��� �Ӽ�")]
    public Transform gridPivot;
    public int gridWidth;
    public int gridHeight;

    [Header("��Ÿ")]
    public Camera mainCam;

    public Vector3 WorldMousePos
    {
        get { return mainCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 1.5f); }
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
    /// �׸��� �������� ���� ������ ��ġ�� ����ϴ�.
    /// </summary>
    /// <param name="worldPos"></param>
    /// <param name="onGridPos"></param>
    /// <returns></returns>
    public bool TryGetWorldPosOnGrid(Vector3 worldPos, out Vector3 onGridPos)
    {
        onGridPos = Vector3.zero;

        if (IsWorldPosOnGrid(worldPos))
        {
            var gridIndex = GetGridMapIndex(worldPos);
            onGridPos = GridMap[gridIndex[0], gridIndex[1]].position;
            return true;
        }

        return false;
    }

    public bool TryGetWorldPosOnGrid(Vector3 worldPos, out Vector3 onGridPos, out int[] gridIndex)
    {
        onGridPos = Vector3.zero;
        gridIndex = new int[2];

        if (IsWorldPosOnGrid(worldPos))
        {
            gridIndex = GetGridMapIndex(worldPos);
            onGridPos = GridMap[gridIndex[0], gridIndex[1]].position;
            return true;
        }

        return false;
    }


    /// <summary>
    /// �Է��� ���� �������� ��ġ�� �׸��� ���� �ִ��� �˾Ƴ��ϴ�.
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public bool IsWorldPosOnGrid(Vector3 worldPos)
    {
        Vector3 minPos = gridPivot.position + new Vector3(-0.5f, -0.5f, 0);//gridMap[0, 0] + new Vector3(-0.5f,-0.5f,0);
        Vector3 maxPos = gridPivot.position + new Vector3(-0.5f + gridWidth, -0.5f + gridHeight, 0);//gridMap[gridWidth - 1, gridHeight - 1] + new Vector3(0.5f, 0.5f, 0);

        if ((worldPos.x >= minPos.x && worldPos.x <= maxPos.x)
            && (worldPos.y >= minPos.y && worldPos.y <= maxPos.y))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// �׸������ �ε����� ã���ϴ�.
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public int[] GetGridMapIndex(Vector3 worldPos)
    {
        worldPos.x -= (gridPivot.position.x - 0.5f);
        worldPos.y -= (gridPivot.position.y - 0.5f);

        var x = (int)Mathf.Clamp(worldPos.x, 0f, gridWidth - 1);
        var y = (int)Mathf.Clamp(worldPos.y, 0f, gridHeight - 1);

        int[] gridIndex = { x , y };

        return gridIndex;
    }

    /// <summary>
    /// �׸��� �ε����� �׸������ ��带 ã���ϴ�.
    /// </summary>
    /// <param name="gridIndex"></param>
    /// <returns></returns>
    public Node GetNodeInGrid(int[] gridIndex)
    {
        return GridMap[gridIndex[0],gridIndex[1]];
    }

    public Node GetNodeInGrid(Vector2 gridIndex)
    {
        var clampedX = (int)Mathf.Clamp(gridIndex.x - 1, 0, gridWidth - 1);
        var clampedY = (int)Mathf.Clamp(gridIndex.y - 1, 0, gridHeight - 1);
        return GridMap[clampedX, clampedY];
    }

    public bool TryGetAttachedNodeInGrid(ref Node node, int[] index)
    {
        var currentNode = GetNodeInGrid(index);

        if (currentNode.isAttached)
        {
            node = currentNode;
            return true;
        }
        return false;
    }

    /// <summary>
    /// �׸������ ��忡 ������Ʈ�� ���Դϴ�.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public bool TryAttachObjToNode(Node node, GameObject obj)
    {
        if (node == null || node.isAttached) return false;
        
        node.isAttached = true;
        node.attachedObject = obj;

        node.attachedObject.transform.position = node.position;
        return true;
    }

    /// <summary>
    /// �׸������ ��忡 ������Ʈ�� ���ϴ�.
    /// </summary>
    /// <param name="node"></param>
    /// <param name="obj"></param>
    public bool TryDettachObjFromNode(Node node, bool destroy = true)
    {
        if (node == null || !node.isAttached) return false;

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

                //������ ����ȭ
                Gizmos.color = Color.white;
                Gizmos.DrawCube(worldPos, Vector3.one);
            }
        }

        if(!TryGetWorldPosOnGrid(WorldMousePos, out Vector3 onPos))
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
