using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour {
    public Transform card01;
    public GameObject CardPrefab;

    public Transform fromCard;
    public Transform toCard;
    public Transform AIfromCard;
    public Transform AItoCard;
    public string[] CardName;
    private UISprite nowGenerateCard;
    private UISprite getGenerateCard;
    private UISprite AInowGenerateCard;
    


    public ShunChai shunorchai;
    public MyCard mycard;

    public int[] paiku = new int[60];
    public int index = 0;


    private void Awake()
    {
        GenerateCardToLibrary();

    }
    //随机生成卡牌方法
    private void Update()
    {

    }
    public void GenerateCardToLibrary()
    {
        //0杀1闪2无懈3无中4桃5兵6拆7决斗8闪电9乐10顺11桃园
        for(int i= 0; i < 60; i++)
        {
            int getcard = Random.Range(0, 100);
            if (getcard < 40)
            {
                paiku[i] = 10;
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
                paiku[i] = 8;
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

        GameObject go = NGUITools.AddChild(this.gameObject, CardPrefab);
        nowGenerateCard = go.GetComponent<UISprite>();
        go.transform.position = fromCard.position;
        //随机生成卡牌的数据从这里修改
        nowGenerateCard.spriteName = CardName[paiku[index]];
        index++;
        if (index == 60)
        {
            GenerateCardToLibrary();
            index = 0;
        }
        iTween.MoveTo(go, toCard.position,0.6f);
        return go;

    }
    public GameObject GetAICard(GameObject get)
    {

        GameObject go = NGUITools.AddChild(this.gameObject, CardPrefab);
        nowGenerateCard = go.GetComponent<UISprite>();
        getGenerateCard = get.GetComponent<UISprite>();
        Debug.Log(getGenerateCard.spriteName);
        go.transform.position = fromCard.position;
        //随机生成卡牌的数据从这里修改
        nowGenerateCard.spriteName = getGenerateCard.spriteName;
        return go;
    }
    public GameObject AIGetMyCard(GameObject get)
    {

        GameObject go = NGUITools.AddChild(this.gameObject, CardPrefab);
        go.transform.position = card01.position;
        nowGenerateCard = go.GetComponent<UISprite>();
        getGenerateCard = get.GetComponent<UISprite>();
        Debug.Log(getGenerateCard.spriteName);
        //随机生成卡牌的数据从这里修改
        nowGenerateCard.spriteName = getGenerateCard.spriteName;
        return go;
    }
    public GameObject AIRadomGenerateCard()
    {
        GameObject go = NGUITools.AddChild(this.gameObject, CardPrefab);
        go.transform.position = card01.position;
        //随机生成卡牌的数据从这里修改
        //int index = 0;
        AInowGenerateCard = go.GetComponent<UISprite>();
        AInowGenerateCard.spriteName = CardName[paiku[index]];
        Debug.Log(AInowGenerateCard.spriteName);
        index++;
        if (index == 60)
        {
            GenerateCardToLibrary();
            index = 0;
        }
        return go;
        
    }

}
