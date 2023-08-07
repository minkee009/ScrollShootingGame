using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    //public Transform EnemyTransform;
    public Camera MainCamera;

    public bool playerPosClamp = true;
    float clampX = 0;
    float clampY = 0;
    
    //public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ʈ���� �ִ� ���� ã�� (x ,y)
        var wholeClampPos = MainCamera.ViewportToWorldPoint(Vector3.one);

        // ��ġ ���� �� �缳��
        clampX = Mathf.Abs(wholeClampPos.x - 0.5f);
        clampY = Mathf.Abs(wholeClampPos.y - 0.5f);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.up * v;

        //�̵�
        transform.position += dir * speed * Time.deltaTime;

        if (!playerPosClamp) return;

        if (Mathf.Abs(transform.position.x) > clampX)
        {
            int minus = transform.position.x > 0f ? 1 : -1;

            transform.position = new Vector3(clampX * minus, transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(transform.position.y) > clampY)
        {
            int minus = transform.position.y > 0f ? 1 : -1;

            transform.position = new Vector3(transform.position.x, clampY * minus, transform.position.z);
        }

        //�� �����
        /*Vector3 enemyPos = EnemyTransform != null ? EnemyTransform.position : Vector3.zero;
        Vector3 playerPos = transform.position;

        var toEnemyDir = (enemyPos - playerPos).normalized;
        var toEnemyDist = (enemyPos - playerPos).magnitude;*/
        
        /*Debug.Log("���� : " + toEnemyDir + " | �Ÿ� : " +  toEnemyDist);
        Debug.DrawRay(transform.position, toEnemyDir, Color.yellow);*/

        //lineRenderer.SetPosition(0, playerPos);
        //lineRenderer.SetPosition(1, playerPos + toEnemyDir);
    }
}
