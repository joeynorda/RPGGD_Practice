using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IClient
{
    void SendCmd(Cmd cmd);
    void Recive(Cmd cmd);
}





//本地客户端
//和服务器交互 
//1.客户端直接访问服务器代码
//2.逻辑上的服务器  和服务器的交互 模块
//客户端访问服务器
public class Net : Singleton<Net>, IClient
{

    IServer _server;//当前服务器

    //消息类型、消息解析函数
    Dictionary<Type, Action<Cmd>> _parser = new Dictionary<Type, Action<Cmd>>();


    //消息缓存
    List<Cmd> _cache = new List<Cmd>();


    //当前是否在阻塞状态
    private bool _pause;

    //消息是否阻塞 不处理
    public bool Pause
    {
        get { return _pause; }
        set 
        {
            _pause = value;
            if (!_pause)
            {
                Recive(null);
            }
        }
    }

    public Net()
    {
        //客户端注册消息解析

        //客户端接收角色列表信息 
        _parser.Add(typeof(RoleListCmd), UserData.OnRoleList);


        //进入地图
        _parser.Add(typeof(EnterMapCmd), SceneMgr.OnEnterMap);


        //当前主角id
        _parser.Add(typeof(MainRoleThisIDCmd), RoleMgr.OnMainRoleThisid);


        //创建角色
        _parser.Add(typeof(CreateSceneRoleCmd), RoleMgr.OnCreateSceneRole);


    }


    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="successCallback">连接成功</param>
    /// <param name="failedCallback">连接失败</param>
    public void ConnectServer(Action successCallback, Action failedCallback)
    {
        //给变量 _server 赋值
        _server = Server.Instance;
        _server.Connect(this);

        //判断连接服务器是否成功

        if (true)
        {
            Debug.Log("<color=#7FFF00><size=12>" + $"连接服务器成功!" + "</size></color>");
            successCallback?.Invoke();
        }
        else
        {
            failedCallback?.Invoke();
        }
    }





    //客户端接收消息 相当于 服务器发送消息
    public void Recive(Cmd cmd)
    {

        if (cmd != null)
        {
            _cache.Add(cmd);
        }


        //阻塞状态 则不解析
        if (Pause)
        {
            return;
        }


        //消息缓存
        //list 消息顺序一致
        foreach (var cacheCmd in _cache)
        {
            //客户端消息分发
            if (_parser.TryGetValue(cacheCmd.GetType(), out Action<Cmd> func))
            {
                func.Invoke(cacheCmd);
            }
        }
        _cache.Clear();

    }



    //客户端发送消息 相当于 服务器接收消息
    public void SendCmd(Cmd cmd)
    {
        _server.Recive(cmd);
    }



    /// <summary>
    /// 检查消息Cmd 是否是目标类型
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static bool CheckCmd(Cmd cmd,Type targetType)
    {
        if (cmd.GetType() != targetType)
        {
            Debug.LogError(string.Format("需要{0},但是收到了{1}", targetType, cmd.GetType()));
            return false;
        }
        return true;
    }
}

