using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("이동 속성")]
    public Rigidbody rb;
    public BoxCollider box;
    public float speed = 5f;
    public float moveSharpness = 12f;

    [Header("회전 관련")]
    public Transform rootPivot;
    public float rotateAmount = 24f;

    public float dodgeTime = 0.2f;
    public float dodgeReadyTime = 2f;
    public float dogeReadyTimer { get; private set; }

    [Header("위치 제한")]
    public bool ClampPosXY = false;

    [ConditionalHide("ClampPosXY", true)]
    public float ClampX = 0;

    [ConditionalHide("ClampPosXY", true)]
    public float ClampY = 0;

    //프라이빗 멤버
    float _targetRotY = 0;
    float _currentRotY = 0;
    float _currentH = 0;
    Vector3 _currentVelocity = Vector3.zero;
    float _dogeTimer = 0;
    bool _isDodge = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        dogeReadyTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        //입력
        float h = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        float v = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);

        //회피 입력
        if (!_isDodge && Mathf.Abs(h) > 0 && dogeReadyTimer >= dodgeReadyTime && Input.GetKeyDown(KeyCode.X))
        {
            box.enabled = false;
            _isDodge = true;
            _dogeTimer = 0;
            _currentH = h;
        }

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        dir = dir.normalized;

        dir = _isDodge ? new Vector3(0f,dir.y,0f) + Vector3.right * (_currentH * 4 * (0.2f / dodgeTime)) : dir;

        //속도 계산
        if(GameManager.instance.currentGameState == CurrentGameState.Play) 
        {
            _currentVelocity = Vector3.Lerp(_currentVelocity, speed * deltaTime * dir, moveSharpness * deltaTime);
            transform.position += _currentVelocity;
        }
            

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
                //v = 0;
            }
        }
        
        //회전 설정
        _targetRotY = h != 0
            ? (h > 0 ? -rotateAmount : rotateAmount) : 0;

        if (_isDodge)
        {
            var minus = _currentH > 0 ? -1 : 1;
            _dogeTimer += deltaTime;
            _targetRotY = 360 * (_dogeTimer / dodgeTime) * minus;

            if(_dogeTimer / dodgeTime > 1)
            {
                box.enabled = true;
                _isDodge = false;
                dogeReadyTimer = 0;
                _currentRotY = _currentRotY - (360*minus);
            }
        }
        else
        {
            dogeReadyTimer = Mathf.Min(3, dogeReadyTimer + deltaTime);
        }

        _currentRotY = Mathf.Lerp(_currentRotY, _targetRotY, 25f * deltaTime);

        rootPivot.rotation = Quaternion.Euler(0f, _currentRotY, 0f);

        //강체 오류 방지
        rb.MovePosition(transform.position);
    }
}
