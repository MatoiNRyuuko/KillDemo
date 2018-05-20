using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class JoinRoomRequest : BaseRequest {
    private RoomListPanel roomListPanel;

    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.JoinRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }

    public void SendRequest(int id)
    {
        base.SendRequest(id.ToString());
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('-');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        UserData ud1 = null;
        UserData ud2 = null;
        if(returnCode == ReturnCode.Success)
        {
            string[] udStrArray = strs[1].Split('|');
            ud1 = new UserData(udStrArray[0]);
            ud2 = new UserData(udStrArray[1]);
        }
        roomListPanel.OnJoinResponse(returnCode, ud1, ud2);
    }
}
