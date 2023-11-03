using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    //绑定摇杆移动
    internal void BindingJoystick(Action<Vector2> onJoystickMove, Action onJoystickMoveEnd)
    {
        _joystick.OnMoveDir = onJoystickMove;
        _joystick.OnMoveEnd = onJoystickMoveEnd;
    }


    //绑定点击UI 屏幕移动
    internal void BindingTouchScene(Action<RaycastHit> onTouchScene)
    {
        _touchScene.OnHitStmCallback = onTouchScene;
    }
}