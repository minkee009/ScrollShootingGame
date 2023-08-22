using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public int skillLevel = 0;
    public Transform gunPivot;
    public MobileInputButton aButton;

    float _bombCounter = 3;

    private void Start()
    {
        if(aButton != null)
         aButton.buttonAction += Fire;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        if (GameManager.instance.currentGameState != CurrentGameState.Play) return;
             
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Fire();
        }
        _bombCounter = Mathf.Min(_bombCounter + deltaTime, 3);
    }

    void Fire()
    {
        switch (skillLevel)
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
            if(BulletManager.instance.playerBulletPool.Count > 0)
            {
                var currentBullet = BulletManager.instance.playerBulletPool[0];

                currentBullet.SetActive(true);
                currentBullet.transform.parent = null;
                currentBullet.transform.position = gunPivot.position;
                currentBullet.transform.up = Vector3.up;
                currentBullet.GetComponent<Rigidbody>().Move(currentBullet.transform.position, currentBullet.transform.rotation);
                currentBullet.GetComponent<BulletMove>().reflect = false;

                BulletManager.instance.playerBulletPool.Remove(currentBullet);
            }
        }
        void FireSkill2()
        {
            for(int i = 0; i < 2; i++)
            {
                if (BulletManager.instance.playerBulletPool.Count > 0)
                {
                    var currentBullet = BulletManager.instance.playerBulletPool[0];

                    currentBullet.SetActive(true);
                    currentBullet.transform.parent = null;
                    currentBullet.transform.position = gunPivot.position + Vector3.right * (i == 0 ? 0.25f : -0.25f);
                    currentBullet.transform.up = Vector3.up;
                    currentBullet.GetComponent<Rigidbody>().Move(currentBullet.transform.position, currentBullet.transform.rotation);
                    currentBullet.GetComponent<BulletMove>().reflect = false;

                    BulletManager.instance.playerBulletPool.Remove(currentBullet);
                }
            }
        }
        void FireSkill3()
        {
            for (int i = 0; i < 2; i++)
            {
                if (BulletManager.instance.playerBulletPool.Count > 0)
                {
                    var currentBullet = BulletManager.instance.playerBulletPool[0];
                    var createPos = gunPivot.position + Vector3.right * (i == 0 ? 0.5f : -0.5f);

                    currentBullet.SetActive(true);
                    currentBullet.transform.parent = null;
                    currentBullet.transform.position = createPos;
                    currentBullet.transform.rotation = Quaternion.Euler(0, 0, (i == 0 ? -30 : 30));
                    currentBullet.GetComponent<Rigidbody>().Move(currentBullet.transform.position, currentBullet.transform.rotation);
                    currentBullet.GetComponent<BulletMove>().reflect = true;

                    BulletManager.instance.playerBulletPool.Remove(currentBullet);
                }
            }
        }
        void FireSkill4()
        {
            int degrees = 15;
            int numOfBullet = 360 / degrees;

            if (_bombCounter >= 3)
            {
                for (int i = 0; i < numOfBullet; i++)
                {
                    if (BulletManager.instance.playerBulletPool.Count > 0)
                    {
                        var currentBullet = BulletManager.instance.playerBulletPool[0];
                        
                        currentBullet.SetActive(true);
                        currentBullet.transform.parent = null;
                        currentBullet.transform.position = transform.position;
                        currentBullet.transform.rotation = Quaternion.Euler(0, 0, i * degrees);
                        currentBullet.GetComponent<Rigidbody>().Move(currentBullet.transform.position, currentBullet.transform.rotation);
                        currentBullet.GetComponent<BulletMove>().reflect = true;
                        currentBullet.GetComponent<BulletMove>().speed = 15f;

                        BulletManager.instance.playerBulletPool.Remove(currentBullet);
                    }
                }
                _bombCounter = 0;
            }
        }
    }

    private void OnDestroy()
    {
        aButton.buttonAction -= Fire;
    }
}
