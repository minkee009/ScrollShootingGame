using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotItem : MonoBehaviour
{
    public GameObject botUnit;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.botUnit != null)
        {
            GameManager.instance.botTime = 15f;
            Destroy(gameObject);
            return;
        }

        var playerTransform = other.transform;
        var createPos = other.transform.position;
        var currentBot = Instantiate(botUnit);
        var currentBotMove = currentBot.GetComponent<BotMove>();

        currentBot.transform.position = createPos;
        currentBotMove.Center = playerTransform;
        currentBotMove.InitPos();

        GameManager.instance.botUnit = currentBot;
        GameManager.instance.botTime = 15f;

        Destroy(gameObject);
    }
}
