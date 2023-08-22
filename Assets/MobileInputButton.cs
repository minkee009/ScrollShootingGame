using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MobileInputButton : MonoBehaviour
{
    public UnityAction buttonAction;

    public void OnClick()
    {
        buttonAction?.Invoke();
    }
}
