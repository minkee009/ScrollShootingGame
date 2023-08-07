using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 적을 생성한다.
// 필요 속성 : 특정 시간, 적 게임오브젝트, 현재 시간.
// 순서1. 특정시간이 흐른다.
// 순서2. 현재시간이 특정시간이 되면
// 순서3. 적을 생성해서 EnemyPos에 생성한다.

public class EnemyManager : MonoBehaviour
{
    //public float createTime = 5f;
    float currentTime = 0f;

    [Range(1f, 5f)]
    public float maxRandomTime = 1f;
    private float createTime = 0f;

    public Enemy Enemy;
    public Transform Player;
    public Transform[] SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        createTime = Random.Range(0.3f, maxRandomTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= createTime)
        {
            var randomIndex = (int)Random.Range(0f, SpawnPoints.Length);
            var enemyObj = Instantiate(Enemy);
            enemyObj.playerTransform = Player;
            enemyObj.transform.position = SpawnPoints[randomIndex].position;
            enemyObj.GetComponent<Rigidbody>().MovePosition(SpawnPoints[randomIndex].position);
            currentTime = 0f;
            createTime = Random.Range(1f, maxRandomTime);
        }
    }
}
