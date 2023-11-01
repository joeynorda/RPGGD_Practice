using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IClient
{
    void SendCmd(Cmd cmd);
    void Recive(Cmd cmd);
}





//���ؿͻ���
//�ͷ��������� 
//1.�ͻ���ֱ�ӷ��ʷ���������
//2.�߼��ϵķ�����  �ͷ������Ľ��� ģ��
//�ͻ��˷��ʷ�����
public class Net : Singleton<Net>, IClient
{

    IServer _server;//��ǰ������

    //��Ϣ���͡���Ϣ��������
    Dictionary<Type, Action<Cmd>> _parser = new Dictionary<Type, Action<Cmd>>();


    //��Ϣ����
    List<Cmd> _cache = new List<Cmd>();


    //��ǰ�Ƿ�������״̬
    private bool _pause;

    //��Ϣ�Ƿ����� ������
    public bool Pause
    {
        get { return _pause; }
        set 
        {
            _pause = value;
            if (!_pause)
            {
                Recive(null);
            }
        }
    }

    public Net()
    {
        //�ͻ���ע����Ϣ����

        //�ͻ��˽��ս�ɫ�б���Ϣ 
        _parser.Add(typeof(RoleListCmd), UserData.OnRoleList);


        //�����ͼ
        _parser.Add(typeof(EnterMapCmd), SceneMgr.OnEnterMap);


        //��ǰ����id
        _parser.Add(typeof(MainRoleThisIDCmd), RoleMgr.OnMainRoleThisid);


        //������ɫ
        _parser.Add(typeof(CreateSceneRoleCmd), RoleMgr.OnCreateSceneRole);


    }


    /// <summary>
    /// ���ӷ�����
    /// </summary>
    /// <param name="successCallback">���ӳɹ�</param>
    /// <param name="failedCallback">����ʧ��</param>
    public void ConnectServer(Action successCallback, Action failedCallback)
    {
        //������ _server ��ֵ
        _server = Server.Instance;
        _server.Connect(this);

        //�ж����ӷ������Ƿ�ɹ�

        if (true)
        {
            Debug.Log("<color=#7FFF00><size=12>" + $"���ӷ������ɹ�!" + "</size></color>");
            successCallback?.Invoke();
        }
        else
        {
            failedCallback?.Invoke();
        }
    }





    //�ͻ��˽�����Ϣ �൱�� ������������Ϣ
    public void Recive(Cmd cmd)
    {

        if (cmd != null)
        {
            _cache.Add(cmd);
        }


        //����״̬ �򲻽���
        if (Pause)
        {
            return;
        }


        //��Ϣ����
        //list ��Ϣ˳��һ��
        foreach (var cacheCmd in _cache)
        {
            //�ͻ�����Ϣ�ַ�
            if (_parser.TryGetValue(cacheCmd.GetType(), out Action<Cmd> func))
            {
                func.Invoke(cacheCmd);
            }
        }
        _cache.Clear();

    }



    //�ͻ��˷�����Ϣ �൱�� ������������Ϣ
    public void SendCmd(Cmd cmd)
    {
        _server.Recive(cmd);
    }



    /// <summary>
    /// �����ϢCmd �Ƿ���Ŀ������
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static bool CheckCmd(Cmd cmd,Type targetType)
    {
        if (cmd.GetType() != targetType)
        {
            Debug.LogError(string.Format("��Ҫ{0},�����յ���{1}", targetType, cmd.GetType()));
            return false;
        }
        return true;
    }
}

