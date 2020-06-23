using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMediator : MonoBehaviour
{
    protected HEventDispatcher dispatcher;
    private void Awake()
    {
        if (dispatcher == null) dispatcher = HEventDispatcher.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
