using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

public class RoomPanel : BasePanel {
    private Text localPlayerName;
    private Text localPlayerTotalcount;
    private Text localPlayerWincount;

    private Text enemyPlayerName;
    private Text enemyPlayerTotalcount;
    private Text enemylPlayerWincount;

    private Transform localPlayerPanel;
    private Transform enemyPlayerPanel;
    private Transform startButton;
    private Transform exitButton;

    private UserData ud = null;
    private UserData ud1 = null;
    private UserData ud2 = null;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;
    //判断页面是否要弹出，用主线程的方式弹出。因为回调函数不在主线程，修改不了UI界面。
    private bool IsPop = false;
    private bool LoadScene = false;

    private void Start()
    {
        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();

        enemyPlayerPanel = transform.Find("EnemyPlayerPanel");
        localPlayerPanel = transform.Find("LocalPlayerPanel");
       
        startButton = transform.Find("StartButton");
        exitButton = transform.Find("ExitButton");

        localPlayerName = transform.Find("LocalPlayerPanel/username").GetComponent<Text>();
        localPlayerTotalcount = transform.Find("LocalPlayerPanel/totalcount").GetComponent<Text>();
        localPlayerWincount = transform.Find("LocalPlayerPanel/wincount").GetComponent<Text>();

        enemyPlayerName = transform.Find("EnemyPlayerPanel/username").GetComponent<Text>();
        enemyPlayerTotalcount= transform.Find("EnemyPlayerPanel/totalcount").GetComponent<Text>();
        enemylPlayerWincount= transform.Find("EnemyPlayerPanel/wincount").GetComponent<Text>();

        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartButtonClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitButtonClick);
        EnterAnim();
    }
    private void Update()
    {
        if (ud != null)
        {
            SetLocalPlayerRes(ud.Username, ud.Totalcount, ud.Wincount);
            ClearEnemyRes();
            ud = null;
        }
        if (ud1 != null)
        {
            SetLocalPlayerRes(ud1.Username, ud1.Totalcount, ud1.Wincount);
            if (ud2 != null)
                SetEnemyPlayerRes(ud2.Username, ud2.Totalcount, ud2.Wincount);
            else
                ClearEnemyRes();
            ud1 = null; ud2 = null;
        }
        if(IsPop == true)
        {
            uiMng.PopPanel();
            IsPop = false;
        }
        /*if(LoadScene == true)
        {
            LoadSceneSync();
            LoadScene = false;
        }*/
    }

    public override void OnEnter()
    {
        if(localPlayerPanel!= null)
        {
            EnterAnim();
        }
    }
    public override void OnExit()
    {
        ExitAnim();
    }
    public override void OnPause()
    {
        ExitAnim();
    }
    public override void OnResume()
    {
        EnterAnim();
    }
    
    /// <summary>
    /// 在创建房间时候调用，三个数据通过gamefacade的setUserData方法获取。
    /// </summary>
    public void SetLocalPlayerResSync()
    {
        ud = facade.GetUserData();
    }
    public void SetAllPlayerResSync(UserData ud1,UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }

    public void SetLocalPlayerRes(string username, int totalcount, int wincount)
    {
        localPlayerName.text = username;
        localPlayerTotalcount.text = "总场数:" + totalcount;
        localPlayerWincount.text = "胜场数:" + wincount;
    }
    public  void SetEnemyPlayerRes(string username, int totalcount, int wincount)
    {
        enemyPlayerName.text = username;
        enemyPlayerTotalcount.text = "总场数:" + totalcount;
        enemylPlayerWincount.text = "胜场数:" + wincount;
    }
    public void ClearEnemyRes()
    {
        enemyPlayerName.text = "";
        enemyPlayerTotalcount.text = "等待玩家加入....";
        enemylPlayerWincount.text = "";
    }
    /// <summary>
    /// 
    /// </summary>
    private void OnStartButtonClick()
    {
        startGameRequest.SendRequest();
    }
    private void OnExitButtonClick()
    {
        quitRoomRequest.SendRequest();
    }
    public void OnExitResponse()
    {
        IsPop = true;
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        localPlayerPanel.localPosition = new Vector3(-1000, 0, 0);
        localPlayerPanel.DOLocalMoveX(-193, 0.4f);
        enemyPlayerPanel.localPosition = new Vector3(1000, 0, 0);
        enemyPlayerPanel.DOLocalMoveX(144, 0.4f);
        startButton.localScale = Vector3.zero;
        startButton.DOScale(1, 0.4f);
        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1, 0.4f);
    }
    private void ExitAnim()
    {
        enemyPlayerPanel.DOLocalMoveX(-1000, 0.4f);
        enemyPlayerPanel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
    }
    public void OnStartResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Fail)
        {
            uiMng.ShowMessageSync("你不是房主不能开启游戏(；´д｀)ゞ");
        }
        else
        {
            uiMng.PushPanelSync(UIPanelType.Game);
        }
    }
}
