using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    CardGenerating,//系统随机发牌
    Judgement,//判定阶段
    Judging,//对方出牌的判定阶段
    GetCard,//摸牌阶段 
    PlayCard,//出牌阶段
    End//游戏回合阶段
}
public enum Player
{
    Me,
    AI
}
public class GameController : MonoBehaviour {
    public GameState gameState;
    public Player player;
    private CardGenerator cardGenerator;
    public MyCard myCard;
    public DesCard descard;
    public Sed sed;
    public AI ai;
    public static GameObject cardgo;
    public UILabel Onlyone;
    public Transform card01;
    public Transform card02;
    public UIButton EndTurn;
    public UILabel turn;
    public float timer = 0;
    public bool outshan = false;
    public bool cancle = false;
    public bool destroy = false;
    public GameObject getshun;
    public GameObject getcard;
    public int position = 0;
    public int AIposition = 0;
    public int reallyposition;
    UILabel shanbushan;
    public ShunChai shunchai;
    public bool getAIcard = false;
    public bool chaiAIcard = false;
    private float xoffset;
    //定义出的那张牌的x坐标，用来给后面的牌做判断要不要位移
    private float OriX;
    private Vector3 toPosition;
    private GameObject AIshun;


    private void Awake()
    {
        gameState = GameState.CardGenerating;
        this.cardGenerator = this.GetComponent<CardGenerator>();
        StartCoroutine(GenerateCardForHero());

        GameObject CancleButton = GameObject.Find("Cancle");

        UIEventListener.Get(CancleButton).onClick = CancleButtonClick;

        shanbushan = GameObject.Find("Canshan").GetComponent<UILabel>();

        xoffset = card02.position.x - card01.position.x;

        toPosition = new Vector3(-xoffset, 0, 0);
    }
    private void Start()
    {
        
    }
    private void Update()
    {

        //判定反馈的方法
        IsJudging();
        if (ai.judgingShun==true||ai.judgingChai == true)
        {
            //timer += Time.deltaTime;
            foreach (GameObject go in shunchai.KabeiList)
            {
                if (go.GetComponent<UISprite>().spriteName != "kabei")
                {
                    if(ai.judgingShun == true)
                    {
                        getAIcard = true;
                        ai.judgingShun = false;
                    }
                    if(ai.judgingChai == true)
                    {
                        chaiAIcard = true;
                        ai.judgingChai = false;
                    }
                    
                    
                    reallyposition = position;
                    break;
                }
                position++;
            }
            position = 0;
        }
        //顺手牵羊的判断
        if (getAIcard == true)
        {
            foreach (GameObject aicard in myCard.AIcards)
            {
                
                if (AIposition == reallyposition)
                {
                    getcard = cardGenerator.GetAICard(aicard);
                    myCard.AIcards.Remove(aicard);
                    Debug.Log(reallyposition);
                    Debug.Log(AIposition);

                    break;
                }
                AIposition++;
            }
            AIposition = 0;
            myCard.AddCard(getcard);
            Debug.Log(getcard);
            destroy = true;
            getAIcard = false;
        }
        if(chaiAIcard == true)
        {
            foreach (GameObject aicard in myCard.AIcards)
            {

                if (AIposition == reallyposition)
                {
                    myCard.AIcards.Remove(aicard);
                    Debug.Log(reallyposition);
                    Debug.Log(AIposition);
                    break;
                }
                AIposition++;
            }
            AIposition = 0;
            destroy = true;
            chaiAIcard = false;
        }
        if(destroy == true)
        {
            
            foreach(GameObject go in shunchai.KabeiList)
            {
                Destroy(go);
            }
            shunchai.KabeiList.Clear();
            destroy = false;
        }
        if(gameState == GameState.Judgement)
        {

        }
        if (gameState == GameState.Judging)
        {
            GameObject.Find("ThinkingAnimation").GetComponent<UISprite>().enabled = false;
        }
        //发牌阶段结束后跳转为摸牌阶段
        if (gameState == GameState.GetCard&&player == Player.Me)
        {
            StartCoroutine(GetCard());
            gameState = GameState.PlayCard;
        }
        if (gameState == GameState.GetCard && player == Player.AI)
        {
            AIGetCard();
            gameState = GameState.PlayCard;
        }
        //不在自己回合内，将结束回合按钮禁用
        if (gameState == GameState.PlayCard&& player ==Player.Me)
        {
            GameObject outcard = GameObject.Find("EndTurn");
            UIButton button = outcard.GetComponent<UIButton>();
            button.state = UIButton.State.Normal;
            button.GetComponent<BoxCollider>().enabled = true;
            shanbushan.text = "false";
        }
        
        if (gameState == GameState.PlayCard&& player == Player.AI)
        {
            GameObject.Find("ThinkingAnimation").GetComponent<UISprite>().enabled = true;
            shanbushan.text = "true";
            timer += Time.deltaTime;
            if (timer > 4f)
            {
                AITurn();
                timer = 0;
            }
            if (ai.thinkover == true)
            {
                GameObject.Find("ThinkingAnimation").GetComponent<UISprite>().enabled = false;
                player = Player.Me;
                gameState = GameState.GetCard;
                ai.thinkover = false;
            }

            


        }
        //这一段老长的代码就是，对方出杀后，10s内判断，你出不出闪 无懈之类的
        if(ai.isJudging == true)
        {

            timer += Time.deltaTime;
            UIButton button = GameObject.Find("Cancle").GetComponent<UIButton>();
            button.state = UIButton.State.Normal;
            button.GetComponent<BoxCollider>().enabled = true;

            UIButton buttonOC = GameObject.Find("OutCard").GetComponent<UIButton>();
            /*//这个if是在我的回合判定的
            if (timer < 100f&&ai.judgingShun)
            {
                foreach (GameObject cgo in shunchai.KabeiList)
                {
                    if(cgo.GetComponent<UISprite>().spriteName == "shan")
                    {
                        Debug.Log("keyi");
                    }
                }
            }*/
            //这三个ifelse是在我的回合外判定的
            if (timer > 10f)
                {
                timer = 0;
                ai.isJudging = false;
                if (ai.judgshan == true)
                {
                    ai.AIyousha = false;
                    myCard.MyHp--;
                    Debug.Log("时间到你没出杀，扣血");
                    ai.judgshan = false;
                }
                if (ai.judgwuxie == true)
                {
                    AIGetCard();
                    AIGetCard();
                    ai.judgwuxie = false;
                }

               

                button.state = UIButton.State.Disabled;
                button.GetComponent<BoxCollider>().enabled = false;

                ai.MoveDownJudg(ai.down);
                buttonOC.state = UIButton.State.Disabled;
                buttonOC.GetComponent<BoxCollider>().enabled = false;

                player = Player.AI;
                gameState = GameState.PlayCard;
            }
            else if(timer < 10f &&outshan == true)
            {
                timer = 0;
                ai.isJudging = false;
                
                if (ai.judgshan == true)
                {
                    ai.AIyousha = false;
                    outshan = false;
                    ai.judgshan = false;
                }
                if (ai.judgwuxie == true)
                {
                    outshan = false;
                    ai.judgwuxie = false;
                }


                player = Player.AI;
                gameState = GameState.PlayCard;

                button.state = UIButton.State.Disabled;
                button.GetComponent<BoxCollider>().enabled = false;
            }
            else if(timer< 10f && cancle == true)
            {
                timer = 0;
                ai.isJudging = false;
                if (ai.judgshan == true)
                {
                    myCard.MyHp--;
                    ai.AIyousha = false;
                }
                if (ai.judgwuxie == true&&ai.judgwuzhong == true)
                {
                    AIGetCard();
                    AIGetCard();
                    ai.judgwuxie = false;
                }
                if(ai.judgwuxie == true && ai.judgshun == true)
                {
                    int x = UnityEngine.Random.Range(0, myCard.cards.Count);
                    Debug.Log(x);
                    int position = 0;
                    foreach (GameObject wodiu in myCard.cards)
                    {

                        if (position == x)
                        {
                            OriX = wodiu.transform.position.x;
                            AIshun = cardGenerator.AIGetMyCard(wodiu);
                            Destroy(wodiu);
                            position = 0;
                            break;
                        }
                        position++;
                    }
                    myCard.AIcards.Add(AIshun);
                    myCard.cards.RemoveAt(x);
                    foreach (GameObject cgo in myCard.cards)
                    {
                        if (cgo.transform.position.x > OriX)
                        {
                            cgo.transform.Translate(toPosition);
                        }
                    }
                    ai.judgshun = false;
                }
                if(ai.judgwuxie == true && ai.judgchai == true)
                {
                    int x = UnityEngine.Random.Range(0, myCard.cards.Count);
                    int position = 0;
                    foreach (GameObject wodiu in myCard.cards)
                    {

                        if (position == x)
                        {
                            OriX = wodiu.transform.position.x;
                            Destroy(wodiu);
                            position = 0;
                            break;
                        }
                        position++;
                    }
                    myCard.cards.RemoveAt(x);
                    foreach (GameObject cgo in myCard.cards)
                    {
                        if (cgo.transform.position.x > OriX)
                        {
                            cgo.transform.Translate(toPosition);
                        }
                    }
                    ai.judgchai = false;
                }

                cancle = false;
                
                ai.MoveDownJudg(ai.down);

                player = Player.AI;
                gameState = GameState.PlayCard;

                buttonOC.state = UIButton.State.Disabled;
                buttonOC.GetComponent<BoxCollider>().enabled = false;

                button.state = UIButton.State.Disabled;
                button.GetComponent<BoxCollider>().enabled = false;
            }
            
        }
    }
    //游戏开始时抽4张牌的方法
    public IEnumerator GenerateCardForHero()
    {
        GameObject cardGo;
        GameObject AIcardGo;

        for (int i = 0; i < 4; i++)
        {
            //调用卡牌生成的随机生成方法
            cardGo = cardGenerator.RadomGenerateCard();
            AIcardGo = cardGenerator.AIRadomGenerateCard();
            //解决一个bug  开始卡片不移动  把下面两句话调换位置就好了  待研究
            yield return new WaitForSeconds(1f);
            myCard.AddCard(cardGo);
            myCard.AIAddCard(AIcardGo);
            if (i == 3)
            {
                gameState = GameState.GetCard;
            }

        }
    }
    //抽牌阶段抽一张牌的方法
    public IEnumerator GetCard()
    {
        GameObject cardGo;
        cardGo = cardGenerator.RadomGenerateCard();
        yield return new WaitForSeconds(1f);
        myCard.AddCard(cardGo);
    }
    public void AIGetCard()
    {
        GameObject cardGo;
        cardGo = cardGenerator.AIRadomGenerateCard();
        myCard.AIAddCard(cardGo);
    }
    public void OnButtonClick()
    {
        GameObject p = GameObject.Find("Position");

        foreach (GameObject cgo in myCard.cards)
        {
            if (cgo.transform.position.y - p.transform.position.y > 0.09f)
            {
                cardgo = cgo;
                if(cardgo.GetComponent<UISprite>().spriteName == "tao")
                {
                    DesCard._instance.ShowCard(cardgo.GetComponent<UISprite>().spriteName);
                    myCard.MyHp++;
                }
                else
                {
                    DesCard._instance.ShowCard(cardgo.GetComponent<UISprite>().spriteName);
                    //将出的牌交给AI处理
                    ai.JudgmentKnightsOfThunder(cardgo);
                }
            }
        }
        //cardgo = sed.GetMyCardObject();
        //myCard.GetPosition(cardgo);
        UISprite judg = cardgo.GetComponent<UISprite>();
        //定义出牌后 以后的牌要移动的距离
        float xoffset = card02.position.x - card01.position.x;
        //定义出的那张牌的x坐标，用来给后面的牌做判断要不要位移
        float OriX = cardgo.transform.position.x;

        Vector3 toPosition = new Vector3(-xoffset, 0, 0);

        Onlyone.text = "1";
        //调用移除gameobject的方法
        myCard.RemoveCard(cardgo);
        //从list中移除gameobject后销毁，从屏幕消失
        GameObject.Destroy(cardgo);
        //销毁卡牌后，遍历游戏组件list，如果有X坐标比销毁的卡片大的，向左位移
        foreach (GameObject cgo in myCard.cards)
        {
            if (cgo.transform.position.x > OriX)
            {
                cgo.transform.Translate(toPosition);
            }
        }

        //将按钮设置为不可用的代码
        GameObject outcard = GameObject.Find("OutCard");
        UIButton button = outcard.GetComponent<UIButton>();
        button.state = UIButton.State.Disabled;
        button.GetComponent<BoxCollider>().enabled = false;
        if (ai.isJudging == true)
        {
            outshan = true;
        }
        else
        {
            outshan = false;
        }


    }

    //结束回合的按钮，将状态设置为别人的回合，将玩家设置为AI玩家
    public void EndTheTurn()
    {
        GameObject outcard = GameObject.Find("EndTurn");
        UIButton button = outcard.GetComponent<UIButton>();
        button.state = UIButton.State.Disabled;
        button.GetComponent<BoxCollider>().enabled = false;
        player = Player.AI;
        gameState = GameState.GetCard;
        outshan = false;
        GameObject.Find("ThinkingAnimation").GetComponent<UISprite>().enabled = true;

    }
    public void AITurn()
    {
       ai.AIoutcard();


    }
    public void IsJudging()
    {
        if(ai.isJudging == true)
        {
            gameState = GameState.Judging;
        }
    }
    void CancleButtonClick(GameObject button)
    {
        cancle = true;
    }
    public void DeleteGameObject(GameObject go)
    {
        shunchai.KabeiList.Remove(go);
    }
}
