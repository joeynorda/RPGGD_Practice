using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServer
{

    //连接服务器 并给当前连接的客户端赋值
    void Connect(IClient client);

    void SendCmd(Cmd cmd);

    void Recive(Cmd cmd);
}

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

        //登录消息
        _parser.Add(typeof(LoginCmd), CmdParser.OnLogin);

        //角色选择消息
        _parser.Add(typeof(SelectRoleCmd), CmdParser.OnSelectRole);
    }



    //客户端连接后调用  1对多
    public void Connect(IClient client)
    {
        this._client = client;
    }



    //服务器端接收客户端消息 
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
