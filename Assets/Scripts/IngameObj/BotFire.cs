using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFire : MonoBehaviour
{
    public float botFireTiming = 2f;
    public LayerMask targetLayer;
    public MissileMove missile;
    public GameObject fireEffect;

    float _fireTimer = 0f;

    Transform _targetTransform;
    Vector3 _targetPosition;
    Collider[] _targetColliders = new Collider[4];
    
    // Update is called once per frame
    void Update()
    {
        _fireTimer += Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        var targetDir = Vector3.up;

        
        if (_fireTimer > botFireTiming)
        {
            var targetCount = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 50f, _targetColliders, targetLayer);
            var closestDist = Mathf.Infinity;
            _targetTransform = null;

            if(targetCount > 0)
            {
                for (int i = 0; i < targetCount; i++)
                {
                    //Debug.Log((_targetColliders[i].transform.position - transform.position).magnitude);
                    if ((_targetColliders[i].transform.position - transform.position).magnitude < closestDist)
                    {
                        closestDist = (_targetColliders[i].transform.position - transform.position).magnitude;
                        _targetPosition = _targetColliders[i].transform.position;
                        _targetTransform = _targetColliders[i].transform;
                    }
                }

                targetDir = (_targetPosition - transform.position).normalized;
            }

            var currentMissile = Instantiate(missile);
            currentMissile.transform.position = transform.position;
            currentMissile.transform.up = targetDir;
            currentMissile.GetComponent<Rigidbody>().Move(currentMissile.transform.position, currentMissile.transform.rotation);
            currentMissile.targetTransform = _targetTransform;

            var currentEffect = Instantiate(fireEffect);
            currentEffect.transform.position = transform.position; 

            _fireTimer = 0f;
        }
    }
}
