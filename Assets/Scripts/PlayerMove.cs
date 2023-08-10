using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("이동 속성")]
    public Rigidbody rb;
    public float speed = 5f;
    public float moveSharpness = 12f;

    [Header("회전 관련")]
    public Transform rootTransform;
    public float rotateAmount = 24f;

    [Header("위치 제한")]
    public bool ClampPosXY = false;

    [ConditionalHide("ClampPosXY", true)]
    public float ClampX = 0;

    [ConditionalHide("ClampPosXY", true)]
    public float ClampY = 0;


    //프라이빗 멤버
    float targetRotY = 0;
    float currentRotY = 0;
    Vector3 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //입력
        float h = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        float v = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        dir = dir.normalized;

        //속도 계산
        currentVelocity = Vector3.Lerp(currentVelocity, dir * speed * Time.deltaTime * GameManager.instance.inGameTimeSpeed, moveSharpness * Time.deltaTime * GameManager.instance.inGameTimeSpeed);

        transform.position += currentVelocity;

        //위치 제한
        if (ClampPosXY)
        {
            if (Mathf.Abs(transform.position.x) > ClampX)
            {
                var minus = transform.position.x > 0 ? 1 : -1;

                transform.position = new Vector3(minus * ClampX, transform.position.y, transform.position.z);

                h = 0;
            }

            if (Mathf.Abs(transform.position.y) > ClampY)
            {
                var minus = transform.position.y > 0 ? 1 : -1;

                transform.position = new Vector3(transform.position.x, minus * ClampY, transform.position.z);
                v = 0;
            }
        }
        
        //회전 설정
        targetRotY = h != 0
            ? (h > 0 ? -rotateAmount : rotateAmount) : 0;

        currentRotY = Mathf.Lerp(currentRotY, targetRotY, 25f * Time.deltaTime * GameManager.instance.inGameTimeSpeed);

        rootTransform.localRotation = Quaternion.Euler(0f, currentRotY, 0f);

        //강체 오류 방지
        rb.MovePosition(transform.position);
    }
}
