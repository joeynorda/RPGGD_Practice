using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Description:角色
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
public class Role : MonoBehaviour
{

    protected NavMeshAgent _agent;

    Animator _animator;

    private const string MOTIONTYPE = "MotionType";

    Vector3? targetPos=null;


    //引用  只读不写 不要修改
    CreateSceneRoleCmd _serverData; //服务器传递的角色index 动态数据
    RoleDataBase _tableData; //表里的静态数据




    public int ThisId { get => _serverData.ThisID; }


    //模型路径
    public string ModelPath { get => _tableData.ModelPath; }


    public virtual void Initialize(CreateSceneRoleCmd createRole, RoleDataBase roleDataBase)
    {
        this._serverData = createRole;
        this._tableData = roleDataBase;

        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.stoppingDistance = GameSetting.STOP_DISTANCE;
        _agent.speed = 5f;
        _agent.angularSpeed = float.MaxValue;       //角速度
        _agent.acceleration = float.MaxValue;

        _animator = gameObject.GetComponent<Animator>();
       
    }



    public void SetTargetPostion(Transform targetTransform)
    {
        
    }


    //移动至某一个点
    public void PathTo(Vector3 target)
    {
        _agent.SetDestination(target);

        targetPos = target;

        SetAnimation(1);
    }


    //停止移动
    public void StopMove()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
        SetAnimation(0);
        targetPos = null;
    }




    private void Update()
    {

        if (targetPos == null) return;


        if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPos.Value.x, 0, targetPos.Value.z)) < GameSetting.STOP_DISTANCE)
        {
            OnArrived();
            targetPos = null;
            SetAnimation(0);
        }


    }



    //到达
    private void OnArrived()
    {
        Debug.Log("<color=#7FFF00><size=12>" + $"到达" + "</size></color>");
    }



    //设置动画
    private void SetAnimation(int motionType)
    {
        _animator.SetInteger(MOTIONTYPE, motionType);
    }
}
