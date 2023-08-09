using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 목적 : 플레이어가 쌓은 점수(타격점수, 최고점수, 적 파괴점수)를 저장
public class GameManager : MonoBehaviour
{
    public int attackScore = 0;
    public int destroyScore = 0;
    public int bestScore = 0;

    public TMP_Text attackScoreMesh;
    public TMP_Text destroyScoreMesh;
    public TMP_Text bestScoreMesh;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        attackScoreMesh.text = "0";
        destroyScoreMesh.text = "0";
        bestScore = PlayerPrefs.GetInt("Best Score");
        bestScoreMesh.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        attackScoreMesh.text = attackScore.ToString();
        destroyScoreMesh.text = destroyScore.ToString();
        bestScoreMesh.text = bestScore.ToString();
    }
}
