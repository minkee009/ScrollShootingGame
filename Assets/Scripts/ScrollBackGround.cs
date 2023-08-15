using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    public float Speed;

    float _currentTime;
    MeshRenderer _backGround;

    public void Start()
    {
        _backGround = GetComponent<MeshRenderer>();
        GameManager.instance.Act_OnGameReset += ResetBackGroundOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Speed * Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        _backGround.material.mainTextureOffset = Vector2.up * _currentTime;
        _backGround.material.color = Color.Lerp(_backGround.material.color, new Color(1,1,1,1), 12f * Time.deltaTime);
    }

    public void ResetBackGroundOffset()
    {
        _currentTime = 0;
    }
}
