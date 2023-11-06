using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//摄影棚 显示UI 模型 
public class ModelStudio
{
    protected GameObject _Root;


    public GameObject Root => _Root;


    protected Transform _placeTrans;

    public Transform PlaceTrans { get => _placeTrans; }

    GameObject _modelRoot; //放置的模型根 节点

    public virtual void Init()
    {
        _Root = ResMgr.Instance.GetInstance("UI/SelectRole/ModelStudio");
        _placeTrans = _Root.Find<Transform>("PlacePoint");
    }


    //放置模型
    public void SetModel(GameObject modelRoot)
    {
        _modelRoot = modelRoot;
        _modelRoot.transform.SetParent(_placeTrans, false);
    }

    //销毁
    public void Destroy()
    {
        ResMgr.Instance.Release(_Root);
    }

    //清空模型
    public void ClearModel()
    {
        _placeTrans.transform.ClearAllChilds();
    }
}
