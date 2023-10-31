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
    public static void OnEnterMap(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(EnterMapCmd)))
        {
            return;
        }
        EnterMapCmd enterMapCmd = cmd as EnterMapCmd;


        Instance.LoadScene(enterMapCmd.MapID);
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

        Debug.Log("<color=#7FFF00><size=12>" + $"场景加载完毕!" + "</size></color>");


        //放开消息 进行分发
        Net.Instance.Pause = false;

    }
}

