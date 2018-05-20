using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DesCardSR : MonoBehaviour {
    public static DesCardSR _instance;

    private EmCard emCard;

    public GameObject cardPrefab;
    private HorizontalLayoutGroup DesCardLayout;
    private List<GameObject> srList = new List<GameObject>();
    private string cardName = "";
    private float timer = 0;
    private float FadeTime = 6;
    private float DestroyTime = 8;
    private float DestroyNum = 0;
    private void Update()
    {
        if (cardName != "")
        {
            GetSprite(cardName);
            cardName = "";
        }
        if(srList != null)
        {
            timer += Time.deltaTime;
            if (timer > FadeTime)
                FadeShowCard();
            if (timer > DestroyTime)
                ClearShowCard();
        }
    }
    private void Awake()
    {
        _instance = this;
        DesCardLayout = transform.Find("Layout").GetComponent<HorizontalLayoutGroup>();
        emCard = GameObject.Find("EnemyCard").GetComponent<EmCard>();
    }
    public void ShowCardUp(Sprite sp)
    {
        GameObject DesCard = GameObject.Instantiate(cardPrefab);
        DesCard.transform.DOShakeScale(2, 20, 10, 90);
        DesCard.GetComponent<Image>().sprite = sp;
        DesCard.transform.SetParent(DesCardLayout.transform);
        srList.Add(DesCard);
        timer = 0;
    }
    public void GetSpriteSync(string data)
    {
        cardName = data;
    }
    private void GetSprite(string data)
    {
        Sprite sp = Resources.Load("Textrues/Card/" + data, typeof(Sprite)) as Sprite;
        ShowCardUp(sp);
    }
    public void DestroyEnemyCardSync()
    {
        emCard.DestroyEnemyCardSync();
    }
    private void FadeShowCard()
    {
        DestroyNum = srList.Count;
        for (int i = 0; i < srList.Count; i++)
        {
            srList[i].GetComponent<Image>().DOFade(0, 1);
        }
    }
    private void ClearShowCard()
    {
        for(int i=0;i< DestroyNum; i++)
        {
            Destroy(srList[i]);
        }
        DestroyNum = 0;
        srList.Clear();
    }
}
