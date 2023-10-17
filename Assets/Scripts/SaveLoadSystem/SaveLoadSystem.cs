using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

public class SaveLoadSystem : MonoBehaviour
{
    public GridSystem gridSys;
    public ChangeBackGroundMat bgMat;

    public GameObject[] nodePrefabs;

    Dictionary<string, GameObject> _nodePrefabsDict;

    private void Awake()
    {
        _nodePrefabsDict = new Dictionary<string, GameObject>();
        foreach (var info in nodePrefabs)
        {
            if(info.TryGetComponent(out NodeObj obj))
            {
                _nodePrefabsDict.Add(obj.typeName, info.gameObject);
            }
            else if(info.TryGetComponent(out INodeProp prop))
            {
                _nodePrefabsDict.Add(prop.TypeName, info.gameObject);
            }
        }
    }

    public void SaveGameData()
    {
        GameData currentData = new GameData();
        currentData.nodeDatas = new List<NodeInfo>(gridSys.AttachedNodes.Count);
        currentData.bgIndex = bgMat.selectedBGIndex;

        foreach (var node in gridSys.AttachedNodes.Keys)
        {
            NodeInfo currentInfo = new NodeInfo();
            var currentNodeObj = node.attachedObject.GetComponent<NodeObj>();

            currentInfo.gridIndex = gridSys.AttachedNodes[node];
            currentInfo.typeName = currentNodeObj.typeName;
            currentInfo.propTypeName = currentNodeObj.TryGetComponent(out INodeProp cProp) ? cProp.TypeName : "";

            switch (currentNodeObj.typeName)
            {
                case "Enemy":
                    currentInfo.presets = new string[1];
                    EnemyNodeObj enemyObj = currentNodeObj as EnemyNodeObj;
                    currentInfo.presets[0] = enemyObj.ItemObjPreset == null ? "" : enemyObj.ItemObjPreset.typeName;

                    currentInfo.presetProps = new string[1];
                    currentInfo.presetProps[0] = enemyObj.ItemObjPreset != null && enemyObj.ItemObjPreset.nodeProps.Length > 0 ? enemyObj.ItemObjPreset.nodeProps[0].TypeName : "" ;
                    break;

                case "ObjectCreator":
                    currentInfo.presets = new string[2];
                    ObjectCreatorNodeObj objectCreatorNodeObj = currentNodeObj as ObjectCreatorNodeObj;
                    currentInfo.presets[0] = objectCreatorNodeObj.CreateObjPreset == null ? "" : objectCreatorNodeObj.CreateObjPreset.typeName;
                    currentInfo.presets[1] = objectCreatorNodeObj.EnemysChildPreset == null ? "" : objectCreatorNodeObj.EnemysChildPreset.typeName;

                    currentInfo.presetProps = new string[2];
                    currentInfo.presetProps[0] = objectCreatorNodeObj.CreateObjPreset != null && objectCreatorNodeObj.CreateObjPreset.nodeProps.Length > 0 ? objectCreatorNodeObj.CreateObjPreset.nodeProps[0].TypeName : "";
                    currentInfo.presetProps[1] = objectCreatorNodeObj.EnemysChildPreset != null && objectCreatorNodeObj.EnemysChildPreset.nodeProps.Length > 0 ? objectCreatorNodeObj.EnemysChildPreset.nodeProps[0].TypeName : "";
                    break;
            }

            currentData.nodeDatas.Add(currentInfo);
        }

        string path = Path.Combine(Application.dataPath, "SSMGameData");
        string jsonData = JsonUtility.ToJson(currentData);

        File.WriteAllText(path, jsonData);
    }

    public void LoadGameData()
    {
        string path = Path.Combine(Application.dataPath, "SSMGameData");
        string jsonText = File.ReadAllText(path);

        gridSys.ClearAllNodeInGridMap();

        GameData loadData = JsonUtility.FromJson<GameData>(jsonText);

        bgMat.selectedBGIndex = loadData.bgIndex;
        bgMat.SwitchBG();

        foreach(var nodeInfo in loadData.nodeDatas)
        {
            //노드오브젝트 생성
            var createObj = Instantiate(_nodePrefabsDict[nodeInfo.typeName]);
            if(nodeInfo.propTypeName != "")
            {
                var createObjProp = Instantiate(_nodePrefabsDict[nodeInfo.propTypeName]);
                createObjProp.GetComponent<INodeProp>().TryCombineOtherNodeObj(createObj.GetComponent<NodeObj>());
                Destroy(createObjProp.gameObject);
            }

            //차일드프리셋 생성
            switch (nodeInfo.typeName)
            {
                case "Enemy":
                    if (nodeInfo.presets[0] == "")
                    {
                        break;
                    }

                    var enemyChildObj = Instantiate(_nodePrefabsDict[nodeInfo.presets[0]]);
                    if (nodeInfo.presetProps[0] != "")
                    {
                        var enemyChildObjProp = Instantiate(_nodePrefabsDict[nodeInfo.presetProps[0]]);
                        enemyChildObjProp.GetComponent<INodeProp>().TryCombineOtherNodeObj(enemyChildObj.GetComponent<NodeObj>());
                        Destroy(enemyChildObjProp.gameObject);
                    }

                    createObj.GetComponent<NodeObj>().TryCombineOtherNodeObj(enemyChildObj.GetComponent<NodeObj>());
                    Destroy(enemyChildObj.gameObject);

                    break;
                case "ObjectCreator":
                    if (nodeInfo.presets[0] == "")
                    {
                        break;
                    }
                    var OCChildObj = Instantiate(_nodePrefabsDict[nodeInfo.presets[0]]);
                    if (nodeInfo.presetProps[0] != "")
                    {
                        var OCChildObjProp = Instantiate(_nodePrefabsDict[nodeInfo.presetProps[0]]);
                        OCChildObjProp.GetComponent<INodeProp>().TryCombineOtherNodeObj(OCChildObj.GetComponent<NodeObj>());
                        Destroy(OCChildObjProp.gameObject);
                    }

                    if (nodeInfo.presets[0] == "Enemy" && nodeInfo.presets[1] != "")
                    {
                        var OCChildObj2 = Instantiate(_nodePrefabsDict[nodeInfo.presets[1]]);
                        if (nodeInfo.presetProps[1] != "")
                        {
                            var OCChildObjProp = Instantiate(_nodePrefabsDict[nodeInfo.presetProps[1]]);
                            OCChildObjProp.GetComponent<INodeProp>().TryCombineOtherNodeObj(OCChildObj2.GetComponent<NodeObj>());
                            Destroy(OCChildObjProp.gameObject);
                        }
                        OCChildObj.GetComponent<NodeObj>().TryCombineOtherNodeObj(OCChildObj2.GetComponent<NodeObj>());
                        Destroy(OCChildObj2.gameObject);
                    }

                    createObj.GetComponent<NodeObj>().TryCombineOtherNodeObj(OCChildObj.GetComponent<NodeObj>());
                    Destroy(OCChildObj.gameObject);
                    break;
            }

            gridSys.TryAttachObjToNode(gridSys.GetNodeInGrid(nodeInfo.gridIndex), createObj);
        }
    }

    [Serializable]
    public class NodeInfo
    {
        //기본 정보
        public int[] gridIndex;
        public string typeName;
        public string propTypeName;

        //자식 정보
        public string[] presets;
        public string[] presetProps;
    }

    [Serializable]
    public class GameData
    {
        public int bgIndex = 0;
        public List<NodeInfo> nodeDatas;
    }
    
}
