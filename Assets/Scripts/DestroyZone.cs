using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.currentGameState != CurrentGameState.Play 
            || other.CompareTag("Missile")) return;

        Destroy(other.gameObject);
    }
}
