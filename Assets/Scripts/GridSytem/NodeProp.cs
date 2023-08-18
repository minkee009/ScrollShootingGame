using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeProp : MonoBehaviour
{
    public virtual bool TryCombineOtherNodeObj(NodeObj other)
    {
        return false;
    }

    public virtual void AddcomponentForInstance(GameObject instance)
    {
        
    }

    public virtual void RemovecomponentForInstance(GameObject instance)
    {
        
    }
}
