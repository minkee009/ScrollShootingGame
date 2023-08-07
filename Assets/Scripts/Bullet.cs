using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


// ��ǥ: ���� ���� ���ư���.
// ������ �ʿ��ϴ�.
// �ӵ��� �ʿ��ϴ�.

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f;
    public Vector3 dir = Vector3.up;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null 
            && other.gameObject.layer != this.gameObject.layer 
            && other.gameObject.tag != gameObject.tag)
        {
            if(other.tag != "DangerZone")
            {
                GameObject.Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
