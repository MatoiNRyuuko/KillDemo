using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Model;

namespace GameServer.Servers
{
    class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();
        private MySqlConnection mysqlConn;

        private Room room;
        private User user;
        private Result result;

        public Client(Socket Client, Server Server)
        {
            this.clientSocket = Client;
            this.server = Server;
            mysqlConn = connHelper.connect();
        }
        public MySqlConnection MySQLCONN
        {
            get { return mysqlConn; }
        }

        
        public void Start()
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(msg.DATA, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (clientSocket == null || clientSocket.Connected == false) return;
                int count = clientSocket.EndReceive(ar);
                if(count == 0)
                {
                    Close();
                }
                msg.ReadMessage(count, OnProcessMessage);
                Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
            
        }

        private void OnProcessMessage(RequestCode requestCode,ActionCode actionCode,string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
            Console.WriteLine(actionCode);
        }

        private void Close()
        {
            connHelper.CloseConnection(mysqlConn);
            if (clientSocket != null)
            {
                clientSocket.Close();
                
            }
            if (room != null)
            {
                room.Close(this);
            }
            server.RemoveClient(this);
        }

        public void Send(ActionCode actionCode,string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }

        public void SetUserData(User user, Result result)
        {
            this.user = user;
            this.result = result;
        }
        public string GetUserData()
        {
            return user.Id + "," + user.Username + "," + result.TotalCount + "," + result.WinCount;
        }
        public int GetUserId()
        {
            return user.Id;
        }

        public Room Room
        {
            set { room = value; }
            get { return room; }
        }
        public bool IsHouseOwner()
        {
            return room.IsHouseOwner(this);
        }
    }
}
