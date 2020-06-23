using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMediator : BaseAreaMediator
{
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        if (dispatcher == null) dispatcher = HEventDispatcher.GetInstance();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }
    //注册/添加事件
    void Start()
    {
        if (dispatcher == null) dispatcher = HEventDispatcher.GetInstance();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    //事件分发
    void Update()
    {
        if (dispatcher == null)
        {
            dispatcher = HEventDispatcher.GetInstance();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            dispatcher.DispatchEvent(new BaseHEvent(BaseEventType.GAME_DATA, this));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            dispatcher.DispatchEvent(new BaseHEvent(BaseEventType.GAME_WIN, this));
        }
    }
    //事件移除
    private void OnDestroy()
    {
        dispatcher.RemoveEventListener(BaseEventType.GAME_DATA, StartHEvent);
        dispatcher.RemoveEventListener(BaseEventType.GAME_WIN, StopHEvent);
    }

    public void test()
    {
        Debug.Log("listener test success");
    }

    //添加事件方法
    void StartDataEvent(BaseHEvent hEvent)
    {
        //Debug.Log("Doule" + hEvent.ToString());
        //Debug.Log("Sender" + hEvent.Sender.ToString());
    }

    void StartHEvent(BaseHEvent hEvent)
    {
        //Debug.Log("StartHEvent + HEvent" + hEvent.ToString());
        SkillMediator skill = hEvent.Sender as SkillMediator;
        skill.test();
    }

    void StopHEvent(BaseHEvent hEvent)
    {
        Debug.Log("StopHEvent + HEvent" + hEvent.ToString());
    }
    public override void OnEnter()
    {
        if (dispatcher == null)
        {
            dispatcher = HEventDispatcher.GetInstance();
        }
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();

        dispatcher.AddEventListener(BaseEventType.GAME_DATA, StartHEvent);
        dispatcher.AddEventListener(BaseEventType.GAME_DATA, StartDataEvent);
        dispatcher.AddEventListener(BaseEventType.GAME_WIN, StopHEvent);
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }

    public override void OnExit()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }


    public void OnClickCloseBtn()
    {
        UIManager.instance.PopAreaView();
    }

}
