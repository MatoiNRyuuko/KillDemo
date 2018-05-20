using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ChaiRequest : BaseRequest {
    private CardEffect cardEffect;
    private GameController gameController;
    public override void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        cardEffect = GameObject.Find("GameController").GetComponent<CardEffect>();
        requestCode = RequestCode.Game;
        actionCode = ActionCode.Chai;
        base.Awake();
    }
    public void SendRequest(string data)
    {
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        if (data != "Initiative")
        {
            string[] judg = data.Split(',');
            if (judg[0] == "Destroy")
            {
                cardEffect.RemoveCard(judg[1]);
                gameController.ShunOrChai("Chai");
            }
            else
            {
                cardEffect.ChoosingEnemyCard(data);
                cardEffect.ShunOrChai("Chai");
            }
        }
    }
}
