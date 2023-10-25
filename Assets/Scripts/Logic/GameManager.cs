using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class GameManager:Singleton<GameManager>
{
    public void Init()
    {

        //启动引擎 逻辑


        //登录

        SceneManager.LoadScene("LogIn");

        //测试
        //foreach (var item in NpcTable.Instance.AllNpcData.Values)
        //{
        //    UnityEngine.Debug.Log("<color=#7FFF00><size=12>" + $"{item.ID}" + "</size></color>");
        //    UnityEngine.Debug.Log("<color=#7FFF00><size=12>" + $"{item.Name}" + "</size></color>");
        //    UnityEngine.Debug.Log("<color=#7FFF00><size=12>" + $"{item.ModelPath}" + "</size></color>");
        //} 


    }
}
