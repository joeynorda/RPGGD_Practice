using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//UI����
public class UIMgr : Singleton<UIMgr>
{


    //����EventSystem ��� ���UI �¼�ճ�� 
    public bool UIEventSystemEnabled
    {
        set { _eventSystem.enabled = value; }
        get { return _eventSystem.enabled; }
    }






    GameObject _uiRoot;
    EventSystem _eventSystem;

    //����UI�㼶
    //��������  ��Σ����ڵ� 
    Dictionary<UILayer, GameObject> _uiLayerRoot = new Dictionary<UILayer, GameObject>();

    public void Init()
    {
        if (_uiRoot == null)
        {
            _uiRoot = ResMgr.Instance.GetInstance("UI/UISystem");
            GameObject.DontDestroyOnLoad(_uiRoot);
            _eventSystem = _uiRoot.Find<EventSystem>("EventSystem");
        }

        //UI ��5��
        _uiLayerRoot.Add(UILayer.Scene, _uiRoot.Find<Transform>("Canvas/Scene").gameObject);
        _uiLayerRoot.Add(UILayer.Touch, _uiRoot.Find<Transform>("Canvas/Touch").gameObject);
        _uiLayerRoot.Add(UILayer.FightUI, _uiRoot.Find<Transform>("Canvas/FightUI").gameObject);
        _uiLayerRoot.Add(UILayer.Normal, _uiRoot.Find<Transform>("Canvas/Normal").gameObject);
        _uiLayerRoot.Add(UILayer.Top, _uiRoot.Find<Transform>("Canvas/Top").gameObject);

    }


    //���UI  
    public GameObject Add(string uiPath,UILayer layer = UILayer.Normal)
    {
        var root = ResMgr.Instance.GetInstance(uiPath);
        if (_uiLayerRoot.TryGetValue(layer, out GameObject target))
        {
            root.transform.SetParent(target.transform,false);
        }
        return root;
    }


    //ɾ��UI
    public void Remove(GameObject ui)
    {
        //��Դ���� 

        ResMgr.Instance.Release(ui);
    }



    /// <summary>
    /// �滻UI
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
    /// ɾ��ĳһ�㼶����UI
    /// </summary>
    public void RemoveLayer(UILayer layer = UILayer.Normal)
    {
        //remove
        _uiLayerRoot[layer].transform.ClearAllChilds();
    }
}




//ui���
public enum UILayer
{
    Scene,
    Touch,
    FightUI,
    Normal,
    Top
}