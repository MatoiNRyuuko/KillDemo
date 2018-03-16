using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP2 : MonoBehaviour {
    public UISprite myhp;
    public MyCard mycard;
    public void Awake()
    {
    }
    void Update()
    {
        if(mycard.MyHp >=3)
        {
            myhp.color = Color.green;
        }
        if (mycard.MyHp < 3 )
        {
            myhp.color = Color.grey;
        }
    }
}
