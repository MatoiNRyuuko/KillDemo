using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[10240];
        private int startIndex = 0;
        public byte[] DATA
        {
            get
            {
                return data;
            }
        }
        public int StartIndex
        {
            get
            {
                return startIndex;
            }
        }
        public int RemainSize
        {
            get
            {
                return data.Length - StartIndex;
            }
        }
        public void ReadMessage(int dataAmount,Action<RequestCode,ActionCode,string> processDataCallBack)
        {
            startIndex += dataAmount;
            if(startIndex <= 4)
            {
                return;
            }
            int count = BitConverter.ToInt32(data, 0);
            if(startIndex - 4>= count)
            {
                RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                string s = Encoding.UTF8.GetString(data, 12, count-8);
                Console.WriteLine("解析出一条数据" + s);
                processDataCallBack(requestCode, actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
        }
        public static byte[] PackData(ActionCode actionData, string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)actionData);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            int dataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            byte[] newBytes = dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>();
            return newBytes.Concat(dataBytes).ToArray<byte>();
        }
    }
}
