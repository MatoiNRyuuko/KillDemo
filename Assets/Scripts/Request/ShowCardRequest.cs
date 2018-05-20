using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class ShowCardRequest : BaseRequest {
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.ShowCard;
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        DesCardSR._instance.GetSpriteSync(data);
        DesCardSR._instance.DestroyEnemyCardSync();
    }
}
