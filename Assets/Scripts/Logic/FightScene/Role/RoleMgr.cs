using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;


/// <summary>
/// Description:客户端角色管理器 
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:
/// Modify:
/// </summary>
public class RoleMgr : Singleton<RoleMgr>
{

    int _mainRoleThisID;

    //thisID,role
    public Dictionary<int, Role> AllRole = new Dictionary<int, Role>();



    //记录当前角色ID
    internal static void OnMainRoleThisid(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(MainRoleThisIDCmd)))
        {
            return;   
        }
        MainRoleThisIDCmd mainRoleThisIDCmd = cmd as MainRoleThisIDCmd;

        //记录当前主角thisid
        RoleMgr.Instance._mainRoleThisID = mainRoleThisIDCmd.ThisID;

    }


    //创建场景角色
    internal static void OnCreateSceneRole(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(CreateSceneRoleCmd)))
        {
            return;
        }
        CreateSceneRoleCmd createRole = cmd as CreateSceneRoleCmd;

        RoleDataBase roleDataBase = RoleTable.Instance[createRole.ModelID];
        if (roleDataBase == null)
        {
            Debug.LogError("未找到角色模型："+createRole.ModelID);
            return;
        }

        //添加角色
        //创建角色模型   //创建Role脚本  挂载在模型上  
        var roleObj = ResMgr.Instance.GetInstance(roleDataBase.ModelPath);

        Debug.Log("<color=#7FFF00><size=12>" + $"{roleObj.name}-----------------------" + "</size></color>");

        Role role;

        //判断主角
        if (RoleMgr.Instance._mainRoleThisID == createRole.ThisID)
        {
            //是主角
            role = roleObj.AddComponent<MainRole>();
        }
        else
        {
            role = roleObj.AddComponent<Role>();
        }

       
        role.Initialize(createRole, roleDataBase);

        //RoleMgr 管理场景中所有Role
        Instance.AllRole[createRole.ThisID] = role;
        

    }
}
