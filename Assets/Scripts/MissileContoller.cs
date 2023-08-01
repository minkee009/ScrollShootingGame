using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MissileContoller : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Vector3 moveDir;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

}
