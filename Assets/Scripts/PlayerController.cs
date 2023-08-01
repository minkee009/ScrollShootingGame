using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject debugModel;
    public GameObject ingameModel;
    public Transform rootTransform;
    public float rotateAmount = 12f;
    private float targetRotY = 0;
    private float currentRotY = 0;

    private bool isDebugMode = false;

    private void Awake()
    {
        debugModel.SetActive(isDebugMode);
        ingameModel.SetActive(!isDebugMode);
    }

    // Update is called once per frame
    void Update()
    {
        var inputX = (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0);
        var inputY = (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0);

        var wishPos = new Vector3(inputX * 2f, inputY, 0) * 4f * Time.deltaTime;

        if (Mathf.Abs((transform.position + wishPos).x) > 65f)
        {
            var clampF = wishPos.x > 0 ? 1 : -1;
            transform.position = new Vector3(65f * clampF, transform.position.y, transform.position.z);
            inputX = 0;
            wishPos.x = 0;
        }

        if (Mathf.Abs((transform.position + wishPos).y) > 40f)
        {
            var clampF = wishPos.y > 0 ? 1 : -1;
            transform.position = new Vector3(transform.position.x, 40f * clampF, transform.position.z);
            wishPos.y = 0;
        }

        targetRotY = inputX != 0
            ? (inputX > 0 ? -rotateAmount : rotateAmount) : 0;

        currentRotY = Mathf.Lerp(currentRotY, targetRotY, 25f * Time.deltaTime);


        transform.Translate(wishPos);
        rootTransform.localRotation = Quaternion.Euler(0f, currentRotY, 0f);

        if (Input.GetKeyDown(KeyCode.C))
        {
            isDebugMode = !isDebugMode; 
            debugModel.SetActive(isDebugMode);
            ingameModel.SetActive(!isDebugMode);
        }
    }
}
