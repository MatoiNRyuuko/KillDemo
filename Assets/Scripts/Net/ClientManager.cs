using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

public class ClientManager : BaseManager {
    private const string IP= "127.0.0.1";
    private const int PORT= 88;
    private Socket clientSocket;
    private Message msg = new Message();
    public ClientManager(GameFacade facade) : base(facade)
    {

    }
    public override void OnInit()
    {
        base.OnInit();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
        }
        catch(Exception e)
        {
            Debug.Log("无法连接到服务器端，原因是："+e);
        }    
    }
    //客户端发送数据的方法，发送数据前先进行打包PackData，再进行发送
    public void SendRequest(RequestCode requestCode,ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        
        string str = System.Text.Encoding.UTF8.GetString(bytes);
        clientSocket.Send(bytes);
        
    }
    private void Start()
    {
        //StartIndex表示从DATA数组中的哪个位置开始接受，remainSize表示最大的接受数据大小
        clientSocket.BeginReceive(msg.DATA,msg.StartIndex,msg.RemainSize,SocketFlags.None, ReceiveCallBack, null);
    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);

            msg.ReadMessage(count, OnProcessDataCallBack);

            Start();
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnProcessDataCallBack(ActionCode actionCode,string data)
    {
        facade.HandleReponse(actionCode, data);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            clientSocket.Close();
        }
        catch(Exception e)
        {
            Debug.Log("无法关闭客户端，原因是" + e);
        }
    }
}
