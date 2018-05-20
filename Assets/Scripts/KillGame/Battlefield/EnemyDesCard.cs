using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyDesCard : MonoBehaviour {

    public static EnemyDesCard _instance;
    private Image image;
    private EmCard emCard;

    private string cardName = "";

    void Awake()
    {
        _instance = this;
        image = gameObject.GetComponent<Image>();
        emCard = GameObject.Find("EnemyCard").GetComponent<EmCard>();
    }
    private void Update()
    {
        if (cardName != "")
        {
            GetSprite(cardName);
            cardName = "";
        }
    }
    public void GetSprite(string data)
    {
        Sprite sp = Resources.Load("Textrues/Card/" + data, typeof(Sprite)) as Sprite;
        ShowCard(sp);
    }
    public void GetSpriteSync(string data)
    {
        cardName = data;
    }
    public void ShowCard(Sprite sp)
    {
        gameObject.GetComponent<Image>().sprite = sp;
        image.DOFade(1, 0.1f).OnComplete(() => image.DOFade(0, 3));
    }
    public void DestroyEnemyCardSync()
    {
        emCard.DestroyEnemyCardSync();
    }
}
