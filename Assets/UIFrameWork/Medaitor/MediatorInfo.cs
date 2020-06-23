using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class MediatorInfo:ISerializationCallbackReceiver
{
    [NonSerialized]
    public MediatorType mediatorType;
    public string mediatorTypeString;
    public string path;

    public void OnAfterDeserialize()
    {
        mediatorType = (MediatorType)System.Enum.Parse(typeof(MediatorType), mediatorTypeString);
    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }
}
