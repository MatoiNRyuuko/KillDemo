using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHP3 : MonoBehaviour {

    public UISprite AIhp;
    public AI ai;
    public void Awake()
    {
    }
    void Update()
    {
       
        if (ai.hp == 4)
        {
            AIhp.color = Color.green;
        }
        if (ai.hp < 4)
        {
            AIhp.color = Color.grey;
        }
    }
}
