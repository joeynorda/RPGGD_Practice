using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    FunctionHall _functionHall;


    //��ʼ�� ��һ�μ��� ����
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

        if (_functionHall == null)
        {
            _functionHall = new FunctionHall();
        }
    }


    //��ҡ���ƶ�
    internal void BindingJoystick(Action<Vector2> onJoystickMove, Action onJoystickMoveEnd)
    {
        if (_joystick == null) return;

        _joystick.OnMoveDir = onJoystickMove;
        _joystick.OnMoveEnd = onJoystickMoveEnd;
    }


    //�ͷ�ҡ���ƶ�
    public void ReleaseJoyStick()
    {
        if (_joystick == null) return;

        _joystick.OnMoveDir = null;
        _joystick.OnMoveEnd = null;
    }



    //�󶨵��UI ��Ļ�ƶ�
    internal void BindingTouchScene(Action<RaycastHit> onTouchScene)
    {
        if (_touchScene == null) return;

        _touchScene.OnHitStmCallback = onTouchScene;
    }


    //�ͷ���Ļ����
    public void ReleaseTouchScene()
    {
        if (_touchScene == null) return;

        _touchScene.OnHitStmCallback = null;
    }



    /// <summary>
    ///  �ͷ�ҡ��  �ͷŰ��¼�
    ///  �ͷ�TouchScene
    /// </summary>
    internal void Reset()
    {
        ReleaseJoyStick();
        ReleaseTouchScene();


        //�¼�ϵͳ�ر�
        UIMgr.Instance.UIEventSystemEnabled = false;

        ResetJoyStick();
    }


    //ҡ������
    private void ResetJoyStick()
    {
        //ҡ�˹�λ
        if (_joystick != null)
        {
            _joystick.Reset();
        }
    }


}