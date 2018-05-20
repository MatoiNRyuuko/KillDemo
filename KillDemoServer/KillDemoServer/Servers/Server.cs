using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using GameServer.Controller;
using Common;
namespace GameServer.Servers
{
    class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();
        private List<Room> roomList = new List<Room>();
        private ControllerManager controllerManager;

        public Server() { }
        public Server(string ipStr, int port)
        {
            controllerManager = new ControllerManager(this);
            SetIpAndPort(ipStr, port);
        }
        /// <summary>
        /// 绑定ip和端口号给socket
        /// </summary>
        public void SetIpAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            client.Start();
            clientList.Add(client);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }
        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
        public void CreateRoom(Client client)
        {
            Room room = new Room(this);
            room.AddClient(client);
            roomList.Add(room);
        }
        public List<Room> GetRoomList()
        {
            return roomList;
        }
        public void RemoveRoom(Room room)
        {
            if (roomList != null && room != null)
            {
                roomList.Remove(room);
            }
        }
        public Room GetRoomById(int id)
        {
            foreach(Room room in roomList)
            {
                if(room.GetId() == id)
                {
                    return room;
                }
            }
            return null;
        }
        /*public void BroadcastMessageInGame(List<Client> clientList, ActionCode actionCode, string data)
        {
            foreach (Client client in clientList)
            {
                this.SendResponse(client, actionCode, data);
            }
        }*/
    }
}
