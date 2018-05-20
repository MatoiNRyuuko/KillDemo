using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfomation : MonoBehaviour {
    private Button kabeiButton;
    private string thisCardName = "";
    private GameController gameController;
    private void Awake()
    {
        kabeiButton = GetComponent<Button>();
        kabeiButton.onClick.AddListener(OnKABEIClick);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void OnKABEIClick()
    {
        gameController.JudgSC(thisCardName);
    }
    public void GetCardName(string name)
    {
        thisCardName = name;
    }
}
