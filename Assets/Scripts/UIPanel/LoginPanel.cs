using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class LoginPanel : BasePanel {
    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        transform.localPosition = new Vector3(1000, 0, 0);

        usernameIF = transform.Find("usernameLabel/InputField").GetComponent<InputField>();
        passwordIF = transform.Find("passwordLabel/InputField").GetComponent<InputField>();
        loginRequest = GetComponent<LoginRequest>();

        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegistButton").GetComponent<Button>().onClick.AddListener(OnRegistClick);

        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation();
    }
    public override void OnPause()
    {
        transform.DOScale(0, 0.4f);
        Tween tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.1f);
        tweener.OnComplete(() => gameObject.SetActive(false));
    }
    public override void OnResume()
    {
        EnterAnimation();
    }
    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if(msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if(returnCode == ReturnCode.Success)
        {
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，无法登陆，请重新输入");
        }
    }

    private void OnRegistClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Regist);
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    public override void OnExit()
    {
        HideAnimation();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 1);
        transform.DOLocalMove(Vector3.zero, 0.3f);

    }
    private void HideAnimation()
    {
        transform.DOScale(0, 0.4f);
        Tween tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
        tweener.OnComplete(() => gameObject.SetActive(false));
    }
}