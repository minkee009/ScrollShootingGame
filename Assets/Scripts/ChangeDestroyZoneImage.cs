using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDestroyZoneImage : MonoBehaviour
{

    public GridSystem gridSystem;

    public Image destroyZoneImage;
    public RectTransform rect;

    public Sprite openBacket;
    public Sprite closeBacket;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, new Vector2(50f, 50f), Time.deltaTime * 6f);
    }

    public void OnEnterPointer()
    {
        if (gridSystem.isEditNodeMode)
        {
            ChangeImage(false);
        }
    }

    public void OnExitPointer()
    {
        if (gridSystem.isEditNodeMode)
        {
            ChangeImage(true);
        }
    }

    public void ChangeImage(bool close)
    {
        destroyZoneImage.sprite = close ? closeBacket : openBacket;

        rect.sizeDelta = new Vector2(60, 60);
    }
}
