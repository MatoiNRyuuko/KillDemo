using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MessagePanel : BasePanel {

    private Text text;
    private float showTime = 1;
    private string message = null;

    private void Update()
    {
        if(message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        text = GetComponent<Text>();
        text.enabled = false;
        uiMng.InjectMsgPanel(this);
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1, 0.5f, false);
        text.text = msg;
        text.enabled = true;
        Invoke("Hide", showTime);
    }
    private void Hide()
    {
        text.CrossFadeAlpha(0, 1, false);
    }
}
