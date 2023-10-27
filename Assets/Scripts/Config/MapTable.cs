using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MapData : TableDatabase
{
    public string Name; //小地图显示

    public string ScenePath; //场景路径
}



public class MapTable:ConfigTable<MapData,MapTable>
{
    public MapTable()
    {
        Load("Config/MapTable.csv");
    }
}

