using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeProp
{
    public string TypeName { get; }

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
