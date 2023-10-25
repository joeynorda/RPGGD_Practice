using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TableDatabase
{
    public int ID;
}

//表格 基类 
//T ：数据类型
public class ConfigTable<TDataBase,T>:Singleton<T> 
    where T : new ()
    where TDataBase : TableDatabase,new()
{


    List<FieldInfo> fieldInfos = new List<FieldInfo>();


    //数据存储方式一样
    Dictionary<int, TDataBase> _cache = new Dictionary<int, TDataBase>();


    /// <summary>
    /// 读取表文件 以二进制方式读取
    /// </summary>
    /// <param name="configPath"></param>
    protected void Load(string configPath)
    {
        //读表内数据
        var table = Resources.Load<TextAsset>(configPath);      //二进制格式 读取 
        var tableStream = new MemoryStream(table.bytes);
        using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("gb2312")))
        {
            //跳过第一行 读取的是字段名称
            var fieldStr = reader.ReadLine();
            Debug.Log("<color=#7FFF00><size=12>" + $"{fieldStr}" + "</size></color>");

            var fieldArray = fieldStr.Split(',');
            for (int i = 0; i < fieldArray.Length; i++)
            {
                // fieldArray[i]
                FieldInfo fieldInfo = typeof(TDataBase).GetField(fieldArray[i]);
                if (fieldInfo != null)
                {
                    fieldInfos.Add(fieldInfo);
                }
            }



            //通过 反射获取字段信息 

            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {
                //读取到内存
                var itemArray = lineStr.Split(',');
                TDataBase roleData = new TDataBase();


                //两种方式
                //定义方法 子类 复写实现


                //反射 映射字段 设置值


                
                //循环
                lineStr = reader.ReadLine();
            }
        }
        tableStream.Close();
    }



    //索引方式一样
    public TDataBase this[int index]
    {
        get 
        {
            TDataBase data;
            _cache.TryGetValue(index, out data);
            return data;
        }
    }


    public Dictionary<int, TDataBase> GetAll() => _cache;
    

    //表的读取方式一样



    //不同： 数据类型不同


    //不同： 配置文件路径不同


}

