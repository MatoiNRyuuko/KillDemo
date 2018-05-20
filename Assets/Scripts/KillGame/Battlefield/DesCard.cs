using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DesCard : MonoBehaviour {
    public static DesCard _instance;
    private Image image;

    void Awake()
    {
        _instance = this;
        image = gameObject.GetComponent<Image>();
        
    }
    public void ShowCard(Sprite sp)
    {
        gameObject.GetComponent<Image>().sprite = sp;
        image.DOFade(1, 0.1f).OnComplete(() => image.DOFade(0, 3));
    }
}
