using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GGRequest : BaseRequest {
    private GameController gameController;
    private GG gg;
    public override void Awake()
    {
        gg = transform.GetComponent<GG>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
        gg.showGG(data);
        gameController.GameOver();
    }
}
