using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//树形结构  玩家数据转为json 直接 存放
/// <summary>
/// 表示玩家 
/// </summary>
public class Player
{
    public int ThisID;

    //持有玩家 所拥有的角色列表 假的角色数据
    public List<RoleServer> AllRole = new List<RoleServer>();


    public Player()
    {
        //AllRole.Add(new SelectRoleInfo() { Name = "Joe1", ModelID =  1 });
        //AllRole.Add(new SelectRoleInfo() { Name = "Loda2", ModelID = 2 });
        //AllRole.Add(new SelectRoleInfo() { Name = "Mipo3", ModelID = 3 });

        //AllRole.Add(new RoleServer() { Name = "Joe1", ModelID = 1 });
        //AllRole.Add(new RoleServer() { Name = "Loda2", ModelID = 2 });
        //AllRole.Add(new RoleServer() { Name = "Mipo3", ModelID = 3 });
    }
}
