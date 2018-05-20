using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour {
    private Button outCardButton;
    private Button cancleButton;
    private Button endTurnButton;
    private bool endButtonState = false;
    private bool disableButton = false;
    private bool cancleButtonState = false;
    private EndTurnRequest endTurnRequest;
    private GameController gameController;
    void Awake()
    {
        endTurnRequest = GetComponent<EndTurnRequest>();

        outCardButton = transform.Find("OutCardButton").GetComponent<Button>();
        cancleButton = transform.Find("CancleButton").GetComponent<Button>();
        endTurnButton = transform.Find("EndTurnButton").GetComponent<Button>();
        endTurnButton.onClick.AddListener(OnEndTurnButtonClick);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        DisableAllButton();
    }
	void Update () {
        if (endButtonState)
        {
            EndTurnButtonState(endButtonState);
        }
        else
        {
            EndTurnButtonState(endButtonState);
        }
        if (cancleButtonState)
        {
            CancleButtonState(cancleButtonState);
        }
        else
        {
            CancleButtonState(cancleButtonState);
        }
        if (disableButton)
        {
            DisableAllButton();
            disableButton = false;
        }
	}
    public void DisableAllButton()
    {
        outCardButton.interactable = false;
        cancleButton.interactable = false;
        endTurnButton.interactable = false;
    }
    public void DisableAllButtonSync()
    {
        disableButton = true;
    }
    public void EndTurnButtonStateSync(bool s)
    {
        endButtonState = s;
    }
    public void EndTurnButtonState(bool s)
    {
        endTurnButton.interactable = s;
    }
    public void OutcardButtonStartup()
    {
        outCardButton.interactable = true;
    }
    public void ShutDownOutcardButton()
    {
        outCardButton.interactable = false;
    }
    public void OnEndTurnButtonClick()
    {
        gameController.FlodTurn();
    }
    public void EndTurn()
    {
        endTurnRequest.SendRequest();
    }
    public void CancleButtonStateSync(bool s)
    {
        cancleButtonState = s;
    }
    private void CancleButtonState(bool s)
    {
        cancleButton.interactable = s;
    }
}
