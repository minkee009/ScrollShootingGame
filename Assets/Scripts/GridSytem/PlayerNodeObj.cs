using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNodeObj : NodeObj
{
    VariableJoystick _joystick;
    MobileInputButton _aButton;
    MobileInputButton _bButton;

    public void Start()
    {
#if UNITY_ANDROID
        /*_joystick = GameObject.Find("Variable Joystick").GetComponent<VariableJoystick>();
        _aButton = GameObject.Find("A Button").GetComponent<MobileInputButton>();
        _bButton = GameObject.Find("B Button").GetComponent<MobileInputButton>();*/
#endif
        GameManager.instance.Act_OnGamePlay += CreateObj;
        GameManager.instance.Act_OnGameReset += ResetObj;
    }

    public override void CreateObj()
    {
        if (_isFirstCreation)
        {
            _myObject = Instantiate(activeObjPrefab);
            _myObject.transform.position = transform.position;
            _isFirstCreation = false;

#if UNITY_ANDROID
            /*var playerMove = _myObject.GetComponent<PlayerMove>();
            playerMove.joystick = _joystick;
            playerMove.bButton = _bButton;

            var playerFire = _myObject.GetComponent<PlayerFire>();
            playerFire.aButton = _aButton;*/
#endif

            GameManager.instance.playerTransform = _myObject.transform;
        }
    }

    public override void ResetObj()
    {
        if (_myObject != null)
            Destroy(_myObject);

        _isFirstCreation = true;
        var gridIndex = GameManager.instance.gridSystem.GetGridMapIndex(transform.position);
        GameManager.instance.playerInitPos = new Vector2(gridIndex[0] + 1, gridIndex[1] + 1);
    }
}
