using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PopupViewGroup : MonoBehaviour
{
    private Stack<BasePopupMediator> popupStack;
    // Start is called before the first frame update
    void Start()
    {
        popupStack = new Stack<BasePopupMediator>();
    }

    public void addPopupView(BasePopupMediator popupView )
    {
        //Dictionary<string ,string> dictionary = JsonUtility.FromJson<Dictionary<string , string>>(data);
        if(popupStack == null)
        {
            popupStack = new Stack<BasePopupMediator>();
        }

    }
    
}
