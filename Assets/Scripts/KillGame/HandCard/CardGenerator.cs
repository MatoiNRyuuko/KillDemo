using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardGenerator : MonoBehaviour {
    public GameObject CardPrefab;
    private Image cardImage;

    private Transform fromCard;
    private Transform toCard;
    
    private int[] paiku = new int[60];
    private int index = 0;

    public string[] CardName;

    private void Awake()
    {
        fromCard = transform.Find("MyFromcard").transform;
        toCard = transform.Find("MyTocard").transform;
        GenerateCardToLibrary();
    }
    
    private void Update()
    {

    }
    //随机生成卡牌方法
    public void GenerateCardToLibrary()
    {
        //0杀1闪2无懈3无中4桃5兵6拆7决斗8闪电9乐10顺11桃园
        for(int i= 0; i < 60; i++)
        {
            int getcard = Random.Range(0, 100);
            if (getcard < 40)
            {
                paiku[i] = 0;
            }
            if(getcard >= 40 && getcard < 65)
            {
                paiku[i] = 1;
            }
            if(getcard >= 65 && getcard < 75)
            {
                paiku[i] = 4;
            }
            if (getcard >= 75 && getcard < 78)
            {
                paiku[i] = 3;
            }
            if (getcard >= 78 && getcard < 81)
            {
                paiku[i] = 10;
            }
            if (getcard >= 81 && getcard < 84)
            {
                paiku[i] = 6;
            }
            if (getcard >= 84 && getcard < 87)
            {
                paiku[i] = 7;
            }
            if (getcard >= 87 && getcard < 90)
            {
                paiku[i] = 2;
            }
            if (getcard >= 90&& getcard < 92)
            {
                paiku[i] = 5;
            }
            if (getcard >= 92 && getcard < 94)
            {
                paiku[i] = 3;
            }
            if (getcard >= 94 && getcard < 96)
            {
                paiku[i] = 9;
            }
            if (getcard >= 96 && getcard < 100)
            {
                paiku[i] = 11;
            }

        }
    }
    public GameObject RadomGenerateCard()
    {
        GameObject go = Instantiate(CardPrefab, fromCard);
        Sprite sp = Resources.Load("Textrues/Card/" + CardName[paiku[index]], typeof(Sprite)) as Sprite;
        go.GetComponent<Image>().sprite = sp;
        
        //nowGenerateCard = go.GetComponent<UISprite>();
        //随机生成卡牌的数据从这里修改
        //nowGenerateCard.spriteName = CardName[paiku[index]];
        index++;
        if (index == 60)
        {
            GenerateCardToLibrary();
            index = 0;
        }
        //go.transform.DOMove(toCard.position, 0.4f);
        iTween.MoveTo(go, toCard.position, 0.8f);
        return go;
    }
}
