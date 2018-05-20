using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingBattle,
        Battle,
        End
    }
    class Room
    {
        private const int MAX_HP = 200;
        public List<Client> clientRoom = new List<Client>();
        private RoomState state = RoomState.WaitingJoin;
        private Server server;
        private string cardname ="";

        public Room(Server server)
        {
            this.server = server;
        }

        public bool IsWatingJoin()
        {
            return state == RoomState.WaitingJoin;
        }
        /// <summary>
        /// AddClient和RemoveClient是在创建房间和销毁房间的调用方法
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            clientRoom.Add(client);
            client.Room = this;
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingBattle;
            }
        }
        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client);
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingBattle;
            }
            else
            {
                state = RoomState.WaitingJoin;
            }
        }
        public string GetHouseOwnerData()
        {
            return clientRoom[0].GetUserData();
        }
        public void Close(Client client)
        {
            if (client == clientRoom[0])
            {
                server.RemoveRoom(this);
            }
            else
            {
                clientRoom.Remove(client);
            }
        }
        public int GetId()
        {
            if (clientRoom.Count > 0)
            {
                return clientRoom[0].GetUserId();
            }
            return -1;
        }
        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Client client in clientRoom)
            {
                sb.Append(client.GetUserData() + "|");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public void BroadcastMessage(Client excludeclient, ActionCode actionCode, string data)
        {
            foreach (Client client in clientRoom)
            {
                if (client != excludeclient)
                {
                    server.SendResponse(client, actionCode, data);
                }
            }
        }
        public bool IsHouseOwner(Client client)
        {
            return client == clientRoom[0];
        }
        public void Close()
        {
            foreach (Client client in clientRoom)
            {
                client.Room = null;
            }
            server.RemoveRoom(this);
        }
        public void StartTimer()
        {
            new Thread(WhoFirst).Start();
        }
        public void WhoFirst()
        {
            Thread.Sleep(8500);
            BroadcastMessage(null, ActionCode.WhoFirst, "判定中");
            Thread.Sleep(3000);
            Random rd = new Random();
            int result = rd.Next(0, 1);
            if (result == 0)
                Broadcast0First();
            else
                Broadcast1First();
            //BroadcastMessage(null, ActionCode.StartPlay, "r");
        }
        private void Broadcast0First()
        {
            server.SendResponse(clientRoom[0], ActionCode.WhoFirst, "你先手");
            server.SendResponse(clientRoom[1], ActionCode.WhoFirst, "对方先手");
        }
        private void Broadcast1First()
        {
            server.SendResponse(clientRoom[0], ActionCode.WhoFirst, "对方先手");
            server.SendResponse(clientRoom[1], ActionCode.WhoFirst, "你先手");
        }
        //判定区卡牌的结果
        public void Judging(string cardname)
        {
            this.cardname = cardname;
            new Thread(JudgResult).Start();
        }
        private void JudgResult()
        {
            Thread.Sleep(3000);
            if (cardname == "le")
            {
                Random rd = new Random();
                int result = rd.Next(0, 3);
                Console.WriteLine(result);
                if (result != 1)
                    BroadcastMessage(null, ActionCode.JudgCard, "Success");
                else
                    BroadcastMessage(null, ActionCode.JudgCard, "Fail");
            }
            if(cardname == "bing")
            {
                Random rd = new Random();
                int result = rd.Next(0, 3);
                Console.WriteLine(result);
                if (result != 2)
                    BroadcastMessage(null, ActionCode.JudgCard, "Success");
                else
                    BroadcastMessage(null, ActionCode.JudgCard, "Fail");
            }
        }
    }
    

}