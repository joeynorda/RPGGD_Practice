using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//消息解析函数 未分模块的消息解析器
public static class CmdParser
{
    public static void OnLogin(Cmd cmd)
    {
        //cmd -> LoginCmd
        LoginCmd loginCmd =cmd as LoginCmd;
        if (loginCmd == null)
        {
            Debug.LogError(string.Format("需要{0},但是收到了{1}", typeof(LoginCmd), cmd.GetType()));
            return;
        }

        //验证账号密码


        //等待 服务器返回玩家存档，如果存档为空 则跳转创建角色界面，如果不为空 加载


        //获取玩家存档
        Server.Instance.CurPlayer = new Player();

        //向客户端发送玩家的已创建的角色列表 发送给客户端
        RoleListCmd roleListCmd = new RoleListCmd();

        //浅拷贝
        //roleListCmd.AllRole = Server.Instance.CurPlayer.AllRole.GetRange(0, Server.Instance.CurPlayer.AllRole.Count);

        //需要使用深拷贝 每一个数据都拷贝一份
        foreach (var role in Server.Instance.CurPlayer.AllRole)
        {
            var roleInfo = new SelectRoleInfo() { Name = role.Name, ModelID = role.ModelID };

            roleListCmd.AllRole.Add(roleInfo);
        }


        Debug.Log("<color=#7FFF00><size=12>" + $"服务器接收到客户端的登录消息，验证成功后 返回用户角色列表" + "</size></color>");

        //发送客户端
        Server.Instance.SendCmd(roleListCmd);
    }





    /// <summary>
    /// 客户端接收到服务器返回的角色列表消息i
    /// </summary>
    /// <param name="cmd"></param>
    public static void OnRoleList(Cmd cmd)
    {
        
    }





}
