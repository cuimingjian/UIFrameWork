using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMediator : BasePopupMediator
{
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
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
        Destroy(transform.gameObject);
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

    public void OnClickClose()
    {
        UIManager.instance.popPopupView();
    }

}
