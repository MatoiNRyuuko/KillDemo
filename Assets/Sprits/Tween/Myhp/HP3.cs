using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP3 : MonoBehaviour {
    public UISprite myhp;
    public MyCard mycard;
    public void Awake()
    {
    }
    void Update()
    {
        if(mycard.MyHp == 4)
        {
            myhp.color = Color.green;
        }
        else
        {
            myhp.color = Color.grey;
        }
    }
}
