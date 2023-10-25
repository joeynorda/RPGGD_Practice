using System.Collections.Generic;


//角色结构
public class SelectRoleInfo
{
    public string Name;     //角色名
    public string ResPath;  //模型资源路径
}


public  class UserData:Singleton<UserData>
{
    public List<SelectRoleInfo> AllRole = new List<SelectRoleInfo>();


    public UserData()
    {
        AllRole.Add(new SelectRoleInfo() { Name = "Joe1", ResPath = "Prefabs/Character_Hero_Knight_Male 1" });
        AllRole.Add(new SelectRoleInfo() { Name = "Loda2", ResPath = "Prefabs/Character_Knight_02_Orange 1" });
        AllRole.Add(new SelectRoleInfo() { Name = "Mipo3", ResPath = "Prefabs/Character_Hero_Knight_Female 1" });
    }
}