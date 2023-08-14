using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MissileMove : MonoBehaviour
{
    public Rigidbody rb;
    public Transform targetTransform;
    public float speed = 15f;
    public float moveSharpness = 8f;

    float _posTimer = 0;
    float _targetDirMag = 0;
    float _randomRange;
    Vector3 _currentVelocity = Vector3.zero;
    Vector3[] _bezierCurve3P = new Vector3[3];

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _randomRange = Random.Range(-5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        if (GameManager.instance.currentGameState == CurrentGameState.Play)
        {
            _posTimer += speed * deltaTime;
            if (targetTransform != null)
            {
                _targetDirMag = (targetTransform.position - transform.position).magnitude;
                _bezierCurve3P[0] = transform.position;
                _bezierCurve3P[1] = transform.position + (targetTransform.position - transform.position) * 0.3f + Vector3.right * _randomRange;
                _bezierCurve3P[2] = targetTransform.position;
                var targetPos = CalculateBezier(_bezierCurve3P, _posTimer / _targetDirMag);
                var targetDir = (targetPos - transform.position).normalized;
                var targetVel = targetDir * speed * deltaTime;

                _currentVelocity = Vector3.Lerp(_currentVelocity, targetVel, moveSharpness * deltaTime);
                transform.up = _currentVelocity.normalized;
            }   
            else
            {
                _currentVelocity = Vector3.Lerp(_currentVelocity, transform.up * speed * deltaTime, moveSharpness * deltaTime);
            }

            transform.position += _currentVelocity;
            rb.Move(transform.position, transform.rotation);
        }
        
    }

    public Vector3 CalculateBezier(Vector3[] Positions, float t)
    {
        var lerpP1 = Vector3.Lerp(Positions[0], Positions[1], t);
        var lerpP2 = Vector3.Lerp(Positions[1], Positions[2], t);

        return Vector3.Lerp(lerpP1, lerpP2, t);
    }
}
