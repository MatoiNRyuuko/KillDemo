using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GoodGameRequest : BaseRequest {

    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.GG;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {

    }
}
