using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using System.Text;
using System.Linq;

public class Message{

    private byte[] data = new byte[1024];
    //这个变量是用来定义
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
    //用来读取数据
    public void ReadMessage(int dataAmount, Action<ActionCode,string> processDataCallBack)
    {
        startIndex += dataAmount;
        if (startIndex <= 4)
        {
            return;
        }
        int count = BitConverter.ToInt32(data, 0);
        if (startIndex - 4 >= count)
        {
            ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
            string s = Encoding.UTF8.GetString(data, 8, count - 4);
            processDataCallBack(actionCode, s);
            Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
            startIndex -= (count + 4);
        }
    }
    //用来做数据的打包
    public static byte[] PackData(RequestCode requestData, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestData);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length + actionCodeBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
            .Concat(actionCodeBytes).ToArray<byte>()
            .Concat(dataBytes).ToArray<byte>();
    }
}
