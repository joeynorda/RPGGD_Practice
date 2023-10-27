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
        Load(roleConfigPath);
    }
}

