using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP1 : MonoBehaviour {
    public UISprite myhp;
    public MyCard mycard;
    public void Awake()
    {
    }
    void Update()
    {
        if(mycard.MyHp == 2)
        {
            myhp.color = Color.yellow;
        }
        if(mycard.MyHp < 2)
        {
            myhp.color = Color.grey;
        }
        if (mycard.MyHp > 2)
        {
            myhp.color = Color.green;
        }
    }
}
