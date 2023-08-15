using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        var myHit = GetComponent<HitableObj>();

        myHit.OnDie += SetBestScoreOnDie;
    }

    void SetBestScoreOnDie()
    {
        var currentScore = GameManager.instance.attackScore + GameManager.instance.destroyScore;
        if (currentScore > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", currentScore);
            GameManager.instance.bestScore = currentScore;
        }
    }
}
