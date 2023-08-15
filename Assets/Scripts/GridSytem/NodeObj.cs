using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObj : MonoBehaviour
{
    public GameObject activeObjPrefab;
    public SpriteRenderer sprite;
    public bool nonRemoveAbleObj = false;
    public bool combineAbleObj = false;

    protected GameObject _myObject;
    protected bool _isFirstCreation = true;

    private void Start()
    {
        GameManager.instance.Act_OnGamePlay += CreateObj;
        GameManager.instance.Act_OnGameReset += ResetObj;
    }

    private void Update()
    {
        switch (GameManager.instance.currentGameState)
        {
            case CurrentGameState.Play:
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.0f);
                break;
            case CurrentGameState.Pause:
                if (_isFirstCreation)
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
                }
                else
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.Act_OnGamePlay -= CreateObj;
        GameManager.instance.Act_OnGameReset -= ResetObj;
        if (_myObject != null)
            Destroy(_myObject);
    }

    public virtual void CreateObj()
    {
        if (_isFirstCreation)
        {
            _myObject = Instantiate(activeObjPrefab);
            _myObject.transform.position = transform.position;
            _isFirstCreation = false;
        }
    }


    public virtual void ResetObj()
    {
        if(_myObject != null)
            Destroy(_myObject);

        _isFirstCreation = true;
    }

    public virtual bool TryCombineOtherNodeObj(GameObject other)
    {
        return false;
    }
}
