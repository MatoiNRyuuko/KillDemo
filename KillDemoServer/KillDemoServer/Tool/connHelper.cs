using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    class connHelper
    {
        public const string CONNECTIONSTRING = "datasource=127.0.0.1;port=3306;database=game01;username=root;pwd=root;";

        public static MySqlConnection connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONSTRING);
            try
            {
                conn.Open();
                return conn;
            }
            catch(Exception e)
            {
                Console.WriteLine("连接数据库报错,因为:" + e);
                return null;
            }
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            if(conn != null)
            {
                conn.Close();
            }
        }
    }
}
