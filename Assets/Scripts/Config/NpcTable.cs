using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NpcDataBase: TableDatabase
{
    public string Name;
    public string ModelPath;

    public List<int> TestIDList;

    public override string ToString() => this.Name + " | " + this.ID + " | " + this.ModelPath;
}



public class NpcTable : ConfigTable<NpcDataBase,NpcTable>
{
    string configPath = "Config/NpcTable.csv";

    public NpcTable()
    {
        Load(configPath);
    }
   

}

