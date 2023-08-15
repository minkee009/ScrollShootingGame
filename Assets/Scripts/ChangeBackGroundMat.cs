using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackGroundMat : MonoBehaviour
{
    public Material[] backgroundMats;

    public MeshRenderer currentBackGround;
    public MeshRenderer lastBackGround;

    public int selectedBGIndex = 0;

    private void Start()
    {
        SwitchBG();
    }

    public void SetBG1()
    {
        lastBackGround.material = currentBackGround.material;
        currentBackGround.material = backgroundMats[0];
        currentBackGround.material.color = new Color(1,1,1,0);
        selectedBGIndex = 0;
    }

    public void SetBG2()
    {
        lastBackGround.material = currentBackGround.material;
        currentBackGround.material = backgroundMats[1];
        currentBackGround.material.color = new Color(1, 1, 1, 0);
        selectedBGIndex = 1;
    }

    public void SetBG3()
    {
        lastBackGround.material = currentBackGround.material;
        currentBackGround.material = backgroundMats[2];
        currentBackGround.material.color = new Color(1, 1, 1, 0);
        selectedBGIndex = 2;
    }

    public void SwitchBG()
    {
        switch (selectedBGIndex)
        {
            default:
            case 0:
                SetBG1();
                break;
            case 1:
                SetBG2();
                break;
            case 2:
                SetBG3();
                break;
        }
    }
}
