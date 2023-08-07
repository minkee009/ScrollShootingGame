using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ : ���� �����Ѵ�.
// �ʿ� �Ӽ� : Ư�� �ð�, �� ���ӿ�����Ʈ, ���� �ð�.
// ����1. Ư���ð��� �帥��.
// ����2. ����ð��� Ư���ð��� �Ǹ�
// ����3. ���� �����ؼ� EnemyPos�� �����Ѵ�.

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
