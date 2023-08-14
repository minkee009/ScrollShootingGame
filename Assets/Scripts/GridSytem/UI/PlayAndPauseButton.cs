using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAndPauseButton : MonoBehaviour
{
    public TMP_Text textMesh;

    bool _switchMode = false;

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
