using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


//所有界面基类 有底图的对话框
public abstract class Dialog
{
    protected GameObject _uiRoot;

    Button _btnClose;


    //类 存在  资源是否存在？
    public bool IsAlive { get => _uiRoot != null; }


    //加载UIRoot
    protected GameObject LoadUI(string _uiRootPath)
    {
        _uiRoot = UIMgr.Instance.Add(_uiRootPath);
        _btnClose = _uiRoot.Find<Button>("BtnClose");
        if (_btnClose != null)
        {
            _btnClose.onClick.AddListener(OnClickClose);
        }
        return _uiRoot;
    }



    //按钮事件关闭
    protected virtual void OnClickClose()
    {
        CloseSelf();
    }


    //关闭自己
    public virtual void CloseSelf()
    {
        UIMgr.Instance.Remove(_uiRoot);
    }
}
