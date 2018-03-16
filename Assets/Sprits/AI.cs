using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public int hp = 4;

    public int AICardCount = 0;

    public MyCard mycard;

    public bool youshan = false;

    //这两个用来判定AI的牌是否生效
    public bool Meyoushan = false;

    public bool Meyouwuxie = false;
    //定位从AI手牌中的哪张牌为顺或者拆的变量

    public int shunchainum = 0;
    public UILabel AIcardCountLabel;
    public Sed sed;
    public bool isJudging = false;
    public bool iscancled = false;
    public bool AIyousha = false;
    public bool thinkover = false;
    public bool youwuxie = false;
    //拆或者顺在选牌
    public bool choosingcard = false;
    public bool judgingShun = false;
    public bool judgingChai = false;
    //判定AI出过杀了
    public bool chuguosha = false;
    //这两个用来判断AI出牌后我们判定要出闪还是无懈
    public bool judgshan = false;
    public bool judgwuxie = false;
    public bool judgwuzhong = false;
    public bool judgshun = false;
    public bool judgchai = false;

    public float timer = 0;
    public GameObject AIchupai;
    public GameObject down;
    public CardGenerator cardGenerator;
    public ShunChai shunchai;
    public GameObject AIshun;
    public Transform card02;
    public Transform card01;
    private float Orix;
    private Vector3 toPosition;
    private void Awake()
    {
        float xoffset = card02.position.x - card01.position.x;
        //定义出的那张牌的x坐标，用来给后面的牌做判断要不要位移

        toPosition = new Vector3(-xoffset, 0, 0);
    }

    public void JudgmentKnightsOfThunder(GameObject Judgment)
    {
        if (Judgment.GetComponent<UISprite>().spriteName == "sha")
        {
            //杀的动画
            GameObject killtween = GameObject.Find("KillTween");
            UITweener tweener = killtween.GetComponent<UITweener>();
            tweener.PlayForward();

            foreach (GameObject haveshan in mycard.AIcards)
            {
                if (haveshan.GetComponent<UISprite>().spriteName == "shan")
                {
                    //将出的牌显示在画面上
                    AiDesCard._instance.ShowCard(haveshan.GetComponent<UISprite>().spriteName);
                    youshan = true;

                    mycard.AIcards.Remove(haveshan);
                    GameObject.Destroy(haveshan);
                    break;
                }
            }
            if (youshan == false)
            {
                isKilled();
            }
            youshan = false;
        }
        if (Judgment.GetComponent<UISprite>().spriteName == "wuzhong")
        {
            foreach (GameObject havewuxie in mycard.AIcards)
            {
                if (havewuxie.GetComponent<UISprite>().spriteName == "wuxie")
                {
                    //将出的牌显示在画面上
                    AiDesCard._instance.ShowCard(havewuxie.GetComponent<UISprite>().spriteName);
                    youwuxie = true;
                    mycard.AIcards.Remove(havewuxie);
                    GameObject.Destroy(havewuxie);
                    break;
                }
            }
            if(youwuxie == false)
            {
                StartCoroutine(GetCard());
                StartCoroutine(GetCard());
            }
            if (youwuxie == true)
            {
                youwuxie = false;
            }
        }
        if (Judgment.GetComponent<UISprite>().spriteName == "shun")
        {
            
            foreach (GameObject havewuxie in mycard.AIcards)
            {
                if (havewuxie.GetComponent<UISprite>().spriteName == "wuxie")
                {
                    //将出的牌显示在画面上
                    AiDesCard._instance.ShowCard(havewuxie.GetComponent<UISprite>().spriteName);
                    youwuxie = true;
                    mycard.AIcards.Remove(havewuxie);
                    GameObject.Destroy(havewuxie);
                    break;
                }
            }
            if (youwuxie == false)
            {
                for (int i = 0; i < mycard.AIcards.Count; i++)
                {
                    shunchai.Kabei(shunchai.ActOnAIcard());
                }
                judgingShun = true;
            }
            else if (youwuxie == true)
            {
                youwuxie = false;
                foreach(GameObject go in shunchai.ShunchaiCardList)
                {
                    shunchai.KabeiList.Remove(go);
                    Destroy(go);
                }
            }
        }
        if (Judgment.GetComponent<UISprite>().spriteName == "chai")
        {

            foreach (GameObject havewuxie in mycard.AIcards)
            {
                if (havewuxie.GetComponent<UISprite>().spriteName == "wuxie")
                {
                    //将出的牌显示在画面上
                    AiDesCard._instance.ShowCard(havewuxie.GetComponent<UISprite>().spriteName);
                    youwuxie = true;
                    mycard.AIcards.Remove(havewuxie);
                    GameObject.Destroy(havewuxie);
                    break;
                }
            }
            if (youwuxie == false)
            {
                for (int i = 0; i < mycard.AIcards.Count; i++)
                {
                    shunchai.Kabei(shunchai.ActOnAIcard());
                }
                judgingChai = true;
            }
            else if (youwuxie == true)
            {
                youwuxie = false;
                foreach (GameObject go in shunchai.ShunchaiCardList)
                {
                    shunchai.KabeiList.Remove(go);
                    Destroy(go);
                }
            }
        }

    }

    public void AIoutcard()
    {
        
        foreach (GameObject AIcard in mycard.AIcards)
        {
            if (AIcard.GetComponent<UISprite>().spriteName == "sha"&&chuguosha == false)
            {
                chuguosha = true;
                AIyousha = true;
                AIchupai = AIcard;
                //杀的动画
                GameObject killtween = GameObject.Find("KillTween");
                UITweener tweener = killtween.GetComponent<UITweener>();
                tweener.PlayForward();
                //将出的牌显示在画面上
                AiDesCard._instance.ShowCard(AIchupai.GetComponent<UISprite>().spriteName);

                mycard.AIcards.Remove(AIchupai);
                Destroy(AIchupai);

                foreach (GameObject shan in mycard.cards)
                {
                    if (shan.GetComponent<UISprite>().spriteName == "shan")
                    {
                        Meyoushan = true;
                        isJudging = true;
                        judgshan = true;
                        /*mycard.RemoveCard(shan);
                        GameObject.Destroy(shan);*/
                        //将牌组中的第一章闪立起来
                        MoveUpJudg(shan);
                        //激活出牌按钮
                        GameObject outcard = GameObject.Find("OutCard");
                        UIButton button = outcard.GetComponent<UIButton>();
                        button.state = UIButton.State.Normal;
                        button.GetComponent<BoxCollider>().enabled = true;
                        down = shan;
                        break;
                    }
                }
                if (Meyoushan == false)
                {
                    mycard.MyHp--;
                }
                if (Meyoushan == true)
                {
                    Meyoushan = false;
                }
            }
            if(AIcard.GetComponent<UISprite>().spriteName == "tao" && hp < 4)
            {
                mycard.AIcards.Remove(AIcard);
                Destroy(AIcard);
                AiDesCard._instance.ShowCard(AIcard.GetComponent<UISprite>().spriteName);
                {
                    hp++;
                }
                
            }
            if (AIcard.GetComponent<UISprite>().spriteName == "wuzhong")
            {
                AIchupai = AIcard;
                
                AiDesCard._instance.ShowCard(AIchupai.GetComponent<UISprite>().spriteName);
                mycard.AIcards.Remove(AIchupai);
                Destroy(AIchupai);
                foreach (GameObject wuxie in mycard.cards)
                {
                    if (wuxie.GetComponent<UISprite>().spriteName == "wuxie")
                    {
                        isJudging = true;
                        judgwuxie = true;
                        Meyouwuxie = true;
                        judgwuzhong = true;
                        MoveUpJudg(wuxie);
                        //激活出牌按钮
                        GameObject outcard = GameObject.Find("OutCard");
                        UIButton button = outcard.GetComponent<UIButton>();
                        button.state = UIButton.State.Normal;
                        button.GetComponent<BoxCollider>().enabled = true;
                        down = wuxie;
                        break;
                    }
                }
                if (Meyouwuxie == false)
                {
                    AIGetCard();
                    AIGetCard();
                }
                if (Meyouwuxie == true)
                {
                    Meyouwuxie = false;
                }
            }
            if (AIcard.GetComponent<UISprite>().spriteName == "shun"&&mycard.cards.Count != 0)
            {
                AIchupai = AIcard;

                AiDesCard._instance.ShowCard(AIchupai.GetComponent<UISprite>().spriteName);
                mycard.AIcards.Remove(AIchupai);
                Destroy(AIchupai);
                foreach (GameObject wuxie in mycard.cards)
                {
                    if (wuxie.GetComponent<UISprite>().spriteName == "wuxie")
                    {
                        isJudging = true;
                        judgwuxie = true;
                        Meyouwuxie = true;
                        judgshun = true;
                        MoveUpJudg(wuxie);
                        //激活出牌按钮
                        GameObject outcard = GameObject.Find("OutCard");
                        UIButton button = outcard.GetComponent<UIButton>();
                        button.state = UIButton.State.Normal;
                        button.GetComponent<BoxCollider>().enabled = true;
                        down = wuxie;
                        break;
                    }
                }
                if (Meyouwuxie == false)
                {
                    
                    int x = UnityEngine.Random.Range(0, mycard.cards.Count);
                    int position = 0;
                    foreach(GameObject wodiu in mycard.cards)
                    {
                        
                        if (position == x)
                        {
                            Orix = wodiu.transform.position.x;
                            AIshun = cardGenerator.AIGetMyCard(wodiu);
                            Destroy(wodiu);
                            position = 0;
                            break;
                        }
                        position++;
                    }
                    mycard.AIcards.Add(AIshun);
                    mycard.cards.RemoveAt(x);
                    foreach (GameObject cgo in mycard.cards)
                    {
                        if (cgo.transform.position.x > Orix)
                        {
                            cgo.transform.Translate(toPosition);
                        }
                    }

                }
                if (Meyouwuxie == true)
                {
                    Meyouwuxie = false;
                }
            }
            if (AIcard.GetComponent<UISprite>().spriteName == "chai")
            {
                AIchupai = AIcard;

                AiDesCard._instance.ShowCard(AIchupai.GetComponent<UISprite>().spriteName);
                mycard.AIcards.Remove(AIchupai);
                Destroy(AIchupai);
                foreach (GameObject wuxie in mycard.cards)
                {
                    if (wuxie.GetComponent<UISprite>().spriteName == "wuxie")
                    {
                        isJudging = true;
                        judgwuxie = true;
                        Meyouwuxie = true;
                        judgchai = true;
                        MoveUpJudg(wuxie);
                        //激活出牌按钮
                        GameObject outcard = GameObject.Find("OutCard");
                        UIButton button = outcard.GetComponent<UIButton>();
                        button.state = UIButton.State.Normal;
                        button.GetComponent<BoxCollider>().enabled = true;
                        down = wuxie;
                        break;
                    }
                }
                if (Meyouwuxie == false)
                {

                    int x = UnityEngine.Random.Range(0, mycard.cards.Count);
                    int position = 0;
                    foreach (GameObject wodiu in mycard.cards)
                    {

                        if (position == x)
                        {
                            Orix = wodiu.transform.position.x;
                            Destroy(wodiu);
                            position = 0;
                            break;
                        }
                        position++;
                    }
                    mycard.cards.RemoveAt(x);
                    foreach (GameObject cgo in mycard.cards)
                    {
                        if (cgo.transform.position.x > Orix)
                        {
                            cgo.transform.Translate(toPosition);
                        }
                    }

                }
                if (Meyouwuxie == true)
                {
                    Meyouwuxie = false;
                }
            }
            GameObject.Find("ThinkingAnimation").GetComponent<UISprite>().enabled = true;
        }
        chuguosha = false;
        thinkover = true;
        

    }

    public void isKilled()
    {
        hp--;
        youshan = false;
    }
    private void Update()
    {
        AIcardCountLabel.text = Convert.ToString(mycard.AIcards.Count);
        //timer += Time.deltaTime;

    }
    public void MoveUpJudg(GameObject c)
    {
        float a = 0.1f;
        Vector3 toPosition = new Vector3(0, a, 0);
        c.transform.Translate(toPosition);
    }
    public void MoveDownJudg(GameObject c)
    {
        float a = 0.1f;
        Vector3 toPosition = new Vector3(0, -a, 0);
        c.transform.Translate(toPosition);
    }
    public void ClickToCancle()
    {
        iscancled = true;
    }
    public IEnumerator GetCard()
    {
        GameObject cardGo;
        cardGo = cardGenerator.RadomGenerateCard();
        yield return new WaitForSeconds(1f);
        mycard.AddCard(cardGo);
    }
    public void AIGetCard()
    {
        GameObject cardGo;
        cardGo = cardGenerator.AIRadomGenerateCard();
        mycard.AIAddCard(cardGo);
    }

}
