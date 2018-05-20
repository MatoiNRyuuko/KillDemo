using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class EndTurnRequest : BaseRequest {
    GameController gameController;
    public override void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        requestCode = RequestCode.Game;
        actionCode = ActionCode.EndTurn;
        base.Awake();
    }
    public override void SendRequest()
    {
        base.SendRequest("r");
    }
    public override void OnResponse(string data)
    {
        gameController.ChangeTurn(data);
    }
}
