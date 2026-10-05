using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[Serializable]
public class SerializedDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new();
    [SerializeField] private List<Tvalue> values = new();

    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count) 
        {
            throw new Exception("Numero de keys y values no coincide");
        }
        for(int i = 0; i< keys.Count; i++) 
        {
            this[keys[i]] = values[i];
        }
    }

    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(var pair in this) 
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
}
