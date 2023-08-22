using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreatorController : MonoBehaviour
{
    public float maxRandomRange = 4;
    public float minRandomRange = 1;

    public GameObject createEffect;

    float _createTimer;
    float _createDelay;

    public ChildObjPreset createObjPreset;
    public ChildObjPreset enemysItemChild;

    private void Start()
    {
        _createDelay = Random.Range(minRandomRange, maxRandomRange);
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime * GameManager.instance.inGameTimeSpeed;

        _createTimer += deltaTime;

        if(_createTimer > _createDelay && createObjPreset != null)
        {
            _createTimer = 0;
            _createDelay = Random.Range(minRandomRange,maxRandomRange);

            var childInstance = new ChildObjPreset();
            childInstance.activePrefab = createObjPreset.activePrefab;
            childInstance.nodeProps = createObjPreset.nodeProps;
            childInstance.createPos = createObjPreset.createPos;
            childInstance.typeName = createObjPreset.typeName;
            childInstance.createPos = transform.position;
            childInstance.CreateInPlay();

            var effect = Instantiate(createEffect);
            effect.transform.position = transform.position + Vector3.forward * -9f;

            switch (createObjPreset.typeName)
            {
                case "Enemy":
                    if(enemysItemChild != null)
                    {
                        var enemyCon = childInstance.instance.GetComponent<EnemyController>();

                        var itemChild = new ChildObjPreset();
                        itemChild.activePrefab = enemysItemChild.activePrefab;
                        itemChild.pivotTransform = childInstance.instance.transform;
                        itemChild.nodeProps = enemysItemChild.nodeProps;
                        itemChild.typeName = enemysItemChild.typeName;

                        enemyCon.itemPreset = itemChild;
                        childInstance.instance.GetComponent<HitableObj>().OnDie += enemyCon.itemPreset.CreateInPlay;
                    }
                    break;
                default:
                    return;
            }
        }
    }
}
