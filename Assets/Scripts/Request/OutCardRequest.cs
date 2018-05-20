using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class OutCardRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.OutCard;
        base.Awake();
    }
    public void SendRequest(string data)
    {
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        
    }
}
