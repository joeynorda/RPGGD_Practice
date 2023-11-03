using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Burst.Intrinsics.X86.Avx;


/// <summary>
/// Description:场景管理器
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
internal class SceneMgr : Singleton<SceneMgr>
{

    public CameraControll MainCameraControl;





    public static void OnEnterMap(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(EnterMapCmd)))
        {
            return;
        }
        EnterMapCmd enterMapCmd = cmd as EnterMapCmd;


        Debug.Log("<color=#7FFF00><size=12>" + $"客户端接收进入场景Cmd 开始进入场景" + "</size></color>");


        //进入场景 Loading 界面 Load Progress..   UI -> TOP

        //删除Normal层UI
        UIMgr.Instance.RemoveLayer();


        SceneMgr.Instance.LoadScene(enterMapCmd.MapID);
    }

    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="mapID"></param>
    private void LoadScene(int mapID)
    {
        var mapData = MapTable.Instance[mapID];
        if (mapData == null)
        {
            Debug.Log("<color=#EE2C2C><size=12>" + $"未找到地图 mapID:{mapID} " + "</size></color>");
            return;
        }


        Debug.Log("<color=#7FFF00><size=12>" + $"客户端阻塞接收的Cmd 不处理" + "</size></color>");

        //阻塞消息  暂时不处理
        Net.Instance.Pause = true;



        var ao = SceneManager.LoadSceneAsync(mapData.ScenePath);


        //开启协程 异步等待
        QuickCoroutine.Instance.StartCoroutine(LoadEnd(ao));

        //SceneManager.LoadScene(mapData.ScenePath);
    }


    //异步判断
    private IEnumerator LoadEnd(AsyncOperation ao)
    {
        //循环等待
        while (!ao.isDone)
        {
            //ao.progress;  显示进度条的一部分
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("<color=#7FFF00><size=12>" + $"场景加载完毕! 场景进入后 处理消息" + "</size></color>");


        
        //加载完毕
        OnLoadEnd();

    }

    private void OnLoadEnd()
    {

        //初始化相机
        InitCamera();


        //UI
        FightUIMgr.Instance.Init();


        //放开消息 进行分发
        Net.Instance.Pause = false;

       
        //销毁loading

    }



    private void InitCamera()
    {
        //初始化摄像机
        var cameraObj = ResMgr.Instance.GetInstance("SceneCamera");
        MainCameraControl = cameraObj.GetComponent<CameraControll>();
    }
}

