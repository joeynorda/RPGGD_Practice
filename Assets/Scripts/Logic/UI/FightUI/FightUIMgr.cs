using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUIMgr : Singleton<FightUIMgr>
{
    //ҡ��
    Joystick _joystick;
    //���ܰ�ť
    //����ͷ��
    //С��ͼ
    //Ŀ��ͷ��
    //���ܴ���

    public void Init()
    {

        Debug.Log("<color=#7FFF00><size=12>" + $"FightUIMgr  init ..........." + "</size></color>");
        if (_joystick == null)
        {
            _joystick = new Joystick();
        }
    }
}