using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
    private Button PlayState;
    private Button FlodState;
    private Button HealState;

    private void Awake()
    {
        PlayState = transform.Find("PlayState").GetComponent<Button>();
        FlodState = transform.Find("FlodState").GetComponent<Button>();
        HealState = transform.Find("HealState").GetComponent<Button>();
    }
    public void PlayStateUp()
    {
        PlayState.interactable = true;
        FlodState.interactable = false;
        HealState.interactable = false;
    }
    public void FlodStateUp()
    {
        PlayState.interactable = false;
        FlodState.interactable = true;
        HealState.interactable = false;
    }
    public void HealStateUp()
    {
        PlayState.interactable = false;
        FlodState.interactable = false;
        HealState.interactable = true;
    }
}
