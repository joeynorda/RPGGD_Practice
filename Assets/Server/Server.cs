using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 服务器
/// </summary>
public class Server : Singleton<Server>, IServer
{
    IClient _client;


    //消息类型，消息解析函数
    Dictionary<Type, Action<Cmd>> _parser = new Dictionary<Type, Action<Cmd>>();


    //当前登录的玩家 暂时只保留一个玩家
    public Player CurPlayer;


    public Server()
    {

        //消息解析字典
        _parser.Add(typeof(LoginCmd), CmdParser.OnLogin);

    }



    //客户端连接后调用 1对多
    public void Connect(IClient client)
    {
        this._client = client;
    }


    public void Recive(Cmd cmd)
    {
        //_client.send

        Debug.Log("<color=#7FFF00><size=12>" + $"服务器接收到：{cmd.GetType()}" + "</size></color>");

        //消息分发
        if (_parser.TryGetValue(cmd.GetType(), out Action<Cmd> func))
        {
            func?.Invoke(cmd);
        }
    }

    public void SendCmd(Cmd cmd)
    {
        _client.Recive(cmd);
    }
}
