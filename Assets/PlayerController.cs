using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rootTransform;
    public GameObject bullet;
    private float targetRotY = 0;
    private float currentRotY = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        var inputY = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);
        var shoot = Input.GetKeyDown(KeyCode.Space);

        targetRotY = inputX != 0 
            ? (inputX > 0 ? -12f : 12f) : 0;

        currentRotY = Mathf.Lerp(currentRotY, targetRotY, 25f * Time.deltaTime);

        transform.Translate(new Vector3(inputX, inputY, 0) * 4f * Time.deltaTime);
        rootTransform.localRotation = Quaternion.Euler(0f, currentRotY, 0f);

        if (shoot)
        {
            Instantiate(bullet, transform.transform);
        }
    }
}
