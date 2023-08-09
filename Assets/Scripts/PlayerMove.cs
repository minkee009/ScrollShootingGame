using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    //public Transform EnemyTransform;
    public Camera MainCamera;
    public GameObject DeathEffect;

    public bool playerPosClamp = true;
    public float botTiming = 0;
    public int playerBotCount = 0;

    //public bool playerHasBot = false;

    public Transform rootTransform;
    public float rotateAmount = 12f;

    private float targetRotY = 0;
    private float currentRotY = 0;

    public int hp = 10;

    float clampX = 0;
    float clampY = 0;
    
    //public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 뷰포트에서 최대 지점 찾기 (x ,y)
        var wholeClampPos = MainCamera.ViewportToWorldPoint(Vector3.one);

        // 위치 제한 값 재설정
        clampX = Mathf.Abs(wholeClampPos.x - 0.5f);
        clampY = Mathf.Abs(wholeClampPos.y - 0.5f);

        float h = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        float v = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);

        Vector3 dir = Vector3.right * h + Vector3.up * v;

        //이동
        transform.position += dir * speed * Time.deltaTime;

        if (!playerPosClamp) return;

        if (Mathf.Abs(transform.position.x) > clampX)
        {
            int minus = transform.position.x > 0f ? 1 : -1;

            transform.position = new Vector3(clampX * minus, transform.position.y, transform.position.z);
            h = 0;
        }
        if (Mathf.Abs(transform.position.y) > clampY)
        {
            int minus = transform.position.y > 0f ? 1 : -1;

            transform.position = new Vector3(transform.position.x, clampY * minus, transform.position.z);
        }

        targetRotY = h != 0
            ? (h > 0 ? -rotateAmount : rotateAmount) : 0;

        currentRotY = Mathf.Lerp(currentRotY, targetRotY, 25f * Time.deltaTime);

        rootTransform.localRotation = Quaternion.Euler(0f, currentRotY, 0f);

        botTiming += Time.deltaTime * 50f;
        if(botTiming > 360)
        {
            botTiming = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Item" || other.tag != "BotItem")
        {
            hp--;
            if(hp <= 0)
            {
                Die();
            }
        }
        else
        {
            hp = Mathf.Min(hp + 1,10);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            hp--;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        var currentScore = GameManager.instance.attackScore + GameManager.instance.destroyScore;
        if(currentScore > PlayerPrefs.GetInt("Best Score"))
        {
            PlayerPrefs.SetInt("Best Score", currentScore);
            GameManager.instance.bestScore = currentScore;
        }
        var effect = Instantiate(DeathEffect);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
