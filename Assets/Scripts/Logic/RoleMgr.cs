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
        CreateSceneRoleCmd createRol = cmd as CreateSceneRoleCmd;

        Debug.LogError(createRol);
    }
}
