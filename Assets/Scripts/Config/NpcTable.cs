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

    public override string ToString() => this.Name + " | " + this.ID + " | " + this.ModelPath;
}



public class NpcTable : ConfigTable<NpcDataBase,NpcTable>
{
    Dictionary<int, NpcDataBase> _cache = new Dictionary<int, NpcDataBase>();
    string configPath = "Config/NpcTable.csv";

    public NpcTable()
    {
        #region Before

        //var configFile = Resources.Load<TextAsset>(configPath);
        //var memoryStream = new MemoryStream(configFile.bytes);
        //using (var reader = new StreamReader(memoryStream, Encoding.GetEncoding("gb2312")))
        //{

        //    var firstLine = reader.ReadLine();
        //    //Debug.Log("<color=#7FFF00><size=12>" + $"{firstLine}" + "</size></color>");

        //    var line = reader.ReadLine();
        //    while (line != null)
        //    {
        //        //Debug.Log("<color=#EE2C2C><size=12>" + $"{line}" + "</size></color>");

        //        var itemArray = line.Split(',');

        //        if (int.TryParse(itemArray[0], out int id))
        //        {
        //            NpcDataBase npcDataBase = new NpcDataBase();
        //            npcDataBase.ID = id;
        //            npcDataBase.Name = itemArray[1];
        //            npcDataBase.ModelPath = itemArray[2];
        //            _cache[id] = npcDataBase;
        //        }
        //        else
        //        {
        //            Debug.Log("<color=#7FFF00><size=12>" + $"NpcTable转换id失败->{itemArray[0]}" + "</size></color>");
        //        }


        //        line = reader.ReadLine();
        //    }
        //} 
        #endregion

        Load(configPath);
       
        for (int i = 0; i < GetAll().Values.Count; i++)
        {

        }

    }
   

}

