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

    GameObject _instance;

    public void CreateInPlay()
    {
        if (_instance != null) return;
        _instance = GameObject.Instantiate(activePrefab);
        _instance.transform.position = pivotTransform != null ? pivotTransform.position : createPos;

        if (nodeProps != null && nodeProps.Length > 0)
        {
            foreach(var prop in nodeProps)
            {
                prop.RemovecomponentForInstance(_instance);
                prop.AddcomponentForInstance(_instance);
            }
        }
        
        GameManager.instance.Act_OnGameReset += DestroyInstance;
    }

    public void DestroyInstance()
    {
        GameManager.instance.Act_OnGameReset -= DestroyInstance;
        GameObject.Destroy(_instance);
    }
}
