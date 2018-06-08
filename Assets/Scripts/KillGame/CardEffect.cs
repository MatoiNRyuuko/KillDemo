using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CardEffect : MonoBehaviour {
    private enum CardType
    {
        None,
        Normal,
        Strategy
    }
    private GameController gameController;
    private ShunChaiShowArea scShowArea;
    private ShunRequest shunRequest;
    private ChaiRequest chaiRequest;
    private CardType cardType = CardType.None;

    private bool isMycancle = false;
    private bool Dueling = false;
    //关于顺和拆
    private bool Show = false;
    private bool Shun = false;
    private bool Chai = false;
    private bool returnMycard = false;
    private string shunOrchai = "";

    private List<string> ChoosingList = new List<string>();

    private List<string> MyeffectCardName = new List<string>();
    private List<string> EnemyeffectCardName = new List<string>();

    private void Awake()
    {
        gameController = GetComponent<GameController>();
        scShowArea = GameObject.Find("ShunChaiArea").GetComponent<ShunChaiShowArea>();
        chaiRequest = GetComponent<ChaiRequest>();
        shunRequest = GetComponent<ShunRequest>();
    }
    private void Update()
    {
        if (Show)
        {
            ShowEnemyHandCard();
            Show = false;
        }
        if (returnMycard)
        {
            ReturnMyCardList();
            returnMycard = false;
        }
    }
    public void GetCardEffect(string data)
    {
        gameController.state = GameState.Responsing;
        if(data != "Initiative"&&data!="Cancle")
        {
            if (data == "juedou")
                Dueling = true;
            GetCardType(data);
            EnemyeffectCardName.Add(data);
            gameController.CancleButtonStateSync(WhosCardEffect(data));
        }
        if(data == "Initiative")
        {
            gameController.CancleButtonStateSync(WhosCardEffect(data));
        }
        if(data == "Cancle")
        {
            WhosCardEffect(data);
            gameController.state = GameState.PlayCard;
        }
        gameController.WhoSuddenDeath();
    }
    //判断在别人回合我的卡能不能用
    public bool UseCardOnEffect(string cardname)
    {
        if(cardType == CardType.Normal && EnemyeffectCardName[0] == "sha")
        {
            if (cardname == "shan")
                return true;
        }
        if (cardType == CardType.Normal && Dueling)
        {
            if (cardname == "sha")
                return true;
        }
        if(cardType == CardType.Strategy)
        {
            if (cardname == "wuxie")
                return true;
            if(EnemyeffectCardName[0] == "juedou")
            {
                if (cardname == "sha")
                    return true;
            }
        }
        return false;
    }
    //清除卡牌的效果
    public void ClearCardEffect()
    {
        MyeffectCardName.Clear();
        EnemyeffectCardName.Clear();
        cardType = CardType.None;
    }
    //判断卡是锦囊还是普通牌
    private void GetCardType(string cardname)
    {
        if(cardname == "sha"|| cardname == "tao" || cardname == "shan")
            cardType = CardType.Normal;
        else
            cardType = CardType.Strategy;
    }
    public void AddMyCardEffect(string cardname)
    {
        if (cardname == "juedou")
            Dueling = true;
        MyeffectCardName.Add(cardname);
    }
    public bool WhosCardEffect(string s)
    {
        if(gameController.player == Player.Me)
        {
            if (s == "shan")
            {
                ClearCardEffect();
                gameController.state = GameState.PlayCard;
                return false;
            }
            if (s == "tao")
            {
                EnemyCardEffect();
                //TODO判定回没回够血
                gameController.state = GameState.PlayCard;
                return false;
            }
            if (s == "Cancle")
            {
                MyCardEffect();
                gameController.state = GameState.PlayCard;
                return false;
            }
            //这里做的是取消按钮或者桃决斗的判断
            if (s == "Initiative")
            {
                if (MyeffectCardName[0] == "tao")
                {
                    gameController.HealMyHp();
                    ClearCardEffect();
                    gameController.state = GameState.PlayCard;
                } 
                if (isMycancle)
                {
                    if (MyeffectCardName[0] == "juedou")
                    {
                        gameController.LoseMyHp("juedou");                
                    }
                    ClearCardEffect();
                    gameController.state = GameState.PlayCard;
                    isMycancle = false;
                }
                //如果决斗出问题了可能是这里
                return false;
            }
            return true;
        }
        if (gameController.player == Player.Enemy)
        {
            if (s == "Initiative")
            {
                //TODO 决斗的判定还没做
                if (isMycancle)
                {
                    EnemyCardEffect();
                    gameController.state = GameState.PlayCard;
                    isMycancle = false;
                }
                else
                {
                    ClearCardEffect();
                }
                return false;
            }
            if (s == "tao")
            {
                EnemyCardEffect();
                gameController.state = GameState.PlayCard;
                return false;
            }
            if (s == "Cancle")
            {
                gameController.state = GameState.PlayCard;
                if (EnemyeffectCardName[0] == "juedou")
                {
                    gameController.LoseEnemyHp("juedou"); 
                }
                ClearCardEffect();
                return false;
            }
            return true;
        }
        return false;
    }
    private void MyCardEffect()
    {
        if(MyeffectCardName[0] == "sha")
        {
            gameController.LoseEnemyHp("sha");
            Debug.Log("对面-1hp");
        }
        if(MyeffectCardName[0] == "juedou")
        {
            if (isMycancle)
                gameController.LoseMyHp("juedou");
            else
                gameController.LoseEnemyHp("juedou");
            Dueling = false;
        }
        if(MyeffectCardName[0] == "wuzhong")
        {
            gameController.MyDrawSync();
            gameController.MyDrawSync();
            //TODO
            Debug.Log("我抽两张");
        }
        if(MyeffectCardName[0] == "bing")
        {
            //TODO
            gameController.ShowEnemyJudgCard("bing");
            Debug.Log("对面被兵");
        }
        if (MyeffectCardName[0] == "shun")
        {
            //TODO
            Debug.Log("对面被顺");
        }
        if (MyeffectCardName[0] == "chai")
        {
            //TODO
            Debug.Log("对面被拆");
        }
        if (MyeffectCardName[0] == "le")
        {
            //TODO
            gameController.ShowEnemyJudgCard("le");
            Debug.Log("对面被乐");
        }
        if (MyeffectCardName[0] == "shandian")
        {
            //TODO
            gameController.ShowEnemyJudgCard("shandian");
            Debug.Log("对面被挂闪电");
        }
        
        if (MyeffectCardName[0] == "taoyuan")
        {
            gameController.HealEnemyHp();
            gameController.HealMyHp();
        }
        if (MyeffectCardName[0] == "tao")
        {
            gameController.HealMyHp();
        }
        ClearCardEffect();
    }
    public void EnemyCardEffect()
    {
        if (EnemyeffectCardName[0] == "sha")
        {
            //TODO
            gameController.LoseMyHp("sha");
            Debug.Log("我-1hpsha");
        }
        if(EnemyeffectCardName[0] == "juedou")
        {
            gameController.LoseMyHp("juedou");
            Debug.Log("我-1hpjd");
            Dueling = false;
        }
        if (EnemyeffectCardName[0] == "wuzhong")
        {
            Debug.Log("对面抽两张");
            gameController.EnemyDrawSync();
            gameController.EnemyDrawSync();
        }
        if (EnemyeffectCardName[0] == "bing")
        {
            //TODO
            gameController.ShowMyJudgCard("bing");
            Debug.Log("我被兵");
        }
        if (EnemyeffectCardName[0] == "chai")
        {
            Chai = true;
            ReturnMyCardListSync();
            Debug.Log("我被拆");
        }
        if (EnemyeffectCardName[0] == "le")
        {
            //TODO
            gameController.ShowMyJudgCard("le");
            Debug.Log("我被乐");
        }
        if (EnemyeffectCardName[0] == "shandian")
        {
            //TODO
            gameController.ShowMyJudgCard("shandian");
            Debug.Log("我被挂闪电");
        }
        if (EnemyeffectCardName[0] == "shun")
        {
            Shun = true;
            ReturnMyCardListSync();
            Debug.Log("我被顺");
        }
        if (EnemyeffectCardName[0] == "taoyuan")
        {
            gameController.HealEnemyHp();
            gameController.HealMyHp();
        }
        if (EnemyeffectCardName[0] == "tao")
        {
            gameController.HealEnemyHp();
        }
        if (EnemyeffectCardName[0] == "wuxie")
        {
            gameController.state = GameState.PlayCard;
            gameController.DownCard();
            Debug.Log("我被无懈");
        }
        ClearCardEffect();
    }
    public void MyCancle()
    {
        isMycancle = true;
    }
    //把对方的手牌显示
    public void ChoosingEnemyCard(string data)
    {
        ChoosingList = GetEnemyCardList(data);
        Show = true;
    }
    private void ShowEnemyHandCard()
    {
        scShowArea.ShowUp(ChoosingList);
        ChoosingList.Clear();
    }
    //在顺的时候将我的手牌返回给对方
    private void ReturnMyCardList()
    {
        StringBuilder sb = new StringBuilder();
        List<string> ReturnList = new List<string>();
        ReturnList = gameController.getMyCardList();
        foreach(string cardname in ReturnList)
        {
            sb.Append(cardname + ",");
        }
        sb.Remove(sb.Length - 1, 1);
        Debug.Log(sb.ToString());
        if(Shun)
            shunRequest.SendRequest(sb.ToString());
        if (Chai)
            chaiRequest.SendRequest(sb.ToString());
        Shun = false;
        Chai = false;
    }
    public void ReturnMyCardListSync()
    {
        returnMycard = true;
    }
    public List<string> GetEnemyCardList(string data)
    {
        string[] cardString = data.Split(',');
        List<string> cardStringList = new List<string>();
        foreach (string cardname in cardString)
        {
            cardStringList.Add(cardname);
        }
        return cardStringList;
    }
    
    //发送让对方销毁卡牌的请求
    public void SendDestroyRequest(string cardname)
    {
        if(shunOrchai == "Shun")
            shunRequest.SendRequest("Destroy," + cardname);
        if(shunOrchai == "Chai")
            chaiRequest.SendRequest("Destroy," + cardname);
        shunOrchai = "";
    }
    //接到销毁请求后的销毁操作
    public void RemoveCard(string cardname)
    {
        gameController.RemoveCard(cardname);
    }
    public void ShunOrChai(string soc)
    {
        shunOrchai = soc;
        gameController.ShunOrChai(soc);
    }
}
