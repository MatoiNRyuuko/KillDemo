using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCardController : MonoBehaviour,IPointerDownHandler{
    private MyCard myCard;
    private GameController gameController;
    private Transform oriPosition;
    private Vector3 offsetY;
    private BoxCollider boxCollider;
    private bool Up = false;
    private bool Down = false;
    private void Awake()
    {
        myCard = GameObject.Find("MyCard").GetComponent<MyCard>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        oriPosition = GameObject.Find("MyCard/card01").transform;
    }

    /*void OnPress(bool isDown)
    {
        GetYLerp();
        if (isDown && offsetY.y <0.01f&&myCard.IsSelect==false) 
        {
            MoveUp();
            myCard.IsSelect = true;
        }
        else if(isDown && offsetY.y > 0.01f && myCard.IsSelect)
        {
            MoveDown();
            myCard.IsSelect = false;
        }
    }*/
    private void Update()
    {
        if (Up)
        {
            MoveUp();
            Up = false;
        }
        if (Down)
        {
            MoveDown();
            Down = false;
        }
    }
    private void MoveUp()
    {
        Vector3 toPosition = new Vector3(0, 40f, 0);
        gameObject.transform.Translate(toPosition);
        gameController.UseCard(gameObject);
    }
    private void MoveDown()
    {
        Vector3 toPosition = new Vector3(0, -40f, 0);
        gameObject.transform.Translate(toPosition);
        gameController.DownCard();
    }
    private void GetYLerp()
    {
        offsetY.y = Mathf.Abs(gameObject.transform.position.y - oriPosition.transform.position.y);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GetYLerp(); 
        if (offsetY.y < 1f && myCard.IsSelect == false)
        {
            Up = true;
            myCard.IsSelect = true;
        }
        else if (offsetY.y >1f && myCard.IsSelect)
        {
            Down = true;
            myCard.IsSelect = false;
        }
    }
}
