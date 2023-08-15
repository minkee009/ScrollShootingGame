using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CustomMovePreset { ToDownward = 0, ToHorizontal, ToPlayer }

public class CustomMoveProperty : NodeProp
{
    public CustomMovePreset preset;
    public float speed = 4.0f;
    public float moveSharpness = 12f;

    CustomMove[] _otherCustomMove;
    CustomMoveProperty _otherMoveComponent;

    public override bool TryCombineOtherNodeObj(NodeObj other)
    {
        if (other.CompareTag("Player")) return false;

        if(other.TryGetComponent(out _otherMoveComponent))
        {
            //Destroy(_otherMoveComponent.sprite.transform);
            DestroyImmediate(_otherMoveComponent);
        }

        other.gameObject.AddComponent<CustomMoveProperty>();
        var c = other.gameObject.GetComponent<CustomMoveProperty>();
        c.preset = preset;
        c.speed = speed;
        c.moveSharpness = moveSharpness;

        //sprite.transform.parent = other.transform;
        //colorChange.RegistColorChangeToAction();

        return true;
    }

    public override void AddcomponentForInstance(GameObject instance)
    {
        if (instance == null) return;
        switch (preset)
        {
            case CustomMovePreset.ToDownward:
                instance.AddComponent<MoveDownward>();
                var move = instance.GetComponent<MoveDownward>();
                move.speed = speed;
                move.moveSharpness = moveSharpness;
                move.rb = instance.GetComponent<Rigidbody>();
                break;
            case CustomMovePreset.ToHorizontal:
                instance.AddComponent<MoveHorizontal>();
                var move2 = instance.GetComponent<MoveHorizontal>();
                move2.speed = speed;
                move2.moveSharpness = moveSharpness;
                move2.rb = instance.GetComponent<Rigidbody>();
                break;
        }
        //firstCreation = false;
    }

    public override void RemovecomponentForInstance(GameObject instance)
    {
        if(instance == null) return;
        //자기 속성 해당하는 모든 컴포넌트 삭제

        _otherCustomMove = instance.GetComponents<CustomMove>();

        foreach (CustomMove move in _otherCustomMove)
        {
            DestroyImmediate(move);
        }
    }
}
