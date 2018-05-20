using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class LoginRequest : BaseRequest {
    private LoginPanel loginPanel;
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }
    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }
    //重写basereqeust方法
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        loginPanel.OnLoginResponse(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            string username = strs[1];
            int totalcount = int.Parse(strs[2]);
            int wincount = int.Parse(strs[3]);
            UserData ud = new UserData(username, totalcount, wincount);
            gamefacade.SetUserData(ud);
        }
    }
}
