using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObjCreater : MonoBehaviour
{
    public GameObject activeObjPrefab;
    public SpriteRenderer sprite;
    public bool nonRemoveableObj = false;

    protected GameObject _nodeObj;
    protected bool _isFirstCreation = true;

    private void Start()
    {
        GameManager.instance.Act_OnGamePlay += CreateNodeObj;
        GameManager.instance.Act_OnGameReset += ResetNodeObj;
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
        GameManager.instance.Act_OnGamePlay -= CreateNodeObj;
        GameManager.instance.Act_OnGameReset -= ResetNodeObj;
        if (_nodeObj != null)
            Destroy(_nodeObj);
    }

    public virtual void CreateNodeObj()
    {
        if (_isFirstCreation)
        {
            _nodeObj = Instantiate(activeObjPrefab);
            _nodeObj.transform.position = transform.position;
            _isFirstCreation = false;
        }
    }


    public virtual void ResetNodeObj()
    {
        if(_nodeObj != null)
            Destroy(_nodeObj);

        _isFirstCreation = true;
    }
}
