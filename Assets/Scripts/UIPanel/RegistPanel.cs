using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegistPanel : BasePanel {
    private InputField userNameInput;
    private InputField passWordInput;
    private InputField repassWordInput;
    private Button RegistButton;
    private Button CloseButton;
    private RegistRequest registRequst;

    private void Awake()
    {
        userNameInput = transform.Find("usernameLabel/InputField").GetComponent<InputField>();
        passWordInput = transform.Find("passwordLabel/InputField").GetComponent<InputField>();
        repassWordInput = transform.Find("RepasswordLabel/InputField").GetComponent<InputField>();

        RegistButton = transform.Find("RegistButton").GetComponent<Button>();
        CloseButton = transform.Find("CloseButton").GetComponent<Button>();

        RegistButton.onClick.AddListener(OnRegistClick);
        CloseButton.onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.3f);
        registRequst = GetComponent<RegistRequest>();
    }
    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.4f);
        Tween tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.3f);
        tweener.OnComplete(() => uiMng.PopPanel());
    }
    private void OnRegistClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(userNameInput.text)){
            msg += "用户名不能为空 ";
        }
        if (string.IsNullOrEmpty(passWordInput.text))
        {
            msg += "密码不能为空 ";
        }
        if (passWordInput.text != repassWordInput.text)
        {
            msg += "重复密码和密码不同";
        }
        if( msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        registRequst.SendRequest(userNameInput.text, passWordInput.text);
    }
    public void OnRigisterResponse(ReturnCode returnCode)
    {
        if(returnCode == ReturnCode.Success)
        {
            uiMng.ShowMessageSync("注册成功");
        }
        else
        {
            uiMng.ShowMessageSync("已存在的用户名");
        }
    }
}
