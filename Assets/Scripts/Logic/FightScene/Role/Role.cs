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

    //目标点
    public Transform target;

    NavMeshAgent _agent;

    Animator _animator;

    private const string MOTIONTYPE = "MotionType";




    //引用  只读不写 不要修改
    CreateSceneRoleCmd _serverData; //服务器传递的角色index
    RoleDataBase _tableData;



    public void Initialize(CreateSceneRoleCmd createRole, RoleDataBase roleDataBase)
    {
        this._serverData = createRole;
        this._tableData = roleDataBase;

        _agent = gameObject.AddComponent<NavMeshAgent>();
        _agent.stoppingDistance = GameSetting.STOP_DISTANCE;
        _agent.speed = 5f;
        _agent.angularSpeed = float.MaxValue;       //角速度

        _animator = gameObject.GetComponent<Animator>();
       
    }



    public void SetTargetPostion(Transform targetTransform)
    {
        
    }






    private void Update()
    {

        if (target == null) return;

        _agent.SetDestination(new Vector3(target.position.x,0,target.position.z));
        _animator.SetInteger(MOTIONTYPE, 1);

        if (Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z), new Vector3( target.transform.position.x,0,target.transform.position.z)) <= GameSetting.STOP_DISTANCE)
        {
            _animator.SetInteger(MOTIONTYPE, 0);
        }
    }
}
