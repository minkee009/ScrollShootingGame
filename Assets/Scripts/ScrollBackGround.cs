using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    float _currentTime;
    public float Speed;
    public Material backGround;

    public void Start()
    {
        GameManager.instance.Act_OnGameReset += ResetBackGroundOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Speed * Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        backGround.mainTextureOffset = Vector2.up * _currentTime;
    }

    public void ResetBackGroundOffset()
    {
        _currentTime = 0;
    }
}
