using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IClient
{
    void SendCmd(Cmd cmd);
    void Recive(Cmd cmd);
}


public interface IServer
{

    //���ӷ�����
    void Connect(IClient client);

    void SendCmd(Cmd cmd);
    void Recive(Cmd cmd);
}



//�ͷ��������� 
//1.�ͻ���ֱ�ӷ��ʷ���������
//2.�߼��ϵķ�����  �ͷ������Ľ��� ģ��
//�ͻ��˷��ʷ�����
public class Net : Singleton<Net>, IClient
{

    IServer _server;//��ǰ������

    //��Ϣ���͡���Ϣ��������
    Dictionary<Type, Action<Cmd>> _parser = new Dictionary<Type, Action<Cmd>>();



    public Net()
    {
        //�ͻ���ע����Ϣ����

        //�ͻ��˽��ս�ɫ�б���Ϣ 
        _parser.Add(typeof(RoleListCmd), UserData.OnRoleList);
    }


    /// <summary>
    /// ���ӷ�����
    /// </summary>
    /// <param name="successCallback">���ӳɹ�</param>
    /// <param name="failedCallback">����ʧ��</param>
    public void ConnectServer(Action successCallback,Action failedCallback)
    {
        //������ _server ��ֵ
        _server = Server.Instance;
        _server.Connect(this);

        //�ж����ӷ������Ƿ�ɹ�

        if (true)
        {
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
        //_server.Send();
        Debug.Log("<color=#7FFF00><size=12>" + $"�ͻ��˽��յ���Ϣ��{cmd.GetType()}" + "</size></color>");


        //�ͻ�����Ϣ�ַ�
        if (_parser.TryGetValue(cmd.GetType(), out Action<Cmd> func))
        {
            func.Invoke(cmd);
        }

    }



    //�ͻ��˷�����Ϣ �൱�� ������������Ϣ
    public void SendCmd(Cmd cmd)
    {
        _server.Recive(cmd);
    }
}

