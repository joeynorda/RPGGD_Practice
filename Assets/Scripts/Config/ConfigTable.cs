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

/// <summary>
/// 开发期和打包分开
/// </summary>
/// <typeparam name="TDataBase"></typeparam>
/// <typeparam name="T"></typeparam>


//表格 基类 
//T ：数据类型
public class ConfigTable<TDataBase, T> : Singleton<T>
    where T : new()
    where TDataBase : TableDatabase, new()
{

    //字段信息
    List<FieldInfo> fieldInfos = new List<FieldInfo>();


    //数据存储方式一样
    Dictionary<int, TDataBase> _cache = new Dictionary<int, TDataBase>();


    /// <summary>
    /// 读取表文件 以二进制方式读取 
    /// //表的读取方式一样
    /// </summary>
    /// <param name="tablePath"></param>  不同： 配置文件路径不同
    protected void Load(string tablePath) 
    {
        MemoryStream tableStream;

        //预定义
        //开发期 读 Project/Config

#if UNITY_EDITOR
        var tableBytes = File.ReadAllBytes(Application.dataPath + "/../" + tablePath);
        tableStream = new MemoryStream(tableBytes);
#else
        //发布之后 读 Resources/Config
        //读表内数据
        var table = Resources.Load<TextAsset>(configPath);      //二进制格式 读取 
        tableStream = new MemoryStream(table.bytes);
#endif



        using (var reader = new StreamReader(tableStream, Encoding.GetEncoding("gb2312")))
        {
            //跳过第一行 读取的是字段名称
            var fieldNameStr = reader.ReadLine();

            var fieldNameArray = fieldNameStr.Split(',');

            for (int i = 0; i < fieldNameArray.Length; i++)
            {
                // fieldArray[i]
                FieldInfo fieldInfo = typeof(TDataBase).GetField(fieldNameArray[i]);
                if (fieldInfo != null)
                {
                    fieldInfos.Add(fieldInfo);
                }
            }



            //通过 反射获取字段信息 

            var lineStr = reader.ReadLine();
            while (lineStr != null)
            {
                ////读取到内存
                //var itemArray = lineStr.Split(',');
                //TDataBase data = new TDataBase();

                //if (itemArray.Length != fieldInfos.Count)
                //{
                //    Debug.Log("<color=#EE2C2C><size=12>" + $"{lineStr} 反射错误" + "</size></color>");
                //    continue;
                //}


                ////两种方式
                ////①定义方法 子类 复写实现
                ////ParseItem(data, itemArray);

                //data.ID = int.Parse(itemArray[0]);


                ////②反射 映射字段 设置值
                ////对每个字段进行解析
                //for (int i = 0; i < fieldInfos.Count; i++)
                //{
                //    if (fieldInfos[i].FieldType == typeof(int))
                //    {
                //        fieldInfos[i].SetValue(data, int.Parse(itemArray[i]));
                //    }
                //    else if (fieldInfos[i].FieldType == typeof(string))
                //    {
                //        fieldInfos[i].SetValue(data, itemArray[i]);
                //    }
                //}


                TDataBase data = ReadLine(fieldInfos, lineStr);
                if (data != null)
                {
                    _cache.Add(data.ID, data);
                }
                
                
                //循环
                lineStr = reader.ReadLine();
            }
        }
        tableStream.Close();
    }


    //①定义方法 子类 复写实现
    //protected virtual void ParseItem(TDataBase data, string[] itemArray)
    //{

    //}


    /// <summary>
    /// 反射解析
    /// </summary>
    /// <param name="allFieldInfo"></param>
    /// <param name="lineStr"></param>
    /// <returns></returns>
    private static TDataBase ReadLine(List<FieldInfo> allFieldInfo, string lineStr)
    {
        if (string.IsNullOrEmpty(lineStr))
        {
            return default(TDataBase);
        }

        var itemArray = lineStr.Split(',');
        if (itemArray.Length != allFieldInfo.Count)
        {
            Debug.Log("<color=#7FFF00><size=12>" + $"Error! 长度不匹配：{lineStr}" + "</size></color>");
            return default(TDataBase);
        }

        TDataBase dataBase = new TDataBase();

        for (int i = 0; i < allFieldInfo.Count; i++)
        {

            //int float string bool Array(List)
            if (allFieldInfo[i].FieldType == typeof(int))
            {
                allFieldInfo[i].SetValue(dataBase, int.Parse(itemArray[i]));
            }
            else if (allFieldInfo[i].FieldType == typeof(string))
            {
                allFieldInfo[i].SetValue(dataBase, itemArray[i]);
            }
            else if (allFieldInfo[i].FieldType == typeof(float))
            {
                allFieldInfo[i].SetValue(dataBase, float.Parse(itemArray[i]));
            }
            else if (allFieldInfo[i].FieldType == typeof(bool))
            {
                var v = int.Parse(itemArray[i]);
                if (v != 0)
                {
                    //非零即真
                }
                allFieldInfo[i].SetValue(dataBase, bool.Parse(itemArray[i]));
            } 


            //List<>  分隔符 $
            else if (allFieldInfo[i].FieldType == typeof(List<int>))
            {
                var list = new List<int>();
                Debug.Log("<color=#7FFF00><size=12>" + $"{itemArray[i]}" + "</size></color>");
                foreach (var item in itemArray[i].Split('$'))
                {
                    list.Add(int.Parse(item));
                }

                allFieldInfo[i].SetValue(dataBase, list);
            }
            else if (allFieldInfo[i].FieldType == typeof(List<float>))
            {
                var list = new List<float>();
                foreach (var item in itemArray[i].Split('$'))
                {
                    list.Add(float.Parse(item));
                }

                allFieldInfo[i].SetValue(dataBase, list);
            }
            else if (allFieldInfo[i].FieldType == typeof(List<string>))
            {
                var list = new List<string>(itemArray[i].Split('$')) { };
                allFieldInfo[i].SetValue(dataBase, list);
            }
        }

        return dataBase;
    }












    //索引方式一样   //索引器一致
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
    



}

