using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// UI管理对话框 管理所有的系统界面
/// </summary>
public class DialogMgr:Singleton<DialogMgr>
{

    //管理所有
    private List<Dialog> _allDialogs = new List<Dialog>();


    //打开对话框
    public T Open<T>() where T : Dialog, new()
    {
        var dlg = new T();
        _allDialogs.Add(dlg);
        return dlg;
    }



    //关闭对话框
    public void Close(Dialog dlg)
    {
        dlg.CloseSelf();
        _allDialogs.Remove(dlg);
    }



    //关闭所有对话框界面
    public void CloseAll()
    {

        //foreach 期间不能修改 容器
        foreach (var dlg in _allDialogs)
        {
            dlg.CloseSelf();
        }
        _allDialogs.Clear();
    }
}
