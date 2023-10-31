using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using LitJson;

public class DB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var con = new SqliteConnection();
        con.Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class SQLiteMgr : Singleton<SQLiteMgr>, IDataBase
{

    private SqliteConnection conn;
    private SqliteCommand cmd; //指令集 操作对象 执行sql

    const string PlayerTableName = "PlayerData";


   

    public void Init()
    {

        Debug.Log("<color=#7FFF00><size=12>" + $"db init ..." + "</size></color>");
        conn = new SqliteConnection("Data Source=./ServerDB.db");
        conn.Open();
        cmd = conn.CreateCommand();


        //创建表  id,data  存放Player序列化之后的文本  NoSql
        InitTables();
    }


    public Player GerUserData(int id)
    {
        var dataTable = ExecuteSelectTable(string.Format(QueryDefine.SELECT_PLAYER_DATA, PlayerTableName, id));

        if (dataTable.Rows.Count <= 0) return null;

        //table 数据 转化为一行 Convert.ToString(dataTable.Rows[0][0])
        string json = dataTable.Rows[0][0].ToString();
        return JsonMapper.ToObject<Player>(json);
    }


    //保存数据到数据库
    public void SavePlayerData(Player playerData)
    {
        ExecuteNoQuery(string.Format(QueryDefine.INSERT_WITH_UPDATE, PlayerTableName, playerData.ThisID, JsonMapper.ToJson(playerData)));
    }


   


    public void Close()
    {
        conn.Close();
    }


    //初始化表
    private void InitTables()
    {
        ExecuteNoQuery(string.Format(QueryDefine.CREATE_TABLE_BEGIN,PlayerTableName)+QueryDefine.CREATE_TABLE_END);
    }



    //执行Sql
    private void ExecuteNoQuery(string cmdStr, SqliteParameter[] parameters = null)
    {
        cmd.CommandText = cmdStr;
        if (parameters != null)
        {
            cmd.Parameters.AddRange(parameters);
        }
        cmd.ExecuteNonQuery();//非查询性语句
        cmd.Parameters.Clear();
    }


    private DataTable ExecuteSelectTable(string sql, SqliteParameter[] parameters = null)
    {
        cmd.CommandText = sql;
        if (parameters != null)
        {
            cmd.Parameters.AddRange(parameters);
        }
        SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);
        return data;
    }

   
}










/// <summary>
/// 通用数据库查询模板
/// </summary>
public static class QueryDefine
{
    public const string CREATE_TABLE_BEGIN = "CREATE TABLE IF NOT EXISTS {0} (id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Data TEXT";

    public const string CREATE_TABLE_END = ")";

    public const string INSERT_WITH_UPDATE = "INSERT OR REPLACE INTO {0} VALUES({1},'{2}')";

    public const string SELECT_PLAYER_DATA = "select Data from {0} where id = {1} ";

}






public interface IDataBase
{
    void Init();

    Player GerUserData(int id);


    void SavePlayerData(Player playerData);

    void Close();
}


