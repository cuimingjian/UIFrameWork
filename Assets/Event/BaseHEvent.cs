using System;
using System.Collections;
//using UnityEngine;

public class BaseHEvent
{
    protected Hashtable m_arguments;
    protected string m_type;
    protected Object m_Sender;

    public IDictionary Params
    {
        get { return this.m_arguments; }
        set { this.m_arguments = (value as Hashtable); }
    }

    public string Type
    {//事件类型 构造函数会给Type 和Sender赋值
        get { return this.m_type; }
        set { this.m_type = value; }
    }
    
    public Object Sender
    {//物体
        get { return this.m_Sender; }
        set { this.m_Sender = value; }
    }

    public override string ToString()
    {
        return this.m_type + "[" + ((this.m_Sender == null) ? "null" : this.m_Sender.ToString()) + "]";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"> 事件类型 </param>
    /// <param name="sender"> 物体 </param>
    public BaseHEvent(string type, Object sender)
    {
        this.Type = type;
        Sender = sender;
        if (this.m_arguments == null)
        {
            this.m_arguments = new Hashtable();
            UnityEngine.Debug.LogError("this.m_arguments" + this.m_arguments.Count);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"> 事件类型 </param>
    /// <param name="args"> 携带的数据 </param>
    /// <param name="sender"> 物体 </param>
    public BaseHEvent(string type, Hashtable args, Object sender)
    {
        this.Type = type;
        this.m_arguments = args;
        Sender = sender;

        if (this.m_arguments == null)
        {
            this.m_arguments = new Hashtable();
        }
    }

    public BaseHEvent Clone()
    {
        return new BaseHEvent(this.m_type, this.m_arguments, Sender);
    }
}