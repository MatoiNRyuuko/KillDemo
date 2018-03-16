using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShunChai : MonoBehaviour {
    public List<GameObject> ShunchaiCardList = new List<GameObject>();
    public List<GameObject> KabeiList = new List<GameObject>();
    public Transform OriPosition;
    public Transform AIfromCard;
    public Transform AItoCard;
    public MyCard mycard;
    public float offset;
    public GameObject shunchai;
    private UISprite nowGenerateCard;
    private void Awake()
    {
        offset = AIfromCard.transform.position.x - AItoCard.transform.position.x;
    }
    private void Start()
    {
        offset = AIfromCard.position.x - AItoCard.position.x;
    }
    public GameObject  ActOnAIcard()
    {

        
        GameObject go = NGUITools.AddChild(this.gameObject, shunchai);
        nowGenerateCard = go.GetComponent<UISprite>();
        go.transform.position = AIfromCard.position;
        nowGenerateCard.spriteName = "kabei";
            
        return go;
        /*foreach (GameObject getCard in mycard.AIcards)
        {*/
        //go.transform.position = OriPosition.position + new Vector3(offset, 0, 0) * KabeiList.Count;
        //ShunchaiCardList.Add(go);
        //}

        //go = getCard;
        //go.GetComponent<UISprite>().spriteName = "kabei";

        //ShunchaiCardList.Add(getCard);


    }
    public void Kabei(GameObject kabei)
    {
        GameObject go;
        go = kabei;
        go.transform.parent = this.transform;
        go.transform.position = OriPosition.position + new Vector3(offset, 0, 0) * KabeiList.Count;
        KabeiList.Add(go);
    }
}
