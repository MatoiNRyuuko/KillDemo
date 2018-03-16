using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHP0 : MonoBehaviour {
    public UISprite AIhp;
    public AI ai;
    public void Awake()
    {
    }
    void Update()
    {
        if (ai.hp == 0)
        {
            AIhp.color = Color.grey;
        }
        if (ai.hp == 1)
        {
            AIhp.color = Color.red;
        }
        if (ai.hp == 2)
        {
            AIhp.color = Color.yellow;
        }
        if (ai.hp > 2)
        {
            AIhp.color = Color.green;
        }
    }
}
