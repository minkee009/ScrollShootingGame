using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    float currentTime;
    public float Speed;
    public Material backGround;


    // Update is called once per frame
    void Update()
    {
        currentTime += Speed * Time.deltaTime;

        backGround.mainTextureOffset = Vector2.up * currentTime;
    }
}
