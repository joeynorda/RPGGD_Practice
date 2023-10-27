using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServer
{

    //���ӷ����� ������ǰ���ӵĿͻ��˸�ֵ
    void Connect(IClient client);

    void SendCmd(Cmd cmd);

    void Recive(Cmd cmd);
}

/// <summary>
/// ������
/// </summary>
public class Server : Singleton<Server>, IServer
{
    IClient _client;


    //��Ϣ���ͣ���Ϣ��������
    Dictionary<Type, Action<Cmd>> _parser = new Dictionary<Type, Action<Cmd>>();


    //��ǰ��¼����� ��ʱֻ����һ�����
    public Player CurPlayer;


    public Server()
    {

        //��Ϣ�����ֵ�

        //��¼��Ϣ
        _parser.Add(typeof(LoginCmd), CmdParser.OnLogin);

        //��ɫѡ����Ϣ
        _parser.Add(typeof(SelectRoleCmd), CmdParser.OnSelectRole);
    }



    //�ͻ������Ӻ����  1�Զ�
    public void Connect(IClient client)
    {
        this._client = client;
    }



    //�������˽��տͻ�����Ϣ 
    public void Recive(Cmd cmd)
    {
        //_client.send

        Debug.Log("<color=#7FFF00><size=12>" + $"���������յ���{cmd.GetType()}" + "</size></color>");

        //��Ϣ�ַ�
        if (_parser.TryGetValue(cmd.GetType(), out Action<Cmd> func))
        {
            func?.Invoke(cmd);
        }
    }

    public void SendCmd(Cmd cmd)
    {
        _client.Recive(cmd);
    }
}
