using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public BulletMove bullet;
    public int skillLevel = 0;
    public Transform gunPivot;

    float bombCounter = 3;

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        if (GameManager.instance.currentGameState != CurrentGameState.Play) return;
             
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Fire(skillLevel);
        }
        bombCounter = Mathf.Min(bombCounter + deltaTime, 3);
    }

    void Fire(int level)
    {
        switch (level)
        {
            case 0:
                FireSkill1();
                break;
            case 1:
                FireSkill2(); 
                break;
            case 2:
                FireSkill2();
                FireSkill3();
                break;
            case 3:
                FireSkill2();
                FireSkill3();
                FireSkill4();
                break;
        }

        void FireSkill1()
        {
            var currentBullet = Instantiate(bullet);
            currentBullet.transform.position = gunPivot.position;
            currentBullet.transform.up = Vector3.up;
            currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
        }
        void FireSkill2()
        {
            for(int i = 0; i < 2; i++)
            {
                var currentBullet = Instantiate(bullet);
                currentBullet.transform.position = gunPivot.position + Vector3.right * (i == 0 ? 0.25f : -0.25f);
                currentBullet.transform.up = Vector3.up;
                currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
            }
        }
        void FireSkill3()
        {
            for (int i = 0; i < 2; i++)
            {
                var currentBullet = Instantiate(bullet);
                var createPos = gunPivot.position + Vector3.right * (i == 0 ? 0.5f : -0.5f);

                currentBullet.transform.position = createPos;
                currentBullet.GetComponent<Rigidbody>().MovePosition(createPos);
                
                currentBullet.transform.rotation = Quaternion.Euler(0, 0, (i == 0 ? -30 : 30));
                currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);
                currentBullet.reflect = true;
            }
        }
        void FireSkill4()
        {
            int degrees = 15;
            int numOfBullet = 360 / degrees;

            if (bombCounter >= 3)
            {
                for (int i = 0; i < numOfBullet; i++)
                {
                    var currentBullet = Instantiate(bullet);
                    currentBullet.transform.position = transform.position;
                    currentBullet.GetComponent<Rigidbody>().MovePosition(transform.position);
                    currentBullet.speed = 15f;

                    currentBullet.transform.rotation = Quaternion.Euler(0, 0, i * degrees);
                    currentBullet.GetComponent<Rigidbody>().MoveRotation(currentBullet.transform.rotation);

                    currentBullet.reflect = true;
                }
                bombCounter = 0;
            }

        }
    }



}
