using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : MonoBehaviour
{
    public float BotAttackTiming = 2f;
    public LayerMask EnemyMask;
    public Missile myMissile;
    public GameObject smokeEffect;

    Transform enemyT;
    Vector3 enemyPos;
    float attackTime = 0;
    Collider[] colliders = new Collider[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTime += Time.deltaTime;

        var enemyDir = Vector3.up;

        if (attackTime > BotAttackTiming)
        {
            var enemyCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 50f, colliders, EnemyMask);
            var closestDist = 20f;
            Transform enemyTransform = null;

            if (enemyCount > 0)
            {
                for(int i = 0; i < enemyCount; i++)
                {
                    //Debug.Log((colliders[i].transform.position - transform.position).magnitude);
                    if ((colliders[i].transform.position - transform.position).magnitude < closestDist)
                    {
                        closestDist = (colliders[i].transform.position - transform.position).magnitude;
                        enemyPos = colliders[i].transform.position;
                        enemyTransform = colliders[i].transform;
                    }
                }

                enemyDir = (enemyPos - transform.position).normalized;
            }

            var currentMissile = Instantiate(myMissile);
            currentMissile.transform.position = transform.position;
            currentMissile.GetComponent<Rigidbody>().MovePosition(transform.position);
            currentMissile.transform.up = enemyDir;
            currentMissile.GetComponent<Rigidbody>().MoveRotation(currentMissile.transform.rotation);
            currentMissile.EnemyTransform = enemyTransform;
            currentMissile.speed = 15f;

            var smokeFx = Instantiate(smokeEffect);
            smokeFx.transform.position = transform.position;

            attackTime = 0f;
        }
            
      

        /*if (attackTime > BotAttackTiming)
        {
            var closestDist = Mathf.Infinity;
            if (Physics.OverlapSphereNonAlloc(gameObject.transform.position, 16f,colliders,EnemyMask) > 1)
            {
               
                foreach(var c in colliders)
                {
                    Debug.Log((c.transform.position - transform.position).magnitude);
                    if(closestDist > (c.transform.position - transform.position).magnitude)
                    {
                        closestDist = (c.transform.position - transform.position).magnitude;
                        enemyT = c.transform;
                    }
                }

                enemyDir = (enemyT.position - transform.position).normalized;
            }

            var currentBullet = Instantiate(Missile);
            currentBullet.transform.position = transform.position;
            currentBullet.GetComponent<Rigidbody>().MovePosition(transform.position);
            currentBullet.transform.up = enemyDir;
            currentBullet.tag = "Player";
            currentBullet.speed = 45f;

            attackTime = 0f;
        }*/
    }
}
