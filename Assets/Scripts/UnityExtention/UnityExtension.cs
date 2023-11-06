using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UnityExtension  //开放修改   扩展方法
{

    /// <summary>
    /// 查找
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T Find<T>(this GameObject parent, string path)
    {
        var targetObj = parent.transform.Find(path);
        if (targetObj == null)
        {
            return default(T);
        }
        return targetObj.GetComponent<T>();
    }


    /// <summary>
    /// 查找T类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static T Find<T>(this Transform parent, string path)
    {
        return parent.Find(path).GetComponent<T>();
    }


    /// <summary>
    /// 清除所有子节点
    /// </summary>
    /// <param name="parentTrans"></param>
    public static void ClearAllChilds(this Transform parentTrans)
    {
        for (int i = 0; i < parentTrans.childCount; i++)
        {
            UnityEngine.GameObject.Destroy(parentTrans.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// 设定layer
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="layer"></param>
    public static void SetChildLayer(this Transform trans, int layer)
    {
        if (trans != null)
        {
            trans.gameObject.layer = layer;
            for (int i = 0; i < trans.childCount; i++)
            {
                trans.GetChild(i).gameObject.layer = layer;
            }
        }
    }

}
