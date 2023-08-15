using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideButton : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.Act_OnGamePlay += HideObj;
        GameManager.instance.Act_OnGamePause += DisplayObj;
    }

    public void HideObj()
    {
        gameObject.SetActive(false);
    }

    public void DisplayObj()
    {
        gameObject.SetActive(true);
    }
}
