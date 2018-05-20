using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class WhoFirstRequest : BaseRequest {
    private WhoFirst whoFirst;
    public override void Awake()
    {
        whoFirst = GetComponent<WhoFirst>();
        actionCode = ActionCode.WhoFirst;
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        whoFirst.JudgResultSync(data);
    }
}
