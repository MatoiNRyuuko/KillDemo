using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sed : MonoBehaviour {

    public GameObject c;
    public Transform pCard;
    UISprite go;
    GameObject Cardgo;
    public UILabel Onlyone;
    GameObject p;
    GameObject outcard;
    UILabel shanbushan;
    UILabel myhpcount;
    public MyCard mycand;
    public AI ai;
    public void Awake()
    {
        c = this.gameObject;
        p = GameObject.Find("Position");
        outcard = GameObject.Find("OutCard");
        shanbushan = GameObject.Find("Canshan").GetComponent<UILabel>();
        myhpcount = GameObject.Find("MyhpCount").GetComponent<UILabel>();
    }
    private void Update()
    {
        int myhp =Convert.ToInt32(myhpcount.text);
        //这个if判断在我的回合不能出闪和无懈
        if (c.transform.position.y - p.transform.position.y > 0.09f &&shanbushan.text=="false")
        {
            if (c.GetComponent<UISprite>().spriteName == "wuxie" || c.GetComponent<UISprite>().spriteName == "shan" )
            {
                UIButton button = outcard.GetComponent<UIButton>();
                button.state = UIButton.State.Disabled;
                button.GetComponent<BoxCollider>().enabled = false;
            }
            
        }
        //这个if判断在别人的回合我可以出闪和无懈
        if(c.transform.position.y - p.transform.position.y > 0.09f && shanbushan.text == "true")
        {
            if(c.GetComponent<UISprite>().spriteName == "shan" && c.GetComponent<UISprite>().spriteName == "wuxie")
            {
                UIButton button = outcard.GetComponent<UIButton>();
                button.state = UIButton.State.Normal;
                button.GetComponent<BoxCollider>().enabled = true;
            }
            
        }
        //这个if判断在别人的回合我不能出闪以外的牌
        /*if (ai.isJudging == false)
        {
            UIButton button = outcard.GetComponent<UIButton>();
            button.state = UIButton.State.Disabled;
            button.GetComponent<BoxCollider>().enabled = false;
        }*/
        //这个if判断我满血的时候不能吃桃
        if (c.transform.position.y - p.transform.position.y > 0.09f && c.GetComponent<UISprite>().spriteName == "tao"&&myhp==4)
        {
            UIButton button = outcard.GetComponent<UIButton>();
            button.state = UIButton.State.Disabled;
            button.GetComponent<BoxCollider>().enabled = false;
        }

    }
    public void Start()
    {
        
    }
    void OnPress(bool isDown)
    {
        if (isDown&& c.transform.position.y - p.transform.position.y < 0.0001f && Onlyone.text =="1") 
        {
            MoveUp();
            //将按钮设置为可点击的代码
            GameObject outcard = GameObject.Find("OutCard");
            UIButton button = outcard.GetComponent<UIButton>();
            button.state = UIButton.State.Normal;
            button.GetComponent<BoxCollider>().enabled = true;


            if (c.transform.position.y - p.transform.position.y > 0.09f)
            {
                Onlyone.text = "2";
            }

        }
        else if (isDown&&c.transform.position.y- p.transform.position.y>0.09f&&Onlyone.text == "2")
        {
            MoveDown();
            //将按钮设置为不可点击的代码
            GameObject outcard = GameObject.Find("OutCard");
            UIButton button = outcard.GetComponent<UIButton>();
            button.state = UIButton.State.Disabled;
            button.GetComponent<BoxCollider>().enabled = false;

            if (c.transform.position.y - p.transform.position.y < 0.0001f)
            {
                Onlyone.text = "1";
            }


        }
    }
    /*void OnDoubleClick()
    {
        this.transform.parent.GetComponent<MyCard>().RemoveCard(this.gameObject);
        this.transform.parent.GetComponent<MyCard>().GetPosition(this.gameObject);
        GameObject.Destroy(this.gameObject);
        go = this.gameObject.GetComponent<UISprite>();
        
        Debug.Log(go.spriteName);
    }*/
    public GameObject GetMyCardObject()
    {
        GameObject p = GameObject.Find("Position");
        if (c.transform.position.y - p.transform.position.y > 0.09f)
        {
            Cardgo = this.gameObject;
            Debug.Log(Cardgo);
            
        }
        return Cardgo;

    }

    

    public void MoveUp()
    {
        
        float a = 0.1f;
        Vector3 toPosition = new Vector3(0, a, 0);
        c.transform.Translate(toPosition);
        //go = this.gameObject.GetComponent<UISprite>();
        

    }
    public void MoveDown()
    {
        
        float a = 0.1f;
        Vector3 toPosition = new Vector3(0, -a, 0);
        c.transform.Translate(toPosition);
       

    }
    
}
