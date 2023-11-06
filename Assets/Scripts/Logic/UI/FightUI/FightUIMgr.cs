using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FightUIMgr : Singleton<FightUIMgr>
{
    //摇杆
    Joystick _joystick;

    //触摸  UI 点击交互 寻路
    TouchScene _touchScene;


    //技能按钮
    //人物头像
    //小地图
    //目标头像
    //功能大厅
    FunctionHall _functionHall;


    //初始化 第一次加载 生成
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


    //绑定摇杆移动
    internal void BindingJoystick(Action<Vector2> onJoystickMove, Action onJoystickMoveEnd)
    {
        if (_joystick == null) return;

        _joystick.OnMoveDir = onJoystickMove;
        _joystick.OnMoveEnd = onJoystickMoveEnd;
    }


    //释放摇杆移动
    public void ReleaseJoyStick()
    {
        if (_joystick == null) return;

        _joystick.OnMoveDir = null;
        _joystick.OnMoveEnd = null;
    }



    //绑定点击UI 屏幕移动
    internal void BindingTouchScene(Action<RaycastHit> onTouchScene)
    {
        if (_touchScene == null) return;

        _touchScene.OnHitStmCallback = onTouchScene;
    }


    //释放屏幕触摸
    public void ReleaseTouchScene()
    {
        if (_touchScene == null) return;

        _touchScene.OnHitStmCallback = null;
    }



    /// <summary>
    ///  释放摇杆  释放绑定事件
    ///  释放TouchScene
    /// </summary>
    internal void Reset()
    {
        ReleaseJoyStick();
        ReleaseTouchScene();


        //事件系统关闭
        UIMgr.Instance.UIEventSystemEnabled = false;

        ResetJoyStick();
    }


    //摇杆重置
    private void ResetJoyStick()
    {
        //摇杆归位
        if (_joystick != null)
        {
            _joystick.Reset();
        }
    }


}