using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//UI管理
public class UIMgr : Singleton<UIMgr>
{
    GameObject _uiRoot;


    //保存UI层级
    //快速索引  层次，根节点 
    Dictionary<UILayer, GameObject> _uiLayerRoot = new Dictionary<UILayer, GameObject>();

    public void Init()
    {
        if (_uiRoot == null)
        {
            _uiRoot = ResMgr.Instance.GetInstance("UI/UISystem");
            GameObject.DontDestroyOnLoad(_uiRoot);
        }

        //UI 分5层
        _uiLayerRoot.Add(UILayer.Scene, _uiRoot.Find<Transform>("Canvas/Scene").gameObject);
        _uiLayerRoot.Add(UILayer.Touch, _uiRoot.Find<Transform>("Canvas/Touch").gameObject);
        _uiLayerRoot.Add(UILayer.FightUI, _uiRoot.Find<Transform>("Canvas/FightUI").gameObject);
        _uiLayerRoot.Add(UILayer.Normal, _uiRoot.Find<Transform>("Canvas/Normal").gameObject);
        _uiLayerRoot.Add(UILayer.Top, _uiRoot.Find<Transform>("Canvas/Top").gameObject);

    }


    //添加UI  
    public GameObject Add(string uiPath,UILayer layer = UILayer.Normal)
    {
        var root = ResMgr.Instance.GetInstance(uiPath);
        if (_uiLayerRoot.TryGetValue(layer, out GameObject target))
        {
            root.transform.SetParent(target.transform,false);
        }
        return root;
    }


    //删除UI
    public void Remove(GameObject ui)
    {
        //资源回收 

        ResMgr.Instance.Release(ui);
    }



    /// <summary>
    /// 替换UI
    /// </summary>
    /// <param name="uipath"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public GameObject Replace(string uipath,UILayer layer = UILayer.Normal)
    {
        //remove
        RemoveLayer(layer);

        //Add
        return Add(uipath, layer);
    }


    /// <summary>
    /// 删除某一层级所有UI
    /// </summary>
    public void RemoveLayer(UILayer layer = UILayer.Normal)
    {
        //remove
        _uiLayerRoot[layer].transform.ClearAllChilds();
    }
}




//ui层次
public enum UILayer
{
    Scene,
    Touch,
    FightUI,
    Normal,
    Top
}