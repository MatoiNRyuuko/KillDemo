using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using GameServer.Model;

namespace GameServer.DAO
{
    class UserDAO
    {
        public User VerityUser(MySqlConnection conn,string username,string password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username = @username and password =@password", conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                //准备一个reader，通过Read方法读取数据
                reader = cmd.ExecuteReader();
                //这个if判断是否读到上面给的信息，不是分开读，是username和password一起读。
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("在VerityUser时候出现异常" + e);
            }
            finally
            {
                reader.Close();
            }
            return null;
            
            
        }

        public bool GetUserByUsername(MySqlConnection conn, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("Select * from user where username = @username", conn);

                cmd.Parameters.AddWithValue("username", username);
                //准备一个reader，通过Read方法读取数据
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                else
                {
                    return false ;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetUserByUsername时候出现异常" + e);
            }
            finally
            {
                reader.Close();
            }
            return false;
        }

        public void AddUser(MySqlConnection conn, string username, string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into user set username = @username , password =@password", conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                //执行cmd语句的方法
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser时候出现异常" + e);
            }
            finally
            {
               
            }
        }
    }
}
