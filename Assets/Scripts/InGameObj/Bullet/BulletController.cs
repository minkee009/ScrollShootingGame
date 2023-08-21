using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public void Start()
    {
        GameManager.instance.Act_OnGameReset += ForceDestroy;
    }

    public void ForceDestroy()
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        GameManager.instance.Act_OnGameReset -= ForceDestroy;
    }
}
