using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveLoadSystem : MonoBehaviour
{
    public GridSystem gridSys;
    public ChangeBackGroundMat bgMat;

    public GameObject[] nodePrefabs;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        NodeObj[] nodeObjs = new NodeObj[gridSys.AttachedNodes.Count];
        var index = 0;
        foreach(var node in gridSys.AttachedNodes.Keys)
        {
            nodeObjs[index++] = node.attachedObject.GetComponent<NodeObj>();
        }
        index = 0;
        Debug.Log(nodeObjs.Length);

        foreach (var obj in nodeObjs)
        {
            var prop = obj.GetComponents<INodeProp>();
            if(prop.Length > 0)
            {
                Debug.Log("프롭있음");
            }
            else
            {
                Debug.Log("프롭 없음!");
            }

            switch (obj.typeName)
            {
                default:
                    Debug.Log($"{obj.typeName}");
                    break;
                case "Enemy":
                    EnemyNodeObj enemy = (EnemyNodeObj)obj;
                    Debug.Log($"{obj.typeName} : {(enemy.ItemObjPreset is null ? "null" : enemy.ItemObjPreset.typeName)} : {(enemy.ItemObjPreset.nodeProps is null ? "null" : enemy.ItemObjPreset.nodeProps[0].TypeName)}");
                    
                    break;
                case "ObjectCreator":
                    ObjectCreatorNodeObj objectCreator = (ObjectCreatorNodeObj)obj;
                    Debug.Log($"{obj.typeName} : {(objectCreator.CreateObjPreset is null ? "null" : objectCreator.CreateObjPreset.typeName)} : {(objectCreator.EnemysChildPreset is null ? "null" : objectCreator.EnemysChildPreset.typeName)}");
                    break;
            }
            
        }
    }

    public class NodeData
    {
        public string typeName;
        public string[] propTypeNames;
        public string[] childObjTypeNames;
    }
}
