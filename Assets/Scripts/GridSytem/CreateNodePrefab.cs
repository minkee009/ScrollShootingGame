using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodePrefab : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject uiButton;
    public NodeMover mover;

    //public GridSystem gridSystem;
    //public ChangeTrashCanImage changeTrashcan;
    bool _readyToCreate;
    //GameObject _currentNode;

    // Update is called once per frame
    void Update()
    {
        var isGamePlaying = GameManager.instance.currentGameState == CurrentGameState.Play;
        uiButton.SetActive(!isGamePlaying);
        if (isGamePlaying) return;

        if (_readyToCreate && Input.GetMouseButtonDown(0))
        {
            CreateNodeInRuntime();
        }
    }

    void CreateNodeInRuntime()
    {
        var currentNodePrefab = Instantiate(nodePrefab);
        currentNodePrefab.transform.position = GameManager.instance.gridSystem.WorldMousePos;
        mover.SelectNodePrefab(currentNodePrefab);
    }

    public void OnMouseEvent()
    {
        _readyToCreate = true;
    }

    public void ExitMouseEvent()
    {
        _readyToCreate = false;
    }
}
