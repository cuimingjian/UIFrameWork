using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 单例类，用来管理UI的加载，替换，销毁。
/// 注意事项：1，类的构造方法是私有，防止被类外调用。
///         2，定义一个静态对象，用于类外访问，在类内构造。
/// </summary>
///
public class UIManager
{
    private Dictionary<MediatorType, string> MediatorPathDict;  //存储界面对应的Prefab的路径
    private Stack<BaseAreaMediator> viewStack;  //界面栈
    private Stack<BasePopupMediator> popupStack;  //popup界面栈
    private Dictionary<MediatorType, BaseAreaMediator> areaMediatorDict; //实例化后的Area面板
    private Dictionary<MediatorType, BasePopupMediator> popupmediatorDict; //实例化后的Popup面板
    private static UIManager _instance;
    private HEventDispatcher Dispatcher; //事件单例
    private UIManager(){
        if (Dispatcher == null)
        {
            Dispatcher = HEventDispatcher.GetInstance();
        }
        ParseMediatorTypeJson();
        Dispatcher.AddEventListener(BaseEventType.EVT_PUSH_VIEW, ListenerPush);
        Dispatcher.AddEventListener(BaseEventType.EVT_POPUP_VIEW, ListenerPop);
    }

    public static UIManager instance {
        get {
            if(_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }
    [Serializable]
    class ViewData
    {
        public List<MediatorInfo> infolist;
    }

    /// <summary>
    /// 加载配置的界面资源
    /// </summary>
    private void ParseMediatorTypeJson() {
        MediatorPathDict = new Dictionary<MediatorType, string>();
        TextAsset jsonData =  Resources.Load<TextAsset>("MediatorType");
        ViewData viewObject = JsonUtility.FromJson<ViewData>(jsonData.text);

        foreach (MediatorInfo info in viewObject.infolist)
        {
            MediatorPathDict.Add(info.mediatorType, info.path);
        }
    }

    /// <summary>
    /// 将Area界面加载到画布上
    /// </summary>
    /// <param name="viewType"></param>
    public void PushAreaView(MediatorType viewType) {
        if(viewStack == null)
        {
            viewStack = new Stack<BaseAreaMediator>();
        }
        //判断栈中显示是否有显示的界面，如果有先暂停。
        if(viewStack.Count > 0)
        {
            BaseAreaMediator topView = viewStack.Peek();
            topView.OnPause();
        }
        //清除上一个AreaView上的Popup
        if (popupStack != null)
        {
            while (popupStack.Count > 0)
            {
                BasePopupMediator popView = popupStack.Pop();
                popView.OnExit();
            }
        }
        BaseAreaMediator currentView = GetAreaMediatorByType(viewType);
        currentView.OnEnter();
        viewStack.Push(currentView);
        
    }
    /// <summary>
    /// 将Popup加载到View上
    /// </summary>
    /// <param name="viewType"></param>
    public void PushPopupView(MediatorType viewType)
    {
        if (popupStack == null) popupStack = new Stack<BasePopupMediator>();
        if(popupStack.Count > 0)
        {
            BasePopupMediator topPop = popupStack.Peek();
            topPop.OnPause();
        }
        BaseAreaMediator areaView = viewStack.Peek();
        areaView.OnPushPoppupView();
        BasePopupMediator popupView = GetPopupMediatorByType(viewType);
        //popupView.transform.SetParent(areaView.transform,false);
        popupView.OnEnter();
        popupStack.Push(popupView);


    }
    /// <summary>
    /// AreaView 关闭出栈 恢复下层Area
    /// </summary>
    /// <param name="viewType"></param>
    public void PopAreaView() {
        if (viewStack == null) return;
        if (viewStack.Count <= 0) return;
        BaseAreaMediator topArea = viewStack.Pop();
        topArea.OnExit();
        if (viewStack.Count <= 0) return;
        BaseAreaMediator curArea = viewStack.Peek();
        curArea.OnResume();
    }
    /// <summary>
    /// PopView 出栈 恢复下层Pop状态
    /// </summary>
    /// <param name="viewType"></param>
    public void popPopupView()
    {
        if (popupStack == null) return;
        Debug.Log(popupStack.Count);
        if (popupStack.Count <= 0) return;
        BasePopupMediator topPop = popupStack.Pop();
        topPop.OnExit();
        if (popupStack.Count <= 0)
        {
            BaseAreaMediator topArea = viewStack.Peek();
            topArea.OnPopPoppupView();
            return;
        }
        BasePopupMediator curPop = popupStack.Peek();
        curPop.OnResume();
    }
    /// <summary>
    /// 根据type获取对应的界面实例
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private BaseAreaMediator GetAreaMediatorByType(MediatorType type)
    {
        if(areaMediatorDict == null)
        {
            areaMediatorDict = new Dictionary<MediatorType, BaseAreaMediator>();
        }
        BaseAreaMediator mediator;
        areaMediatorDict.TryGetValue(type, out mediator);
        BaseAreaMediator baseArea = areaMediatorDict.getValue(type);
        if(mediator == null)
        {
            String path;
            MediatorPathDict.TryGetValue(type, out path);
            GameObject insMediator = UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
            if (areaMediatorDict.ContainsKey(type))
            {
                areaMediatorDict[type] = insMediator.GetComponent<BaseAreaMediator>();
            }
            else
            {
                areaMediatorDict.Add(type, insMediator.GetComponent<BaseAreaMediator>());
            }
            insMediator.GetComponent<BaseAreaMediator>().transform.SetParent(CanvasTransform, false);
            return insMediator.GetComponent<BaseAreaMediator>();
        }
        return mediator;
    }
    /// <summary>
    /// 根据type获取Mediator
    /// </summary>
    /// <param name="type">mediator type</param>
    /// <returns></returns>
    private BasePopupMediator GetPopupMediatorByType(MediatorType type)
    {
        if (popupmediatorDict == null) popupmediatorDict = new Dictionary<MediatorType, BasePopupMediator>();
        BasePopupMediator mediator;
        popupmediatorDict.TryGetValue(type,out mediator);
        if(mediator == null)
        {
            String path;
            MediatorPathDict.TryGetValue(type, out path);
            GameObject insMediator = UnityEngine.Object.Instantiate(Resources.Load(path)) as GameObject;
            if (popupmediatorDict.ContainsKey(type))
            {
                popupmediatorDict[type] = insMediator.GetComponent<BasePopupMediator>();
            }
            else
            {
                popupmediatorDict.Add(type, insMediator.GetComponent<BasePopupMediator>());
            }
            insMediator.GetComponent<BasePopupMediator>().transform.SetParent(CanvasTransform, false);
            return insMediator.GetComponent<BasePopupMediator>();
        }
        return mediator;
    }

    /// <summary>
    /// Area事件回调
    /// </summary>
    /// <param name="baseH"></param>
    private void ListenerPush(BaseHEvent baseH)
    {
        IDictionary dic = baseH.Params;
        MediatorType viewtype = (MediatorType)System.Enum.Parse(typeof(MediatorType), (string)dic["view"]);
        PushAreaView(viewtype);
    }
    /// <summary>
    /// Popup事件回调
    /// </summary>
    /// <param name="baseH"></param>
    private void ListenerPop(BaseHEvent baseH)
    {
        IDictionary dic = baseH.Params;
        MediatorType viewtype = (MediatorType)System.Enum.Parse(typeof(MediatorType), (string)dic["view"]);
        PushPopupView(viewtype);
    }

}
