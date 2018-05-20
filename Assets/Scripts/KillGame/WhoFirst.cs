using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WhoFirst : MonoBehaviour {
    private Text showText;
    private Image gamePanelImage;
    private string result = "";
    private GameController gameController;

    private void Awake()
    {
        showText = transform.Find("ShowText").GetComponent<Text>();
        gamePanelImage = gameObject.GetComponent<Image>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (result != "")
        {
            if (result == "判定中")
            {

                showText.text = "系统在决定谁的先手。。";
                gamePanelImage.DOColor(Color.gray, 1f);
                result = "";
            }
            else
            {
                JudgResult();
                ChangeGameState(result);
                result = "";
            }
        }
    }
    public void JudgResultSync(string data)
    {
        result = data;
    }
    public void JudgResult()
    {
        showText.text = result;
        gamePanelImage.DOColor(Color.white, 1f).OnComplete(() => showText.DOFade(0, 1));
    }
    private void ChangeGameState(string result)
    {
        if(result == "你先手")
        {
            gameController.player = Player.Me;
            gameController.state = GameState.GetCard;
        }
        else
        {
            gameController.player = Player.Enemy;
            gameController.state = GameState.GetCard;
        }
    }
}
