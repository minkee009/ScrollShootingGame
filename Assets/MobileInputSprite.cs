using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MobileInputSprite : MonoBehaviour
{
    public bool checkChild = true;

    Image image;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        if(checkChild)
            transform.GetChild(0).TryGetComponent(out text);

        HideSprite();

        GameManager.instance.Act_OnGamePlay += ShowSprite;
        GameManager.instance.Act_OnGamePause += HideSprite;
    }

    public void ShowSprite()
    {
        image.color = new Color(1,1,1, 0.5f);
        if (text != null)
        {
            text.color = new Color(0, 0, 0, 0.5f);
        }
    }

    public void HideSprite()
    {
        image.color = new Color(1, 1, 1, 0f);
        if (text != null)
        {
            text.color = new Color(0, 0, 0,0f);
        }
    }
}
