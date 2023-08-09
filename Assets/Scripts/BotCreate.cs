using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCreate : MonoBehaviour
{
    public GameObject Bot;


    private void Start()
    {
        Destroy(gameObject, 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var pMove = other.GetComponent<PlayerMove>();
            if (pMove.playerBotCount < 1)
            {
                //º¿ »ý¼º
                pMove.playerBotCount += 1;
                var playerBot = GameObject.Instantiate(Bot);
                var botMove = playerBot.GetComponent<BotMove>();
                botMove.Center = other.transform;
                botMove.InitPos();
            }

            Destroy(gameObject);
        }
    }
}
