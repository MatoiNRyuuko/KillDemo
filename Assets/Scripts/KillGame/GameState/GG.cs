using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GG : MonoBehaviour {
    private Image gamePanelImage;
    private string showtext = "";
    private Text show;
    private void Awake()
    {
        gamePanelImage = transform.GetComponent<Image>();
        show = GameObject.Find("ShowText").GetComponent<Text>();
    }
    private void Update()
    {
        if (showtext != "")
        {
            show.text = showtext;
            gamePanelImage.color = Color.gray;
        }
    }
    public void showGG(string result)
    {
        showtext = result;
        show.DOFade(1, 1);
    }
}
