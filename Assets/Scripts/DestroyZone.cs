using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DangerZone" || other.tag == "Player" 
            || other.tag == "Missile")
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
