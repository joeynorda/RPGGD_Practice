using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源加载管理
/// 使得资源加载方式 和 使用逻辑分离
/// 以便于支持热更新、对象池
/// 走统一入口
/// </summary>
public class ResMgr : Singleton<ResMgr>
{


    /// <summary>
    /// 获取实例
    /// </summary>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public GameObject GetInstance(string resPath)
    {

        var tmp = GetResources<GameObject>(resPath);
        if (tmp == null)
        {
            Debug.Log("<color=#EE2C2C><size=12>" + $"{resPath} 未找到" + "</size></color>");
            return null;
        }
        return GameObject.Instantiate(tmp);
    }

    /// <summary>
    /// 加载T 类型资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public T GetResources<T>(string resPath)where T :Object
    {
        //************
        //全局唯一  只有这一个地方调用Resources.Load<T>()
        return Resources.Load<T>(resPath);
    }


    /// <summary>
    /// 资源 释放
    /// </summary>
    public void Release(GameObject target)
    {
        //回收到缓冲池

        //硬删除
        GameObject.Destroy(target);
    }

}