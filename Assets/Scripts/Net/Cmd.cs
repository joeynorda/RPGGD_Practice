using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    public List<SelectRoleInfo> AllRole = new List<SelectRoleInfo>();
}



//服务器客户端通信媒介
//角色结构
public class SelectRoleInfo
{
    public string Name;     //角色名
    public int ModelID;  //模型ID
}
