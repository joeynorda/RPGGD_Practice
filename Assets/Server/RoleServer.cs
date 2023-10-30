using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/// <summary>
/// Description:服务器角色
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
public class RoleServer
{

    static int _curThisid = 1;

    

    //全局唯一
    public static int GetNewThisID()
    {
        return _curThisid++;
    }


    public string Name;     //角色名
    public int ModelID;  //模型ID


    //攻防血 
    //角色坐标 朝向
    //所在地图
    //道具信息
}

