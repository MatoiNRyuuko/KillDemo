using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    private UISprite sprite;
    private static Card c_Instance;
    private string CardName
    {
        get
        {
            if(sprite == null)
            {
                sprite = this.GetComponent<UISprite>();
            }
            return sprite.spriteName;
        }
        
    }



}
