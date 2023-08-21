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
    public string typeName;

    public GameObject instance;

    public void CreateInPlay()
    {
        if (instance != null) return;
        instance = GameObject.Instantiate(activePrefab);
        instance.transform.position = pivotTransform != null ? pivotTransform.position : createPos;

        if (nodeProps != null && nodeProps.Length > 0)
        {
            foreach(var prop in nodeProps)
            {
                prop.AddcomponentForInstance(instance);
            }
        }
        
        GameManager.instance.Act_OnGameReset += DestroyInstance;
    }

    public void DestroyInstance()
    {
        GameManager.instance.Act_OnGameReset -= DestroyInstance;
        GameObject.Destroy(instance);
    }
}
