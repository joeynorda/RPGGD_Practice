using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserData : Singleton<UserData>
{

    public List<RoleServer> AllRole = new List<RoleServer>();



    internal static void OnRoleList(Cmd cmd)
    {

        Debug.Log("<color=#7FFF00><size=12>" + $"�ͻ��˽��յ����������͵Ľ�ɫ�б���Ϣ" + "</size></color>");


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


            //UIMgr.Instance.Remove();

            //�滻֮ǰUI
            UIMgr.Instance.Replace("UI/SelectRole/SelectRole", UILayer.Normal);
        }
        else
        {
            //��ɫ��������
        }

    }
}