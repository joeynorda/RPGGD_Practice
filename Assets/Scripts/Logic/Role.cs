using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;




/// <summary>
/// Description:角色
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
public class Role : MonoBehaviour
{

    //引用  只读不写 不要修改
    CreateSceneRoleCmd _serverData; //服务器传递的角色index
    RoleDataBase _tableData;

    public void Initialize(CreateSceneRoleCmd createRole, RoleDataBase roleDataBase)
    {
        this._serverData = createRole;
        this._tableData = roleDataBase;
    }



}
