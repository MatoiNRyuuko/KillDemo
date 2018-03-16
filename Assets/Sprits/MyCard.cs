using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MyCard : MonoBehaviour {
    public GameObject cardPrefab;
    public Transform card01;
    public Transform card02;
    public Vector3 po;
    public Transform cardPo;
    public int MyHp = 4;
    public UILabel myhpcount;
    //获取物体组件
    


    //持有这个UISprite
    private UISprite sprite;

    public string[] cardnames;

    public List<GameObject> cards = new List<GameObject>();
    public List<GameObject> AIcards = new List<GameObject>();

    private float xOffset;
    private float yOffset;
    private void Awake()
    {
        myhpcount = GameObject.Find("MyhpCount").GetComponent<UILabel>();
    }
    private void Start()
    {
        
        xOffset = card02.position.x - card01.position.x;
        //yOffset = card04.position.y - card03.position.y;

    }
    private void Update()
    {
        myhpcount.text =Convert.ToString(MyHp);
    }

    public void AddCard(GameObject cardGo)
    {
        GameObject go;
        go = cardGo;
        //go.transform.parent = this.transform;
        //iTween.MoveTo(go, sed.np, 0.8f);
        if (po == cardPo.position)
        {

            Vector3 toPosition = card01.position + new Vector3(xOffset, 0, 0) * (cards.Count);
            
            iTween.MoveTo(go, toPosition, 0.8f);
        }
        else
        {
            Vector3 toPosition = po + new Vector3(0, -0.1f, 0);
            iTween.MoveTo(go, toPosition, 0.8f);
            po = cardPo.position;
        }
        cards.Add(go);
    }
    public void AIAddCard(GameObject cardGo)
    {
        GameObject go;
        go = cardGo;
        go.transform.parent = this.transform;
        AIcards.Add(go);
    }
    public void RemoveCard(GameObject go)
    {
        cards.Remove(go);
    }
    /*public void GetPosition(GameObject go)
    {
        po = go.transform.position;
    }*/


}
