using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

//消息解析函数 未分模块的消息解析器
public static class CmdParser
{


    /// <summary>
    /// 登录请求
    /// </summary>
    /// <param name="cmd"></param>
    public static void OnLogin(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(LoginCmd)))
        {
            return;
        }

        LoginCmd loginCmd = cmd as LoginCmd;
        //验证账号密码


        //等待 服务器返回玩家存档，如果存档为空 则跳转创建角色界面，如果不为空 加载


        //获取玩家存档
        //去数据库里面找寻存档

        var playerData = Server.Instance.DB.GerUserData(1);
        if (playerData == null)
        {
            playerData = new Player();
            playerData.ThisID = 1;
            Server.Instance.DB.SavePlayerData(playerData);
        }

        Server.Instance.CurPlayer = playerData;

        //向客户端发送玩家的已创建的角色列表 发送给客户端
        RoleListCmd roleListCmd = new RoleListCmd();

        //浅拷贝
        //roleListCmd.AllRole = Server.Instance.CurPlayer.AllRole.GetRange(0, Server.Instance.CurPlayer.AllRole.Count);

        //需要使用深拷贝 每一个数据都拷贝一份
        foreach (var role in Server.Instance.CurPlayer.AllRole)
        {
            var roleInfo = new RoleServer() { Name = role.Name, ModelID = role.ModelID };

            roleListCmd.AllRole.Add(roleInfo);
        }


        Debug.Log("<color=#7FFF00><size=12>" + $"服务器接收到客户端的登录消息，验证成功后 返回用户角色列表" + "</size></color>");

        //发送客户端
        Server.Instance.SendCmd(roleListCmd);
    }



    /// <summary>
    /// 接收到客户端的选择角色请求
    /// 角色选择 后 进入场景
    /// </summary>
    /// <param name="cmd"></param>
    public static void OnSelectRole(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(SelectRoleCmd)))
        {
            return;
        }

        SelectRoleCmd selectRoleCmd = cmd as SelectRoleCmd;

        //服务器当前 晚间
        var curPlayer = Server.Instance.CurPlayer;


        //获取服务器当前 选中角色信息
        RoleServer curRole = curPlayer.AllRole[selectRoleCmd.Index];


        //当前选定的角色
        curPlayer.CurRole = curRole;


        //向客户端发送一个能进场景的消息

        //1.告诉客户端进入的场景编号


        //2.分配ThisID 
        var thisid = RoleServer.GetNewThisID();
        curPlayer.CurRole.ThisID = thisid;

        //3.Thisid,//告诉客户端可操控角色 主角ID   主、配角 
        MainRoleThisIDCmd mainRoleThisIDCmd = new MainRoleThisIDCmd() { ThisID = thisid };
        Debug.Log("<color=#7FFF00><size=12>" + $"服务端发送了 mainRoleThisIDCmd" + "</size></color>");
        Server.Instance.SendCmd(mainRoleThisIDCmd); //进入新场景后 才执行以后消息 服务器认为已经进入场景了 




       
        //进入场景
        EnterMap(curRole);
    }



    /// <summary>
    /// 进入场景
    /// </summary>
    /// <param name="roleCmd"></param>
    private static void EnterMap(RoleServer curRole)
    {
        var sceneID = 3;
        EnterMapCmd enterMapCmd = new EnterMapCmd() { MapID = sceneID };
        Server.Instance.SendCmd(enterMapCmd);//给客户端发送进入新场景Cmd 


        //4.生成主角 CreateSceneRole
        CreateSceneRoleCmd roleCmd = new CreateSceneRoleCmd();
        roleCmd.ThisID = curRole.ThisID;
        roleCmd.Name = curRole.Name;
        roleCmd.ModelID = curRole.ModelID;
        roleCmd.Pos = Vector3.zero;
        roleCmd.FaceTo = Vector3.forward;


        //发送消息
        Debug.Log("<color=#7FFF00><size=12>" + $"服务器发送进入新场景Cmd" + "</size></color>");

        // 场景加载完毕后 再执行剩下的消息

        //客户端加载慢   

        //缓存剩下消息   //加载完成后 在分发消息



        Debug.Log("<color=#7FFF00><size=12>" + $"服务端发送了 roleCmd" + "</size></color>");
        Server.Instance.SendCmd(roleCmd);


        //5.生成附近的配角 (暂时不考虑)


        //6.生成附近的NPC
        CreateSomeNpc();
    }


    /// <summary>
    /// 接收到客户端发送的跳转地图请求
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    internal static void OnJumpMap(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(JumpToMapCmd)))
        {
            return;
        }

        JumpToMapCmd selectRoleCmd = cmd as JumpToMapCmd;

        //验证坐标信息


        //跳转地图
        EnterMap(Server.Instance.CurPlayer.CurRole);

    }


    /// <summary>
    /// 创建NPC流程
    /// S->C 发送CreateSomeNpcCmd
    /// </summary>
    private static void CreateSomeNpc()
    {
        var npc1Cmd = new CreateSomeNpcCmd();
        npc1Cmd.ThisID = RoleServer.GetNewThisID();
        npc1Cmd.ModelID = 1;
        npc1Cmd.Name = NpcTable.Instance[npc1Cmd.ModelID].Name;
        npc1Cmd.Pos = new Vector3(0,0,1);



        var npc2Cmd = new CreateSomeNpcCmd();
        npc2Cmd.ThisID = RoleServer.GetNewThisID();
        npc2Cmd.ModelID = 2;
        npc2Cmd.Name = NpcTable.Instance[npc2Cmd.ModelID].Name;
        npc2Cmd.Pos = new Vector3(1, 0, 2);




        var npc3Cmd = new CreateSomeNpcCmd();
        npc3Cmd.ThisID = RoleServer.GetNewThisID();
        npc3Cmd.ModelID = 3;
        npc3Cmd.Name = NpcTable.Instance[npc3Cmd.ModelID].Name;
        npc3Cmd.Pos = new Vector3(2, 0, 0);



        Server.Instance.SendCmd(npc1Cmd);
        Server.Instance.SendCmd(npc2Cmd);
        Server.Instance.SendCmd(npc3Cmd);
    }
}
