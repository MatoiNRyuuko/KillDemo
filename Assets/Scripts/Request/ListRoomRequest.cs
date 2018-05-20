using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ListRoomRequest : BaseRequest {
    private RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        //对服务器返回来的数据进行解析，获取房间的每个客户端信息。
        List<UserData> udList = new List<UserData>();
        if (data != "0")
        {
            string[] udArray = data.Split('|');
            foreach (string ud in udArray)
            {
                string[] strs = ud.Split(',');
                udList.Add(new UserData(int.Parse(strs[0]), strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
            }
        }
        //调用房间列表面板的异步加载方法
        roomListPanel.LoadRoomItemSync(udList);
    }
}
