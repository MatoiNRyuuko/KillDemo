using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    class RoomController : BaseController
    {
        public RoomController()
        {
            requestcode = RequestCode.Room;
        }
        public string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client);
            return ((int)ReturnCode.Success).ToString();
        }
        public string ListRoom(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Room room in server.GetRoomList())
            {
                if (room.IsWatingJoin())
                {
                    sb.Append(room.GetHouseOwnerData() + "|");
                }
            }
            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public string JoinRoom(string data, Client client, Server server)
        {
            int id = int.Parse(data);
            Room room = server.GetRoomById(id);
            if(room == null)
            {
                return ((int)ReturnCode.NotFound).ToString();
            }
            else if(room.IsWatingJoin() == false)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                room.AddClient(client);
                string roomdata = room.GetRoomData();
                room.BroadcastMessage(client, ActionCode.UpdateRoom, roomdata);
                return ((int)ReturnCode.Success).ToString() +"-" + roomdata;
            }
        }
        public string QuitRoom(string data, Client client, Server server)
        {
            bool isHouserOwner = client.IsHouseOwner();
            Room room = client.Room;
            if (isHouserOwner == true)
            {
                room.BroadcastMessage(client, ActionCode.QuitRoom, ((int)ReturnCode.Success).ToString());
                room.Close();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                client.Room.RemoveClient(client);
                room.BroadcastMessage(client, ActionCode.UpdateRoom, room.GetRoomData());
                return ((int)ReturnCode.Success).ToString();
            }
        }
    }
}
