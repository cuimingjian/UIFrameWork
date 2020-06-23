using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIMediator : BaseAreaMediator
{
    private CanvasGroup canvasGroup;
    public void OnPushViewByType(string viewTypeString)
    {
        MediatorType viewType = (MediatorType)System.Enum.Parse(typeof(MediatorType), viewTypeString);
        UIManager.instance.PushAreaView(viewType);
    }

    public void OnPopupViewByType(string viewTypeString)
    {
        MediatorType viewType = (MediatorType)System.Enum.Parse(typeof(MediatorType), viewTypeString);
        UIManager.instance.PushPopupView(viewType);
    }

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public override void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

    public override void OnResume()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public override void OnPushPoppupView()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnPopPoppupView()
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnClickShopBtn(string viewtype)
    {
        Hashtable table = new Hashtable();
        table.Add("view", viewtype);
        HEventDispatcher.GetInstance().DispatchEvent(new BaseHEvent(BaseEventType.EVT_PUSH_VIEW, table, this));
    }

    public void OnClickTaskBtn(string viewtype)
    {
        Hashtable table = new Hashtable();
        table.Add("view", viewtype);
        HEventDispatcher.GetInstance().DispatchEvent(new BaseHEvent(BaseEventType.EVT_POPUP_VIEW, table, this));
    }
}
