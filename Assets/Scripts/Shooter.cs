using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject missile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot)
        {
            foreach (Transform t in firePoints)
            {
                var newMissile = Instantiate(missile);
                newMissile.transform.position = t.position;
                newMissile.GetComponent<MissileContoller>().moveDir = t.transform.up;
            }
        }
    }
}
