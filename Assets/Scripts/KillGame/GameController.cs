using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameState
{
    CardGenerating,//系统随机发牌
    Judgement,//判定阶段
    GetCard,//摸牌阶段 
    PlayCard,//出牌阶段
    Responsing,//卡牌回应阶段
    DisCard,//弃牌阶段 
    SuddenDeath,//濒死阶段 
    End//游戏回合阶段
}
public enum Player
{
    Me,
    Enemy,
    Judging
}
public class GameController : MonoBehaviour
{
    public Player player;
    public GameState state;

    private GameObject myCardGo;
    private GameObject emCardGo;
    private GameObject go;
    //关于手牌的操作
    private CardGenerator cardGenerator;
    private MyCard myCard;
    private EMCardGenerator emCardGenerator;
    private EmCard emCard;
    private CardEffect cardEffect;
    //关于按钮的操作
    private ButtonController buttonController;
    //关于血量的操作
    private MyHp myHp;
    private EnemyHp enemyHp;
    //关于顺拆的操作
    private ShunChaiShowArea shunchaiShowArea;
    private string shunOrchai = "";
    //判定的操作
    private MyJudgArea myJudgArea;
    private EnemyJudgArea enemyJudgArea;
    private JudgCardRequest judgCardRequest;
    private List<string> MyJudgCardList = new List<string>();
    private List<string> EnemyJudgCardList = new List<string>();
    private Text showText;
    private string showString = "";

    //private bool disableMycard = false;
    private bool isResponsing = false;
    private bool isActive = false;
    private bool isMyDraw = false;
    private bool isEnemyDraw = false;
    private bool isEnemyShun = false;
    private bool isEnemyChai = false;
    private bool isMyNormalDraw = false;
    private bool isEnemyNormalDraw = false;
    private bool isLe = false;
    private bool isBing = false;
    private bool isFloding = false;
    private bool isHealing = false;
    //弃牌操作
    private string flodName = "";

    //关于状态显示的操作
    private GameStateController gameStateController;
    private string healName = "";

    //游戏结束的操作
    private GGRequest ggRequest;
    private void Awake()
    {
        player = Player.Judging;
        state = GameState.CardGenerating;

        //关于手牌的操作
        myCardGo = GameObject.Find("MyCard");
        emCardGo = GameObject.Find("EnemyCard");
        cardGenerator = myCardGo.GetComponent<CardGenerator>();
        myCard = myCardGo.GetComponent<MyCard>();
        emCardGenerator = emCardGo.GetComponent<EMCardGenerator>();
        emCard = emCardGo.GetComponent<EmCard>();
        cardEffect = GetComponent<CardEffect>();

        //关于按钮的操作
        buttonController = GameObject.Find("Button").GetComponent<ButtonController>();

        //关于血量的操作
        myHp = GameObject.Find("MyHp").GetComponent<MyHp>();
        enemyHp = GameObject.Find("EnemyHp").GetComponent<EnemyHp>();

        //关于顺拆的操作
        shunchaiShowArea = GameObject.Find("ShunChaiArea").GetComponent<ShunChaiShowArea>();

        //关于判定阶段的操作
        myJudgArea = GameObject.Find("JudgArea/MyJudgArea").GetComponent<MyJudgArea>();
        enemyJudgArea = GameObject.Find("JudgArea/EnemyJudgArea").GetComponent<EnemyJudgArea>();
        judgCardRequest = GameObject.Find("JudgArea").GetComponent<JudgCardRequest>();

        //关于游戏状态的操作
        gameStateController = GameObject.Find("GameState").GetComponent<GameStateController>();
        ggRequest = transform.parent.gameObject.GetComponent<GGRequest>();
    }
    private void Update()
    {
        if(state == GameState.End)
        {
            buttonController.DisableAllButtonSync();
        }
        if (player == Player.Me)
        {
            if (state == GameState.Judgement)
            {
                if (isBing)
                {
                    state = GameState.PlayCard;
                    isBing = false;
                }
                else
                    state = GameState.GetCard;
                myJudgArea.FadeAllJudgCard();
                MyJudgCardList.Clear();
            }
            if (state == GameState.GetCard)
            {
                MyDraw();
                state = GameState.PlayCard;
            }
            if (state == GameState.PlayCard)
            {
                gameStateController.PlayStateUp();
                EndTurnButtonStateSync(true);
                if (isActive)
                    buttonController.OutcardButtonStartup();
                else
                    buttonController.ShutDownOutcardButton();
            }
            //弃牌阶段
            if(state == GameState.DisCard)
            {
                gameStateController.FlodStateUp();
                EndTurnButtonStateSync(false);
                if (needFlod() == false)
                {
                    buttonController.EndTurn();
                    state = GameState.End;
                }
                if (isFloding)
                    buttonController.OutcardButtonStartup();
                else
                    buttonController.ShutDownOutcardButton();
            }
            
        }
        //濒死阶段
        if (state == GameState.SuddenDeath)
        {
            gameStateController.HealStateUp();
            if (myHp.GetMyHp() <= 0)
            {
                EndTurnButtonStateSync(false);
                CancleButtonStateSync(true);
                if (isHealing)
                    buttonController.OutcardButtonStartup();
                else
                    buttonController.ShutDownOutcardButton();
                Debug.Log(isHealing);
            }
            if (enemyHp.GetEnemyHp() <= 0)
            {
                buttonController.ShutDownOutcardButton();
                EndTurnButtonStateSync(false);
                CancleButtonStateSync(false);
                if (enemyHp.GetEnemyHp() > 0)
                {
                    state = GameState.PlayCard;
                }
            }
        }
        if (state == GameState.Responsing)
        {
            EndTurnButtonStateSync(false);
            if (isResponsing)
                buttonController.OutcardButtonStartup();
            else
                buttonController.ShutDownOutcardButton();
        }
        if (player == Player.Enemy)
        {
            EndTurnButtonStateSync(false);
            if (state == GameState.Judgement)
            {
                if (isBing)
                {
                    state = GameState.PlayCard;
                    isBing = false;
                }
                else
                    state = GameState.GetCard;
                enemyJudgArea.FadeAllJudgCard();
                EnemyJudgCardList.Clear();
            }
            if (state == GameState.GetCard)
            {
                EnemyDraw();
                state = GameState.PlayCard;
            }
            if (state == GameState.PlayCard)
            {
                gameStateController.PlayStateUp();
            }
        }
        //抽牌的主线程操作
        if (isMyDraw)
        {
            MyDraw();
            MyDraw();
            isMyDraw = false;
        }
        if (isEnemyDraw)
        {
            EnemyDraw();
            EnemyDraw();
            isEnemyDraw = false;
        }
        if (isEnemyShun)
        {
            EnemyDraw();
            isEnemyShun = false;
        }
        if (isEnemyChai)
        {
            Debug.Log("对面什么也没干");
        }
        if (isMyNormalDraw)
        {
            MyDraw();
            isMyNormalDraw = false;
        }
        if (isEnemyNormalDraw)
        {
            EnemyDraw();
            isEnemyNormalDraw = false;
        }
    }
    public void DisableAllButtonSync()
    {
        buttonController.DisableAllButtonSync();
    }
    private void DisableHandCards()
    {
        myCard.DisableHandCard();
    }
    /// <summary>
    /// 判断卡能不能出
    /// </summary>
    /// <param name="usecard"></param>
    public void UseCard(GameObject usecard)
    {
        Sprite cardsp = usecard.GetComponent<Image>().sprite;
        string cardname = cardsp.name.ToString();
        if (state == GameState.PlayCard && player == Player.Me)
        {
            isActive = MyTurnUseCard(cardname);
        }
        if (state == GameState.Responsing)
        {
            isResponsing = cardEffect.UseCardOnEffect(cardname);
        }
        if(state == GameState.DisCard)
        {
            isFloding = FlodTurnUseCard(cardname);
        }
        if(state == GameState.SuddenDeath)
        {
            isHealing = SuddenDeathUseCard(cardname);
        }
    }
    //我的回合能不能出牌
    private bool MyTurnUseCard(string cardname)
    {
        if (cardname != "shan" && cardname != "wuxie" && EnemyGotJudgCard(cardname) && isLe == false)
        {
            if(cardname =="chai"||cardname == "shun")
            {
                if (emCard.GetEnemyHandCardCount() == 0)
                    return false;
            }
            if(cardname == "bing"|| cardname == "le")
            {
                return GetJudg(cardname);
            }
            if(cardname == "tao" && myHp.GetMyHp()==4)
            {
                return false;
            }
            return true;
        }
  
        return false;
    }
    //弃牌阶段出牌
    private bool FlodTurnUseCard(string cardname)
    {
        flodName = cardname;
        if(flodName == "")
            return false;
        return true;      
    }
    //濒死阶段出牌
    private bool SuddenDeathUseCard(string cardname)
    {
        healName = cardname;
        if (cardname != "tao")
            return false;
        return true;
    }
    public void DownCard()
    {
        isActive = false;
        isResponsing = false;
        flodName = "";
        healName = "";
        isFloding = false;
        isHealing = false;
    }


    public void ChangeTurn(string data)
    {
        if (data == "Enemy end turn")
        {
            player = Player.Me;
            state = GameState.Judgement;
            //MyJudgAreaHaveCard();
        }
        if (data == "You end your turn")
        {
            player = Player.Enemy;
            state = GameState.Judgement;
            isLe = false;
        }
    }
    public void EndTurnButtonStateSync(bool s)
    {
        buttonController.EndTurnButtonStateSync(s);
    }
    public void CancleButtonStateSync(bool s)
    {
        buttonController.CancleButtonStateSync(s);
    }
    public void LoseMyHp(string cardname)
    {
        myHp.LoseHp(cardname);
    }
    public void LoseEnemyHp(string cardname)
    {
        enemyHp.LoseHp(cardname);
    }
    public void HealMyHp()
    {
        myHp.HealMyHp();
    }
    public void HealEnemyHp()
    {
        enemyHp.HealEnemyHp();
    }
    public void MyDrawSync()
    {
        isMyDraw = true;
    }
    private void MyDraw()
    {
        GameObject go = cardGenerator.RadomGenerateCard();
        myCard.AddCard(go);
    }
    /// <summary>
    /// 我得到顺的牌或者拆对面牌的操作
    /// </summary>
    /// <param name="cardname"></param>
    public void GetMyShuncard(string cardname)
    {
        GameObject go = shunchaiShowArea.GetMyShunCard(cardname);
        myCard.AddCard(go);
        cardEffect.SendDestroyRequest(cardname);
        shunchaiShowArea.ClearAreaList();
        emCard.DestroyEnemyCard();
    }
    public void ChaiEnemyCard(string cardname)
    {
        cardEffect.SendDestroyRequest(cardname);
        shunchaiShowArea.ClearAreaList();
        emCard.DestroyEnemyCard();
    }
    public void JudgSC(string cardname)
    {
        if (shunOrchai == "Shun")
            GetMyShuncard(cardname);
        if (shunOrchai == "Chai")
            ChaiEnemyCard(cardname);

        shunOrchai = "";
    }
    public void ShunOrChai(string soc)
    {
        shunOrchai = soc;
    }
    public void EnemyDrawSync()
    {
        isEnemyDraw = true;
    }
    private void EnemyDraw()
    {
        GameObject go = emCardGenerator.EnemyGenerateCard();
        emCard.AddCard(go);
    }
    public List<string> getMyCardList()
    {
        return myCard.getMyCardList();
    }
    //用来销毁被顺掉或者被拆掉的卡
    public void RemoveCard(string cardname)
    {
        if (shunOrchai == "Shun")
            isEnemyShun = true;
        if (shunOrchai == "Chai")
            isEnemyChai = true;
        myCard.RemoveCardSync(cardname);
        shunOrchai = "";
    }
    //判定区域的操作
    public void ShowMyJudgCard(string cardname)
    {
        myJudgArea.ShowMyJudgCard(cardname);
        MyJudgCardList.Add(cardname);
        if(cardname == "le")
            isLe = true;
        if (cardname == "bing")
            isBing = true;
    }
    public void ShowEnemyJudgCard(string cardname)
    {
        enemyJudgArea.ShowEnemyJudgCard(cardname);
        EnemyJudgCardList.Add(cardname);
        if (cardname == "bing")
            isBing = true;
    }
    private bool EnemyGotJudgCard(string cardname)
    {
        foreach (string name in EnemyJudgCardList)
        {
            if (name == cardname)
                return false;
        }
        return true;
    }
    //关于弃牌操作
    public void FlodTurn()
    {
        state = GameState.DisCard;
    }
    private bool needFlod()
    {
        if (myHp.GetMyHp() < myCard.GetMyHandCardCount())
            return true;
        return false;
    }
    public void ResetFlodName()
    {
        flodName = "";
    }
    public void ResetHealingName()
    {
        healName = "";
    }
    private bool GetJudg(string cardname)
    {
        foreach(string name in EnemyJudgCardList)
        {
            if (name == cardname)
                return false;
        }
        return true;
    }

    //濒死状态
    public void WhoSuddenDeath()
    {
        //判断我的血量小于0进入濒死阶段
        if (myHp.GetMyHp() <= 0 || enemyHp.GetEnemyHp() <= 0)
        {
            state = GameState.SuddenDeath;
        }
        else
        {
            if(state != GameState.Responsing)
            {
                state = GameState.PlayCard;
                buttonController.DisableAllButtonSync();
            }
        }
    }
    public void SendMyLoseRequest()
    {
        ggRequest.SendRequest();
    }
    public void GameOver()
    {
        state = GameState.End;
    }
}