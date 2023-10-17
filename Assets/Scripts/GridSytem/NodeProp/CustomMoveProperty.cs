using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CustomMovePreset { MoveDownward = 0, MoveHorizontal, MoveToPlayer }

public class CustomMoveProperty : MonoBehaviour, INodeProp
{
    public CustomMovePreset preset;
    public float speed = 4.0f;
    public float moveSharpness = 12f;

    CustomMove _otherCustomMove;
    CustomMoveProperty _otherMoveComponent;

    public string TypeName => _typeName;

    [SerializeField] string _typeName;

    public bool TryCombineOtherNodeObj(NodeObj other)
    {
        if (other.typeName == "Player" || other.typeName == "ObjectCreator") 
            return false;

        if(other.TryGetComponent(out _otherMoveComponent))
        {
            DestroyImmediate(_otherMoveComponent);
        }

        other.gameObject.AddComponent<CustomMoveProperty>();
        var c = other.gameObject.GetComponent<CustomMoveProperty>();
        c.preset = preset;
        c.speed = speed;
        c.moveSharpness = moveSharpness;
        c._typeName = _typeName;

        return true;
    }

    public void AddcomponentForInstance(GameObject instance)
    {
        if (instance == null) return;

        if (instance.TryGetComponent(out _otherCustomMove) && _otherCustomMove.GetType().ToString() == preset.ToString())
        {
            return;
            //Ŀ���ҹ��� ��Ÿ���� �̸��� ������ ������ �ƴ��� Ȯ���� �Ǿ����
        }
        
        RemovecomponentForInstance(instance);

        switch (preset)
        {
            case CustomMovePreset.MoveDownward:
                instance.AddComponent<MoveDownward>();
                var move = instance.GetComponent<MoveDownward>();
                move.speed = speed;
                move.moveSharpness = moveSharpness;
                move.rb = instance.GetComponent<Rigidbody>();
                break;
            case CustomMovePreset.MoveHorizontal:
                instance.AddComponent<MoveHorizontal>();
                var move2 = instance.GetComponent<MoveHorizontal>();
                move2.speed = speed;
                move2.moveSharpness = moveSharpness;
                move2.rb = instance.GetComponent<Rigidbody>();
                break;
            case CustomMovePreset.MoveToPlayer:
                instance.AddComponent<MoveToPlayer>();
                var move3 = instance.GetComponent<MoveToPlayer>();
                move3.speed = speed;
                move3.moveSharpness = moveSharpness;
                move3.rb = instance.GetComponent<Rigidbody>();
                break;
        }
    }

    public void RemovecomponentForInstance(GameObject instance)
    {
        if(instance == null) return;
       
        _otherCustomMove = instance.GetComponent<CustomMove>();

        //�ڱ� �Ӽ� �ش��ϴ� ������Ʈ ����
        DestroyImmediate(_otherCustomMove);
    }
}
