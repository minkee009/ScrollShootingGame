using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeProp : MonoBehaviour
{
    public abstract bool TryCombineOtherNodeObj(NodeObj other);

    public abstract void AddcomponentForInstance(GameObject instance);

    public abstract void RemovecomponentForInstance(GameObject instance);
}
