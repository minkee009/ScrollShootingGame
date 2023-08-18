using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjPreset
{
    public GameObject activePrefab;
    public NodeProp[] nodeProps;
    public Transform pivotTransform;
    public Vector3 createPos;
    //public string typeName;

    GameObject _instance;

    public void CreateInPlay()
    {
        _instance = GameObject.Instantiate(activePrefab);
        _instance.transform.position = pivotTransform != null ? pivotTransform.position : createPos;

        if (nodeProps != null && nodeProps.Length > 0)
        {
            foreach(var n in nodeProps)
            {
                n.RemovecomponentForInstance(_instance);
                n.AddcomponentForInstance(_instance);
            }
        }

        GameManager.instance.Act_OnGameReset += DestroyInstance;
    }

    public void DestroyInstance()
    {
        GameObject.Destroy(_instance);
    }
}
