using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CurrentGameState { Pause = 0, Play }

public class GameManager : MonoBehaviour
{
    //싱글톤 객체
    public static GameManager instance;

    [Header("오브젝트 초기화 설정")]
    public Transform playerTransform;
    public Vector2 playerInitPos;
    public Vector2 enemyCreaterInitPos;
    public GameObject enemyCreaterPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [HideInInspector]
    public GameObject botUnit = null;

    [Header("드론 봇 설정")]
    public float botTime = 15f;

    [Header("시스템 설정")]
    public GridSystem gridSystem;
    public Camera mainCam;
    public GameObject[] horizontalLetterBoxes;

    [Range(0f, 2f)]
    public float inGameTimeSpeed = 1.0f;
    public CurrentGameState currentGameState;

    public UnityAction Act_OnGamePlay;
    public UnityAction Act_OnGamePause;
    public UnityAction Act_OnGameReset;

    float _targetViewSize = 0f;

    private void Awake()
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

    private void Start()
    {
        if (gridSystem != null)
        {
            gridSystem.CreateGrid();
            Node playerNode = gridSystem.GetNodeInGrid(playerInitPos);
            var player = Instantiate(playerPrefab);
            gridSystem.TryAttachObjToNode(playerNode, player);
            PauseGame();
        }
        else
        {
            PlayGame();
        }

    }

    private void Update()
    {
        mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, _targetViewSize, Time.deltaTime * 18f);

        if (botUnit != null)
        {
            botTime -= Time.deltaTime * inGameTimeSpeed;

            if(botTime < 0f)
            {
                botTime = 15f;
                Destroy(botUnit);
                botUnit = null; 
            }
        }
    }

    public void ResetGame()
    {
        Act_OnGameReset?.Invoke();

        //봇삭제
        botTime = 15f;
        Destroy(botUnit);
        botUnit = null;

        PauseGame();
    }

    public void PlayGame()
    {
        Act_OnGamePlay?.Invoke();
        inGameTimeSpeed = 1.0f;
        currentGameState = CurrentGameState.Play;
        mainCam.clearFlags = CameraClearFlags.SolidColor;
        foreach (var box in horizontalLetterBoxes)
        {
            box.SetActive(true);
        }
        _targetViewSize = 10;
    }

    public void PauseGame()
    {
        Act_OnGamePause?.Invoke();
        inGameTimeSpeed = 0.0f;
        currentGameState = CurrentGameState.Pause;
        mainCam.clearFlags = CameraClearFlags.Skybox;
        foreach (var box in horizontalLetterBoxes)
        {
            box.SetActive(false);
        }
        _targetViewSize = 12;
    }
}
