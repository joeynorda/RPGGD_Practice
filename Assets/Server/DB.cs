using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

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
    private SqliteCommand cmd;

    const string PlayerTableName = "PlayerData";


    public void Close()
    {
    }

    public void Init()
    {
    }
}




public interface IDataBase
{
    void Init();

    void Close();
}


