using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class UserData
{
    public UserData(string userData)
    {
        string[] strs = userData.Split(',');
        this.Id = int.Parse(strs[0]);
        this.Username = strs[1];
        this.Totalcount = int.Parse(strs[2]);
        this.Wincount = int.Parse(strs[3]);
    }

    public UserData(string username,int totalCount,int winCount)
    {
        this.Username = username;
        this.Totalcount = totalCount;
        this.Wincount = winCount;
    }
    public UserData(int id,string username, int totalCount, int winCount)
    {
        this.Id = id;
        this.Username = username;
        this.Totalcount = totalCount;
        this.Wincount = winCount;
    }

    public int Id { get; set; }
    public string Username { get; private set; }
    public int Wincount { get;private set; }
    public int Totalcount { get;private set; }
    
}

