using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 사용자 입력(스페이스)를 받아 총알을 만들고 싶다.
public class PlayerFire : MonoBehaviour
{
    public Bullet myBullet;
    public Transform gunPos;
    int skillLevel = 0;
    float bombCounter = 0;

    //public Transform EnemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        bombCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExcuteSkill(skillLevel);
        }
        bombCounter = Mathf.Min(3, bombCounter + Time.deltaTime);
    }

    private void ExcuteSkill(int _skillLevel)
    {
        switch (_skillLevel)
        {
            case 0:
                ExcuteSkill1();
                break;
            case 1:
                ExcuteSkill2();
                break;
            case 2:
                ExcuteSkill3();
                break;
            case 3:
                ExcuteSkill3();
                ExcuteSkill4();
                break;
        }

        void ExcuteSkill1()
        {
            var currentBullet = Instantiate(myBullet);
            currentBullet.transform.position = gunPos.position;
            currentBullet.GetComponent<Rigidbody>().MovePosition(gunPos.position);

            currentBullet.tag = "Player";
            currentBullet.speed = 15f;
        }

        void ExcuteSkill2()
        {
            for (int i = 0; i < 2; i++)
            {
                var currentBullet = Instantiate(myBullet);
                var createPos = gunPos.position + Vector3.right * (i == 0 ? 0.25f : -0.25f);
                currentBullet.transform.position = createPos;
                currentBullet.GetComponent<Rigidbody>().MovePosition(createPos);

                currentBullet.tag = "Player";
                currentBullet.speed = 15f;
            }
        }

        void ExcuteSkill3()
        {
            for (int i = 0; i < 2; i++)
            {
                var currentBullet = Instantiate(myBullet);
                var createPos = gunPos.position + Vector3.right * (i == 0 ? 0.25f : -0.25f);
                currentBullet.transform.position = createPos;
                currentBullet.GetComponent<Rigidbody>().MovePosition(createPos);

                currentBullet.tag = "Player";
                currentBullet.speed = 15f;
            }

            for (int i =0; i < 2; i++)
            {
                var currentBullet = Instantiate(myBullet);
                var createPos = gunPos.position + Vector3.right * (i == 0 ? 0.5f : -0.5f);
                currentBullet.transform.position = createPos;
                currentBullet.GetComponent<Rigidbody>().MovePosition(createPos);
                currentBullet.transform.rotation = Quaternion.Euler(0, 0, (i == 0 ? -30 : 30));
                currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
                currentBullet.reflect = true;
            }

            

            /*for (int i = 0; i < 3; i++)
            {
                var currentBullet = Instantiate(myBullet);
                var createPos = gunPos.position + Vector3.right * (-0.5f + i * 0.5f);
                currentBullet.transform.position = createPos;
                currentBullet.GetComponent<Rigidbody>().MovePosition(createPos);

                if(i == 0)
                {
                    currentBullet.transform.rotation = Quaternion.Euler(0, 0, 30);
                    currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
                    currentBullet.reflect = true;
                }
                if (i == 2)
                {
                    currentBullet.transform.rotation = Quaternion.Euler(0, 0, -30);
                    currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
                    currentBullet.reflect = true;
                }

                currentBullet.tag = "Player";
                currentBullet.speed = 15f;
            }*/
        }

        void ExcuteSkill4()
        {
            int degrees = 15;
            int numOfBullet = 360 / degrees;

            if(bombCounter >= 3)
            {
                for (int i = 0; i < numOfBullet; i++)
                {
                    var currentBullet = Instantiate(myBullet);
                    currentBullet.transform.position = transform.position;
                    currentBullet.GetComponent<Rigidbody>().MovePosition(transform.position);

                    currentBullet.tag = "Player";
                    currentBullet.speed = 15f;

                    currentBullet.transform.rotation = Quaternion.Euler(0, 0, i * degrees);
                    currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);

                    currentBullet.reflect = true;
                }
                bombCounter = 0;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Item")
        {
            skillLevel++;
            Destroy(other.gameObject);
        }
    }
}
