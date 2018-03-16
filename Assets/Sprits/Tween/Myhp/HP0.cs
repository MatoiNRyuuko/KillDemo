using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP0 : MonoBehaviour {
    public UISprite myhp;
    public MyCard mycard;
    public void Awake()
    {
    }
    void Update()
    {
        if (mycard.MyHp == 0)
        {
            myhp.color = Color.grey;
        }
        if (mycard.MyHp == 1)
        {
            myhp.color = Color.red;
        }
        if (mycard.MyHp == 2)
        {
            myhp.color = Color.yellow;
        }
        if (mycard.MyHp > 2)
        {
            myhp.color = Color.green;
        }
    }
}
