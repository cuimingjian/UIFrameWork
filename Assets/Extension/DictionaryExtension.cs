using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DictionaryExension
{
    public static Tvalue getValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue>dic ,Tkey tkey)
    {
        Tvalue value;
        dic.TryGetValue(tkey, out value);
        return value;
    }

    public static void Say(this string str)
    {
        if(str == null)
        {
            Debug.Log("Say Hello");
        }
        else
        {
            Debug.Log(str);
        }
    }
}
