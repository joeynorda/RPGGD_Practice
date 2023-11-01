using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUIMgr : Singleton<FightUIMgr>
{
    //摇杆
    Joystick _joystick;
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
    }
}