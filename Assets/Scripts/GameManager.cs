using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CurrentGameState { Pause = 0, Play }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Range(0f,2f)]
    public float inGameTimeSpeed = 1.0f;

    public Vector2 playerInitPos;
    public GridSystem gridSystem;

    public GameObject enemyCreaterPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform PlayerTransform;

    public CurrentGameState currentGameState;

    public UnityAction Act_OnGamePlay;
    public UnityAction Act_OnGamePause;
    public UnityAction Act_OnGameReset;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Start()
    {
        if (gridSystem != null)
        {
            gridSystem.CreateGrid();
            Node playerNode = gridSystem.GetNodeInGrid(playerInitPos);
            var player = Instantiate(playerPrefab);
            PlayerTransform = player.transform;
            gridSystem.TryAttachObjToNode(playerNode, player);
            Act_OnGameReset += gridSystem.ResetAllObjPosInNode;
        }

        PauseGame();
    }

    public void ResetGame()
    {
        Act_OnGameReset?.Invoke();
        currentGameState = CurrentGameState.Pause;
    }

    public void PlayGame()
    {
        Act_OnGamePlay?.Invoke();
        inGameTimeSpeed = 1.0f;
        currentGameState = CurrentGameState.Play;
    }

    public void PauseGame()
    {
        Act_OnGamePause?.Invoke();
        inGameTimeSpeed = 0.0f;
        currentGameState = CurrentGameState.Pause;
    }
}
