using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 通用规则
/// </summary>
public static class GameSetting
{

    //移动精度 停止最近距离
    public const float STOP_DISTANCE = 0.1f;


    //Main Role Layer
    public static int MainRoleLayer;

    static GameSetting()
    {
        MainRoleLayer = LayerMask.NameToLayer("MainRole");
    }
}
