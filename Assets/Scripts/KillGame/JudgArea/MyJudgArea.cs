using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MyJudgArea : MonoBehaviour {
    private Image shandian;
    private Image le;
    private Image bing;
    private void Awake()
    {
        shandian = transform.Find("shandian").GetComponent<Image>();
        le = transform.Find("le").GetComponent<Image>();
        bing = transform.Find("bing").GetComponent<Image>();
    }
    public void ShowMyJudgCard(string cardname)
    {
        if(cardname == "shandian")
        {
            shandian.DOFade(1, 1);
        }
        if (cardname == "le")
        {
            le.DOFade(1, 1);
        }
        if (cardname == "bing")
        {
            bing.DOFade(1, 1);
        }
    }
    public void FadeAllJudgCard()
    {
        shandian.DOFade(0, 1);
        le.DOFade(0, 1);
        bing.DOFade(0, 1);
    }
    public void FadeMyJudgCard(string cardname)
    {
        if (cardname == "shandian")
        {
            shandian.DOFade(0, 1);
        }
        if (cardname == "le")
        {
            le.DOFade(0, 1);
        }
        if (cardname == "bing")
        {
            bing.DOFade(0, 1);
        }
    }
}
