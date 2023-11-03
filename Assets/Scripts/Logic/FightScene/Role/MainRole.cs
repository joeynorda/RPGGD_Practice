using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



/// <summary>
/// Description:主角类
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
public class MainRole : Role
{
   

    //向战斗Manager  注册
    public override void Initialize(CreateSceneRoleCmd createRole, RoleDataBase roleDataBase)
    {
        base.Initialize(createRole, roleDataBase);

        //UI 创建要比主角 早

        gameObject.layer = LayerMask.NameToLayer("MainRole");


        BindingControlEvent();

    }

    private void BindingControlEvent()
    {
        //绑定摇杆控制事件
        FightUIMgr.Instance.BindingJoystick(OnJoystickMove, OnJoystickMoveEnd);



        //绑定点击屏幕移动事件
        FightUIMgr.Instance.BindingTouchScene(OnTouchScene);
    }

    //移动结束
    private void OnJoystickMoveEnd()
    {
        Debug.Log("<color=#7FFF00><size=12>" + $"移动结束" + "</size></color>");
        StopMove();
    }


    //移动
    void OnJoystickMove(Vector2 dir)
    {
        Debug.Log("<color=#7FFF00><size=12>" + $"{dir}" + "</size></color>");

        //计算目标点

        //相机正方向 相机朝向Z

        //速度与目标点的校验值  ： 与速度正相关  ，与方向更新频率负相关
        float verifySpeed = 1;

        //主角当前位置 + 位置 目标点远近
        var target = this.transform.position + new Vector3(dir.x, 0, dir.y)* verifySpeed;

        PathTo(target);
    }



    //点击移动 
    void OnTouchScene(RaycastHit hit)
    {
        PathTo(hit.point);
    }


    //场景跳转请求
    public void OnJumpTo(int jumpToMapID)
    {
        //发送跳转场景请求
        Net.Instance.SendCmd(new JumpToMapCmd() { ID = jumpToMapID });
    }
}