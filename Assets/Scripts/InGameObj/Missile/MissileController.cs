using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public GameObject trailEffect;

    public void Start()
    {
        GameManager.instance.Act_OnGameReset += ForceDestroy;
    }

    private void Update()
    {
        bool isDestroyPos = Mathf.Abs(transform.position.y) > 20f || Mathf.Abs(transform.position.x) > 30f;

        if(isDestroyPos)
        {
            SoftDestroy();
        }
    }

    public void ForceDestroy()
    {
        Destroy(gameObject);
    }

    public void SoftDestroy()
    {
        Destroy(trailEffect, 1f);
        trailEffect.transform.parent = null;

        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        GameManager.instance.Act_OnGameReset -= ForceDestroy;
    }
}
