using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopupMediator : BaseMediator
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /// <summary>
    /// 进入界面
    /// </summary>
    public virtual void OnEnter() { }
    /// <summary>
    /// 进入界面 with data
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnEnter(IDictionary data) { }
    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause() { }
    /// <summary>
    /// 返回界面
    /// </summary>
    public virtual void OnResume() { }
    /// <summary>
    /// 返回界面 with data
    /// </summary>
    /// <param name="data"></param>
    public virtual void OnResume(IDictionary data) { }
    /// <summary>
    /// 界面退出
    /// </summary>
    public virtual void OnExit() { }
}
