using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MissileContoller : MonoBehaviour
{
    public float moveSpeed = 15f;
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime,Space.Self);
    }

}
