using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //UIManager.instance.PushAreaView(MediatorType.MainUIView);
        string sa = "not null";
        sa.Say();
        string ss = null;
        ss.Say();
    }
}
