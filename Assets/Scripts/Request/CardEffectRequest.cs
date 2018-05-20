using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class CardEffectRequest : BaseRequest {
    private CardEffect cardEffect;
    public override void Awake()
    {
        cardEffect = GetComponent<CardEffect>();
        requestCode = RequestCode.Game;
        actionCode = ActionCode.CardEffect;
        base.Awake();
    }
    public void SendRequest(string data)
    {
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        cardEffect.GetCardEffect(data);
        Debug.Log(data);
    }
}
