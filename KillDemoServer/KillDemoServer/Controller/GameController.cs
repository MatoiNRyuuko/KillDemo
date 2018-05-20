using GameServer.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using System.Threading;

namespace GameServers.Controller
{
    class GameController : BaseController
    {
        public GameController()
        {
            requestcode = RequestCode.Game;
        }
        public string StartGame(string data, Client client, Server server)
        {
            if (client.IsHouseOwner())
            {
                Room room = client.Room;
                room.BroadcastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
                room.StartTimer();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }
        public string OutCard(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadcastMessage(client, ActionCode.ShowCard, data);
            return ((int)ReturnCode.Success).ToString();
        }
        public string EndTurn(string data, Client client, Server server)
        {
            Room room = client.Room;
            string endTurndata = "Enemy end turn";
            room.BroadcastMessage(client, ActionCode.EndTurn, endTurndata);
            return "You end your turn";
        }
        public string CardEffect(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadcastMessage(client, ActionCode.CardEffect, data);
            return "Initiative";
        }
        public string Shun(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadcastMessage(client, ActionCode.Shun, data);
            return "Initiative";
        }
        public string Chai(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadcastMessage(client, ActionCode.Chai, data);
            return "Initiative";
        }
        public string JudgCard(string data, Client client, Server server)
        {
            Room room = client.Room;
            if(data == "Finish")
                room.BroadcastMessage(null, ActionCode.JudgCard, data);
            else
            {
                room.BroadcastMessage(null, ActionCode.JudgCard, data);
                room.Judging(data);
            }
            return "return";
        }
        public string GG(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadcastMessage(client, ActionCode.GG, "You Win");
            return "You Lose";
        }
    }

}
