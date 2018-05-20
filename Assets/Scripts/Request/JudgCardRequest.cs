using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class JudgCardRequest : BaseRequest
{
    private GameController gameController;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.JudgCard;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
