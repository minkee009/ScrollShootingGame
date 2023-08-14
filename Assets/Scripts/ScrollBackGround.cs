using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    float _currentTime;
    public float Speed;
    public Material backGround;


    // Update is called once per frame
    void Update()
    {
        _currentTime += Speed * Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        backGround.mainTextureOffset = Vector2.up * _currentTime;
    }
}
