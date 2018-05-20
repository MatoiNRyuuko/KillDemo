using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {
    private Button startButton;
    private void Awake()
    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(OnLoginClick);
    }
    public override void OnEnter()
    {
        EnterAnimation();
    }
    private void OnLoginClick()
    {
        uiMng.PushPanel(UIPanelType.Login);
        HideAnimation();
    }
    public override void OnPause()
    {
        HideAnimation();
    }
    public override void OnResume()
    {
        EnterAnimation();
    }
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        startButton.transform.DOScale(1, 0.3f);
    }
    private void HideAnimation()
    {
        startButton.transform.DOScale(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
}
