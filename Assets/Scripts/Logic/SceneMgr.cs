using System;
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
    internal static void OnEnterMap(Cmd cmd)
    {
        if (!Net.CheckCmd(cmd, typeof(EnterMapCmd)))
        {
            return;
        }
        EnterMapCmd enterMapCmd = cmd as EnterMapCmd;

        //if (MapTable.Instance.GetAll().TryGetValue(enterMapCmd.MapID, out MapData mapData))
        //{
        //    SceneManager.LoadScene(mapData.ScenePath);
        //}
        //else
        //{
        //    Debug.Log("<color=#7FFF00><size=12>" + $"{enterMapCmd.MapID}场景未找到" + "</size></color>");
        //}

        LoadScene(enterMapCmd.MapID);
    }

    private static void LoadScene(int mapID)
    {
        var mapData = MapTable.Instance[mapID];
        if (mapData == null)
        {
            Debug.Log("<color=#EE2C2C><size=12>" + $"未找到地图 mapID:{mapID} " + "</size></color>");
            return;
        }
        SceneManager.LoadScene(mapData.ScenePath);
    }
}

