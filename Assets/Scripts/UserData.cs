using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserData : Singleton<UserData>
{

    public List<SelectRoleInfo> AllRole = new List<SelectRoleInfo>();



    internal static void OnRoleList(Cmd cmd)
    {
        //cmd -> LoginCmd
        RoleListCmd roleListCmd = cmd as RoleListCmd;
        if (roleListCmd == null)
        {
            Debug.LogError(string.Format("需要{0},但是收到了{1}", typeof(RoleListCmd), cmd.GetType()));
            return;
        }


        UserData.Instance.AllRole = roleListCmd.AllRole;


        //之前已经创建过界面
        if (roleListCmd.AllRole.Count > 0)
        {
            //选人界面
            SceneManager.LoadScene("SelectRole");
        }
        else
        {
            //角色创建界面
        }

    }
}