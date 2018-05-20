using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShunChaiShowArea : MonoBehaviour {
    private HorizontalLayoutGroup ShunChaiArea;
    private List<GameObject> showAreaCardGo = new List<GameObject>();
    private GameObject myCard;
    public GameObject ShunChaiKabei;
    public GameObject ShunChaiCard;
    private string shunORchai = "";
    private void Awake()
    {
        ShunChaiArea = transform.Find("ShunChaiSrollRect/Layout").GetComponent<HorizontalLayoutGroup>();
        myCard = GameObject.Find("MyCard");
    }
    public void ShowUp(List<string> data)
    {
        for(int i = 0;i < data.Count; i++)
        {
            GameObject DesCard = Instantiate(ShunChaiKabei);
            DesCard.GetComponent<CardInfomation>().GetCardName(data[i]);
            DesCard.transform.SetParent(ShunChaiArea.transform);
            showAreaCardGo.Add(DesCard);
        }
    }
    public GameObject GetMyShunCard(string cardname)
    {
        GameObject shunCard = Instantiate(ShunChaiCard);
        Sprite sp = Resources.Load("Textrues/Card/" + cardname, typeof(Sprite)) as Sprite;
        shunCard.GetComponent<Image>().sprite = sp;
        shunCard.transform.parent = myCard.transform;
        return shunCard;
    }
    public void ClearAreaList()
    {
        for(int i= 0; i < showAreaCardGo.Count; i++)
        {
            Destroy(showAreaCardGo[i]); 
        }
        showAreaCardGo.Clear();
    }
    public void ShunOrChai(string soc)
    {
        shunORchai = soc;
    }
}
