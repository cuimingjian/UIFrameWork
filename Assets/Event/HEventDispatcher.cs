using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HEventListenerDelegate(BaseHEvent hEvent);//定义委托用于传事件基类

public class HEventDispatcher
{
    static HEventDispatcher Instance;
    public static HEventDispatcher GetInstance()//单例
    {
        if (Instance == null)
        {
            Instance = new HEventDispatcher();
        }
        return Instance;
    }

    private Hashtable listeners = new Hashtable(); //掌控所有类型的委托事件

    /// <summary>
    /// 增加监听事件
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">监听delegate</param>
    public void AddEventListener(string type, HEventListenerDelegate listener)
    {
        HEventListenerDelegate hEventListenerDelegate = this.listeners[type] as HEventListenerDelegate;//获得之前这个类型的委托 如果第一次等于Null 
        hEventListenerDelegate = (HEventListenerDelegate)Delegate.Combine(hEventListenerDelegate, listener);//将两个委托的调用列表连接在一起,成为一个新的委托
        this.listeners[type] = hEventListenerDelegate;//赋值给哈希表中的这个类型
    }
    /// <summary>
    /// 删除监听事件
    /// </summary>
    /// <param name="type">事件类型</param>
    /// <param name="listener">监听delegate</param>
    public void RemoveEventListener(string type, HEventListenerDelegate listener)
    {
        HEventListenerDelegate hEventListener = this.listeners[type] as HEventListenerDelegate;//获得之前这个类型的委托 如果第一次等于Null
        if (hEventListener != null)
        {
            hEventListener = (HEventListenerDelegate)Delegate.Remove(hEventListener, listener);//从hEventListener的调用列表中移除listener
            this.listeners[type] = hEventListener;//赋值给哈希表中的这个类型
        }
    }
    /// <summary>
    /// 事件派发器
    /// </summary>
    /// <param name="baseH">监听的事件</param>
    public void DispatchEvent(BaseHEvent baseH)
    {
        HEventListenerDelegate hEventListener = this.listeners[baseH.Type] as HEventListenerDelegate;

        if (hEventListener != null)
        {
            try
            {
                hEventListener(baseH);//执行委托
            }
            catch (Exception e)
            {
                throw new System.Exception(string.Concat(new string[] { "Error Dispatch event", baseH.Type.ToString(), ":", e.Message, " ", e.StackTrace }), e);
            }
        }
    }
    /// <summary>
    /// 删除所有的事件
    /// </summary>
    public void RemoveAll()
    {
        this.listeners.Clear();
    }

}