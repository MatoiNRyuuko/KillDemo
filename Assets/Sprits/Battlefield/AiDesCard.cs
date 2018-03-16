using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDesCard : MonoBehaviour {
    public static AiDesCard _instance;
    private UISprite sprite;
    private float showTime = 3f;
    private bool isShow = false;
    private float timer = 0;

    void Awake()
    {
        _instance = this;
        sprite = this.GetComponent<UISprite>();
        //this.gameObject.SetActive(false);
        sprite.alpha = 0;
    }

    private void Update()
    {
        if (isShow)
        {
            timer += Time.deltaTime;
            if (timer > showTime)
            {
                sprite.alpha = 0;
                timer = 0;
                isShow = false;

            }
            else
            {
                sprite.alpha = (showTime - timer) / showTime;
            }
        }
    }
    public void ShowCard(string cardname)
    {
        sprite.spriteName = cardname;
        sprite.alpha = 1;
        isShow = true;
        timer = 0;
    }
}
