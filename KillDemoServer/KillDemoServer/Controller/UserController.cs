using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.Model;
using GameServer.DAO;

namespace GameServer.Controller
{
    class UserController:BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private ResultDAO resultDAO = new ResultDAO();
        public UserController()
        {
            requestcode = RequestCode.User;
        }
        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user = userDAO.VerityUser(client.MySQLCONN, strs[0], strs[1]);
            if(user == null)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result res = resultDAO.GetResultByUserid(client.MySQLCONN, user.Id);
                client.SetUserData(user, res);
                return string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, res.TotalCount, res.WinCount);
            }
        }
        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = userDAO.GetUserByUsername(client.MySQLCONN, username);
            if(res == true)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                userDAO.AddUser(client.MySQLCONN, username, password);
                return ((int)ReturnCode.Success).ToString();
            }
        }
    }
    
}
