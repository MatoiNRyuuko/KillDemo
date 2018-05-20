using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour {

    private List<Image> enemyHpList = new List<Image>();
    private bool changeColor = false;
    private int EnemyHpCount = 4;
    private Image enemyHp4, enemyHp3, enemyHp2, enemyHp1;
    private void Awake()
    {
        enemyHp1 = GameObject.Find("EnemyHp/HP1").GetComponent<Image>();
        enemyHpList.Add(enemyHp1);
        enemyHp2 = GameObject.Find("EnemyHp/HP2").GetComponent<Image>();
        enemyHpList.Add(enemyHp2);
        enemyHp3 = GameObject.Find("EnemyHp/HP3").GetComponent<Image>();
        enemyHpList.Add(enemyHp3);
        enemyHp4 = GameObject.Find("EnemyHp/HP4").GetComponent<Image>();
        enemyHpList.Add(enemyHp4);
    }
    private void Update()
    {
        if (changeColor)
        {
            HpColor(EnemyHpCount);
            changeColor = false;
        }

    }

    public void LoseHp(string cardname)
    {
        if (cardname == "sha" || cardname == "juedou")
            EnemyHpCount--;
        if (cardname == "shandian")
            EnemyHpCount -= 3;
        changeColor = true;
    }
    public void HealEnemyHp()
    {
        if(EnemyHpCount < 4)
        {
            EnemyHpCount++;
            changeColor = true;
        }
    }
    private void HpColor(int hp)
    {
        if (hp == 4)
        {
            for (int i = 0; i < hp; i++)
            {
                enemyHpList[i].color = Color.green;
            }
        }
        if (hp == 3)
        {
            for (int i = 0; i < hp; i++)
            {
                enemyHpList[i].color = Color.green;
            }
            enemyHpList[3].color = Color.gray;
        }
        if (hp == 2)
        {
            for (int i = 0; i < hp; i++)
            {
                enemyHpList[i].color = Color.yellow;
            }
            enemyHpList[2].color = Color.gray;
            enemyHpList[3].color = Color.gray;
        }
        if (hp == 1)
        {
            for (int i = 1; i < 4; i++)
            {
                enemyHpList[i].color = Color.gray;
            }
            enemyHpList[0].color = Color.red;
        }
        if (hp < 1)
        {
            for (int i = 0; i < 4; i++)
            {
                enemyHpList[i].color = Color.gray;
            }
        }
    }
    public int GetEnemyHp()
    {
        return EnemyHpCount;
    }
}
