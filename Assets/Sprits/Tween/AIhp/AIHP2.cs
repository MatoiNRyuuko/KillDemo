using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHP2 : MonoBehaviour {

    public UISprite AIhp;
    public AI ai;
    public void Awake()
    {
    }
    void Update()
    {

        if (ai.hp < 3)
        {
            AIhp.color = Color.grey;
        }
        if (ai.hp >= 3)
        {
            AIhp.color = Color.green;
        }
    }
}
