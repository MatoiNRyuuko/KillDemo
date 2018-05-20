using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHp : MonoBehaviour {
    private List<Image> myHpList = new List<Image>();
    private bool changeColor = false;
    private int MyHpCount = 4;
    private Image myHp4, myHp3, myHp2, myHp1;
    private void Awake()
    {
        myHp1 = GameObject.Find("MyHp/HP1").GetComponent<Image>();
        myHpList.Add(myHp1);
        myHp2 = GameObject.Find("MyHp/HP2").GetComponent<Image>();
        myHpList.Add(myHp2);
        myHp3 = GameObject.Find("MyHp/HP3").GetComponent<Image>();
        myHpList.Add(myHp3);
        myHp4 = GameObject.Find("MyHp/HP4").GetComponent<Image>();
        myHpList.Add(myHp4);
    }
    private void Update()
    {
        if (changeColor)
        {
            HpColor(MyHpCount);
            changeColor = false;
        }
            
    }
    
    public void LoseHp(string cardname)
    {
        if (cardname == "sha" || cardname == "juedou")
            MyHpCount--;
        if (cardname == "shandian")
            MyHpCount -= 3;
        changeColor = true;
    }
    public void HealMyHp()
    {
        if (MyHpCount < 4)
        {
            MyHpCount++;
            changeColor = true;
        }
    }
    private void HpColor(int hp)
    {
        if(hp == 4)
        {
            for(int i = 0; i < hp; i++)
            {
                myHpList[i].color = Color.green;
            }
        }
        if(hp == 3)
        {
            for (int i = 0; i < hp; i++)
            {
                myHpList[i].color = Color.green;
            }
            myHpList[3].color = Color.gray;
        }
        if (hp == 2)
        {
            for (int i = 0; i < hp; i++)
            {
                myHpList[i].color = Color.yellow;
            }
            myHpList[2].color = Color.gray;
            myHpList[3].color = Color.gray;
        }
        if (hp == 1)
        {
            for (int i = 1; i < 4; i++)
            {
                myHpList[i].color = Color.gray;
            }
            myHpList[0].color = Color.red;
        }
        if (hp < 1)
        {
            for (int i = 0; i < 4; i++)
            {
                myHpList[i].color = Color.gray;
            }
        }
    }
    public int GetMyHp()
    {
        return MyHpCount;
    }
}
