using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 角色属性对话框
/// </summary>
public class RoleAttrDlg:Dialog
{

    UIModelStudio _modelStudio;

    public RoleAttrDlg()
    {
        LoadUI("UI/System/RoleAttr/RoleAttrDlg");

        //加载模型
        _modelStudio = new UIModelStudio();
        _modelStudio.Init();


        //找到主角
        var mainRole = RoleMgr.Instance.MainRole;
        if (mainRole != null)
        {
            var mainRoleModel = ResMgr.Instance.GetInstance(mainRole.ModelPath);
            _modelStudio.SetModel(mainRoleModel);
        }
        else
        {
            Debug.LogError("未找到主角!");
        }


        var camera = _modelStudio.Root.transform.Find<Camera>("Camera");

        var modelArea = _uiRoot.Find<RectTransform>("RoleModel");

        var texture = new RenderTexture((int)modelArea.rect.width, (int)modelArea.rect.height, 24);

        camera.targetTexture = texture;

        var modelRawImage = modelArea.Find<RawImage>("RawImage");

        modelRawImage.gameObject.AddComponent<TouchEx>().DragCallback = (x) =>{
            _modelStudio.PlaceTrans.Rotate(Vector3.up, -x.delta.x);
        };

        modelRawImage.texture = texture;

    }
    
}
