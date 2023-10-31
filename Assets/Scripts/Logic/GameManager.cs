using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:Singleton<GameManager>
{
    GameObject _engineRoot;

    public void Init()
    {

        //初始化自己的引擎
        if (_engineRoot == null)
        {
            _engineRoot = new GameObject("GameEngine");
            GameObject.DontDestroyOnLoad(_engineRoot);
            _engineRoot.AddComponent<GameEngine>();
        }


        //UIManager
        UIMgr.Instance.Init();


        //协程初始化
        QuickCoroutine.Instance.Init();


        //启动引擎 逻辑


        //登录
        SceneManager.LoadScene("LogIn");
        UIMgr.Instance.Replace("UI/Login/Login",UILayer.Normal);
    }
}
