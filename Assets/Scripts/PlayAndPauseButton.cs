using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAndPauseButton : MonoBehaviour
{
    bool _switchMode = false;
    public TMP_Text textMesh;

    public void OnClick()
    {
        _switchMode = !_switchMode;

        if(_switchMode)
        {
            textMesh.text = "Pause";
            GameManager.instance.PlayGame();
        }
        else
        {
            textMesh.text = "Play";
            GameManager.instance.PauseGame();
        }
    }
}
