using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:Singleton<GameManager>
{
    public void Init()
    {

        //启动引擎 逻辑


        //登录
        SceneManager.LoadScene("LogIn");
    }
}
