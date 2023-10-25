using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


//角色 表内 数据结构
public class RoleDataBase : TableDatabase
{
    public string Name;
    public string ModelPath;

    public override string ToString() => this.ID + " | " + this.Name + " | " + this.ModelPath;
}

/// <summary>
/// 角色表
/// </summary>
public class RoleTable:ConfigTable<RoleDataBase, RoleTable>
{
    string roleConfigPath = "Config/RoleTable.csv";

    public RoleTable()
    {
        #region Before
        ////读表内数据
        //var table = Resources.Load<TextAsset>(roleConfigPath);      //二进制格式 读取 
        //var tableStream = new MemoryStream(table.bytes);
        //using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("gb2312")))
        //{
        //    //跳过第一行
        //    reader.ReadLine();

        //    var lineStr = reader.ReadLine();
        //    while (lineStr != null)
        //    {
        //        //读取到内存
        //        var itemArray = lineStr.Split(',');
        //        RoleDataBase roleData = new RoleDataBase();
        //        if (int.TryParse(itemArray[0], out int id))
        //        {
        //            roleData.ID = id;
        //            roleData.Name = itemArray[1];
        //            roleData.ModelPath = itemArray[2];
        //            _cache.Add(id, roleData);
        //        }
        //        else
        //        {
        //            Debug.Log("<color=#7FFF00><size=12>" + $"RoleTable 转ID失败->{itemArray[0]}" + "</size></color>");
        //        }
        //        //循环
        //        lineStr = reader.ReadLine();
        //    }
        //}
        //tableStream.Close(); 
        #endregion

        Load(roleConfigPath);
    }


}

