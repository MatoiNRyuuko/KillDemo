using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Result
    {
        public Result(int id,int userId,int totalcount,int wincount)
        {
            this.Id = id;
            this.UserId = userId;
            this.TotalCount = totalcount;
            this.WinCount = wincount;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalCount { get; set; }
        public int WinCount { get; set; }
    }
}
