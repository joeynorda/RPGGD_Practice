using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 消息基类
/// </summary>
public class Cmd
{
        
}


//C -> S
/// <summary>
/// 登录消息
/// </summary>
public class LoginCmd : Cmd
{
    public string Account;
    public string Password;
}




//S->C
//玩家的角色列表信息
public class RoleListCmd : Cmd
{
    //保存玩家所有角色 
    public List<RoleServer> AllRole = new List<RoleServer>();
}






//C->S
//客户端发送选择角色 进入游戏
public class SelectRoleCmd : Cmd
{
    //角色索引
    public int Index;
}




//S->C 进入地图消息
public class EnterMapCmd : Cmd 
{
    public int MapID;
}




//S->C 服务器发送给客户端主角ID
public class MainRoleThisIDCmd : Cmd
{
    public int ThisID;
}







//服务器客户端通信媒介
//角色结构
public class SelectRoleInfo
{
    public string Name;     //角色名
    public int ModelID;  //模型ID
}







//S->C
//角色进场景
public class CreateSceneRoleCmd:Cmd
{

    //角色唯一标识
    //用于服务器和客户端角色相关交互
    public int ThisID;

    public string Name;

    public int ModelID;


    public Vector3 Pos;
    public Vector3 FaceTo;
}






//S->C
//创建NPC
public class CreateSomeNpcCmd : Cmd
{
    public int ThisID;

    public string Name;

    public int ModelID;

    public Vector3 Pos;
    public Vector3 FaceTo;
}



//C->S
//客户端向服务端发送跳转场景请求
public class JumpToMapCmd:Cmd
{
    //目标地图
    public int ID;
}