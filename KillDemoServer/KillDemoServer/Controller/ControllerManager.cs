using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Reflection;
using GameServer.Servers;
using GameServers.Controller;

namespace GameServer.Controller
{
    class ControllerManager
    {
        //定义一个字典，存放RequestCode和对应的Controller
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server;

        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }
        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
            controllerDict.Add(RequestCode.Room, new RoomController());
            controllerDict.Add(RequestCode.Game, new GameController());
        }
        public void HandleRequest(RequestCode requestCode,ActionCode actionCode,string data,Client client)
        {
            BaseController controller;
            //out找到controller并返回给controller
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller,无法处理请求"); return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[警告]在Controller[" + controller.GetType() + "]中没有对应的处理方法:[" + methodName + "]"); return;
            }
            object[] parameters = new object[] { data, client ,server};
            object o = mi.Invoke(controller, parameters);

            server.SendResponse(client, actionCode, o as string);
        }
    }
}
