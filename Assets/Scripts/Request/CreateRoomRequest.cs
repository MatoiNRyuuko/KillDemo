using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class CreateRoomRequest : BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }

    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if(returnCode == ReturnCode.Success)
        { 
            roomPanel.SetLocalPlayerResSync();
        }
    }
}

