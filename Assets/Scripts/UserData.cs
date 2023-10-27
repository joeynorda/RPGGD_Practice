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
        if (!Net.CheckCmd(cmd, typeof(RoleListCmd)))
        {
            return;
        }

        RoleListCmd roleListCmd = cmd as RoleListCmd;

        UserData.Instance.AllRole = roleListCmd.AllRole;

        //֮ǰ�Ѿ�����������
        if (roleListCmd.AllRole.Count > 0)
        {
            //ѡ�˽���
            SceneManager.LoadScene("SelectRole");
        }
        else
        {
            //��ɫ��������
        }

    }
}