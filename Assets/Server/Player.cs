using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/// <summary>
/// 表示玩家
/// </summary>
public class Player
{
    public List<SelectRoleInfo> AllRole = new List<SelectRoleInfo>();


    public Player()
    {
        AllRole.Add(new SelectRoleInfo() { Name = "Joe1", ModelID =  0 });
        AllRole.Add(new SelectRoleInfo() { Name = "Loda2", ModelID = 1 });
        AllRole.Add(new SelectRoleInfo() { Name = "Mipo3", ModelID = 2 });
    }
}
