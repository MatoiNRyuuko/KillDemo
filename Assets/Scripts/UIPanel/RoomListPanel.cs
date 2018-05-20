using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class RoomListPanel : BasePanel {
    private RectTransform BattleRes;
    private RectTransform RoomList;
    private GameObject roomItemPrefab;
    private VerticalLayoutGroup roomLayout;
    private List<UserData> udList = null;
    private ListRoomRequest listRoomRequest;
    private CreateRoomRequest createRoomRequest;
    private JoinRoomRequest joinroomRequest;

    private UserData ud1= null;
    private UserData ud2 = null;

    private void Awake()
    {
        BattleRes = transform.Find("BattleRes").GetComponent<RectTransform>();
        RoomList = transform.Find("RoomList").GetComponent<RectTransform>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;

        createRoomRequest= GetComponent<CreateRoomRequest>();
        joinroomRequest = GetComponent<JoinRoomRequest>();

        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseButtonClick);
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreatRoomClick);
        transform.Find("RoomList/RefreshRoomButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);

        listRoomRequest = GetComponent<ListRoomRequest>(); 
        //transform.Find("RoomList/RefreshRoomButton").GetComponent<Button>().onClick.AddListener();

        BattleRes.localPosition = new Vector3(-1000, 0, 0);
        RoomList.localPosition = new Vector3(1000, 0, 0);
    }
    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
        if(ud1 !=null && ud2 != null)
        {
            BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
            (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
            ud1 = null;ud2 = null;
        }
    }

    private void OnCloseButtonClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    public override void OnExit()
    {
        HideAnimation();
    }
    public override void OnEnter()
    {
        SetBattleRes();
        OnEnterAnimation();
        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }
        listRoomRequest.SendRequest();
    }
    public override void OnPause()
    {
        HideAnimation();
    }
    public override void OnResume()
    {
        OnEnterAnimation();
        listRoomRequest.SendRequest();
    }

    private void OnEnterAnimation()
    {
        gameObject.SetActive(true);
        BattleRes.transform.DOLocalMove(new Vector3(-270, 0, 0), 1);
        RoomList.transform.DOLocalMove(new Vector3(150, 0, 0), 1);
    }

    private void HideAnimation()
    {
        BattleRes.transform.DOLocalMove(new Vector3(-1000, 0, 0), 1);
        RoomList.transform.DOLocalMove(new Vector3(1000, 0, 0), 1).OnComplete(() => gameObject.SetActive(false));
    }

    private void SetBattleRes()
    {
        UserData userData = facade.GetUserData();
        transform.Find("BattleRes/Username").GetComponent<Text>().text = userData.Username;
        transform.Find("BattleRes/Totalcount").GetComponent<Text>().text = "总场数："+ userData.Totalcount.ToString();
        transform.Find("BattleRes/Wincount").GetComponent<Text>().text = "胜场数："+ userData.Wincount.ToString();
    }

    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray = roomLayout.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }
        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab);
            roomItem.transform.SetParent(roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInfo(ud.Id,ud.Username, ud.Totalcount, ud.Wincount,this);
        }
        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            roomCount * (roomItemPrefab.GetComponent<RectTransform>().sizeDelta.y + roomLayout.spacing));
    }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    public void OnJoinClick(int id)
    {
        joinroomRequest.SendRequest(id);
    }
    private void OnCreatRoomClick()
    {
        BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
        createRoomRequest.SetPanel(panel);
        createRoomRequest.SendRequest();
    }
    private void OnRefreshClick()
    {
        listRoomRequest.SendRequest();
    }
    
    public void OnJoinResponse(ReturnCode returnCode,UserData ud1,UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                uiMng.ShowMessageSync("房间不存在无法加入");
                break;
            case ReturnCode.Fail:
                uiMng.ShowMessageSync("房间已满无法加入");
                break;
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;

        }
    }
}
