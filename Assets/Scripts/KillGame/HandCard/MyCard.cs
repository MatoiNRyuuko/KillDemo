using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyCard : MonoBehaviour
{
    private Transform card01;
    private Transform card02;

    private GameObject getCardGo;

    private CardEffectRequest cardEffectRequest;
    private OutCardRequest outCardRequest;

    private CardGenerator cardGenerator;
    private GameController gameController;
    private CardEffect cardEffect;

    private List<GameObject> cards = new List<GameObject>();
    private Sprite Localsp;

    private float xOffset;
    private string shunCardname = "";

    public bool IsSelect = false;
    private bool showMyCard = false;
    private bool removeCard = false;

    private void Awake()
    {
        card01 = transform.Find("card01");
        card02 = transform.Find("card02");
        xOffset = card02.position.x - card01.position.x;

        GameObject.Find("Button/OutCardButton").GetComponent<Button>().onClick.AddListener(SendOutCardRequest);
        GameObject.Find("Button/CancleButton").GetComponent<Button>().onClick.AddListener(SendCancleRequest);


        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        cardGenerator = GetComponent<CardGenerator>();
        cardEffect = gameController.GetComponent<CardEffect>();

        outCardRequest = GetComponent<OutCardRequest>();
        cardEffectRequest = gameController.GetComponent<CardEffectRequest>();
    }
    private void Start()
    {
        StartCoroutine(StartPlayGetcard());
    }
    private void Update()
    {
        if (showMyCard)
        {
            getMyCardList();
            showMyCard = false;
        }
        if (removeCard)
        {
            RemoveCard();
            removeCard = false;
        }
    }

    public void AddCard(GameObject cardGo)
    {
        GameObject go;
        go = cardGo;
        go.transform.parent = this.transform;
        Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * (cards.Count);
        iTween.MoveTo(go, toPosition, 0.8f);
        cards.Add(go);
    }
    private IEnumerator StartPlayGetcard()
    {
        for (int i = 0; i < 4; i++)
        {
            getCardGo = cardGenerator.RadomGenerateCard();
            yield return new WaitForSeconds(1);
            AddCard(getCardGo);
            yield return new WaitForSeconds(1);
        }
    }
    public void RemoveCard(GameObject go)
    {
        Destroy(go);
        cards.Remove(go);
    }

    /// <summary>
    /// 这里是用来销毁被顺走的卡
    /// </summary>
    /// <param name="cardname"></param>
    public void RemoveCardSync(string cardname)
    {
        shunCardname = cardname;
        removeCard = true;
    }
    private void RemoveCard()
    {
        GameObject go = null;
        for (int i = 0;i<cards.Count;i++)
        {
            if(cards[i].GetComponent<Image>().sprite.name.ToString() == shunCardname)
            {
                go = cards[i];
                RemoveCard(cards[i]);
                SortHandCard(go);
                break;
            }
        }
    }

    //提交出牌的请求
    private void SendOutCardRequest()
    {
        Sprite sp = null;
        GameObject go = null;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].transform.position.y - card01.transform.position.y > 2f)
            {
                sp = cards[i].GetComponent<Image>().sprite;
                go = cards[i];
                IsSelect = false;
                RemoveCard(cards[i]);
                SortHandCard(go);
            }
        }
        if (sp != null)
        {
            Localsp = sp;
            //这里定义不同阶段发送不同的请求操作
            if (gameController.state == GameState.PlayCard)
            {
                ActiveOutCard(sp);
                StartCoroutine(SendCardEffect());
            }
                
            if (gameController.state == GameState.Responsing)
            {
                ResponseOutCard(sp);
                StartCoroutine(SendCardEffect());
            }
            if (gameController.state == GameState.DisCard)
            {
                DisCard(sp);
            }
            if(gameController.state == GameState.SuddenDeath)
            {
                HealingOutCard(sp);
                StartCoroutine(SendCardEffect());
            }
        }
    }
    private IEnumerator SendCardEffect()
    {
        yield return new WaitForSeconds(0.1f);
        cardEffectRequest.SendRequest(Localsp.name.ToString());
    }
    private void SendCancleRequest()
    {
        if(gameController.state == GameState.SuddenDeath)
        {
            gameController.SendMyLoseRequest();
        }
        else
        {
            cardEffect.MyCancle();
            cardEffectRequest.SendRequest("Cancle");
        }
    }
    public void DisableHandCard()
    {
        foreach (GameObject mycard in cards)
        {
            mycard.GetComponent<HandCardController>().enabled = false;
        }
    }
    //主动出牌阶段出牌操作
    private void ActiveOutCard(Sprite sp)
    {
        DesCardSR._instance.ShowCardUp(sp);
        gameController.DownCard();
        gameController.DisableAllButtonSync();
        outCardRequest.SendRequest(sp.name.ToString());
        cardEffect.AddMyCardEffect(sp.name.ToString());
    }
    //回应出牌阶段出牌操作
    private void ResponseOutCard(Sprite sp)
    {
        DesCardSR._instance.ShowCardUp(sp);
        gameController.DownCard();
        gameController.DisableAllButtonSync();
        outCardRequest.SendRequest(sp.name.ToString());
    }
    //弃牌阶段出牌操作
    private void DisCard(Sprite sp)
    {
        DesCardSR._instance.ShowCardUp(sp);
        gameController.DownCard();
        gameController.DisableAllButtonSync();
        outCardRequest.SendRequest(sp.name.ToString());
        gameController.ResetFlodName();
    }
    //濒死阶段出牌操作
    private void HealingOutCard(Sprite sp)
    {
        DesCardSR._instance.ShowCardUp(sp);
        gameController.DownCard();
        outCardRequest.SendRequest(sp.name.ToString());
        gameController.ResetHealingName();
        gameController.HealMyHp();
        gameController.WhoSuddenDeath();
    }
    private void SortHandCard(GameObject cardgo)
    {
        if (cards != null)
        {
            foreach (GameObject go in cards)
            {
                if(go.transform.position.x > cardgo.transform.position.x)
                    go.transform.position += new Vector3(-xOffset, 0, 0);
            }
        }
    }
    public List<String> getMyCardList()
    {
        List<String> myEachCardName = new List<string>();
        foreach (GameObject cardgo in cards)
        {
            myEachCardName.Add((cardgo.GetComponent<Image>().sprite.name).ToString());
        }
        return myEachCardName;
        //Invoke("ClearList", 2);
    }
    public int GetMyHandCardCount()
    {
        return cards.Count;
    }
    /*public void getMyCardListSync()
    {
        showMyCard = true;
    }
    public List<string> returnCardList()
    {
        return myEachCardName;
    }
    private void ClearList()
    {
        myEachCardName.Clear();
    }*/
}
