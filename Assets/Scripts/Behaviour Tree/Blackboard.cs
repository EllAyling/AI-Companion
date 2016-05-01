using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blackboard{

    public Dictionary<string, object> treeData = new Dictionary<string, object>();

    public T GetValueFromKey<T>(string key)
    {
        object outobject;
        if (treeData.TryGetValue(key, out outobject))
        {
            return (T)outobject;
        }
        else return default(T);

    }

    public void SetValue<T>(string key, T value)
    {
        treeData[key] = value;
    }
}
