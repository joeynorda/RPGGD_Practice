using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserData : Singleton<UserData>
{

    public List<RoleServer> AllRole = new List<RoleServer>();



    internal static void OnRoleList(Cmd cmd)
    {

        Debug.Log("<color=#7FFF00><size=12>" + $"客户端接收到服务器发送的角色列表信息" + "</size></color>");


        //cmd -> LoginCmd
        if (!Net.CheckCmd(cmd, typeof(RoleListCmd)))
        {
            return;
        }

        RoleListCmd roleListCmd = cmd as RoleListCmd;

        UserData.Instance.AllRole = roleListCmd.AllRole;

        //之前已经创建过界面
        if (roleListCmd.AllRole.Count > 0)
        {
            //选人界面
            SceneManager.LoadScene("SelectRole");


            //UIMgr.Instance.Remove();

            //替换之前UI
            UIMgr.Instance.Replace("UI/SelectRole/SelectRole", UILayer.Normal);
        }
        else
        {
            //角色创建界面
        }

    }
}