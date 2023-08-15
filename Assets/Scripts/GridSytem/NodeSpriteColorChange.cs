using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpriteColorChange : MonoBehaviour
{
    public SpriteRenderer sprite;
    
    public void SetAlphaToZero()
    {
        sprite.color = new Color(1,1,1,0);
    }

    public void SetAlphaToHalf()
    {
        sprite.color = new Color(1,1,1,0.5f);
    }

    public void RegistColorChangeToAction()
    {
        GameManager.instance.Act_OnGamePlay += SetAlphaToZero;
        GameManager.instance.Act_OnGamePause += SetAlphaToHalf;
    }

    public void ReleaseColorChangeFromAction()
    {
        GameManager.instance.Act_OnGamePlay -= SetAlphaToZero;
        GameManager.instance.Act_OnGamePause -= SetAlphaToHalf;
    }
}
