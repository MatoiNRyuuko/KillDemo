using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour {

    public Text username;
    public Text totalCount;
    public Text winCount;
    public Button joinButton;

    private int id;
    private RoomListPanel panel;

    // Use this for initialization
    void Start()
    {
        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinClick);
        }
    }
    public void SetRoomInfo(int id,string username, int totalCount, int winCount,RoomListPanel panel)
    {
        SetRoomInfo(id, username, totalCount.ToString(), winCount.ToString(), panel);
    }
    public void SetRoomInfo(int id, string username, string totalCount, string winCount, RoomListPanel panel)
    {
        this.id = id;
        this.username.text = username;
        this.totalCount.text = "总场数\n" + totalCount;
        this.winCount.text = "胜利\n" + winCount;
        this.panel = panel;
    }

    private void OnJoinClick()
    {
        panel.OnJoinClick(id);
    }

    public void DestroySelf()
    {
        GameObject.Destroy(this.gameObject);
    }
}
