using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUIMgr : Singleton<FightUIMgr>
{
    //ҡ��
    Joystick _joystick;

    //����  UI ������� Ѱ·
    TouchScene _touchScene;


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


        if (_touchScene == null)
        {
            _touchScene = new TouchScene();
        }
    }


    //��ҡ���ƶ�
    internal void BindingJoystick(Action<Vector2> onJoystickMove, Action onJoystickMoveEnd)
    {
        _joystick.OnMoveDir = onJoystickMove;
        _joystick.OnMoveEnd = onJoystickMoveEnd;
    }


    //�󶨵��UI ��Ļ�ƶ�
    internal void BindingTouchScene(Action<RaycastHit> onTouchScene)
    {
        _touchScene.OnHitStmCallback = onTouchScene;
    }
}