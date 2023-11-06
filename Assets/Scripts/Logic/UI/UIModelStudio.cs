using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//UI模型展示
internal class UIModelStudio:ModelStudio
{
    public override void Init()
    {
        _Root = ResMgr.Instance.GetInstance("UI/System/UIModelStudio");
        _placeTrans = _Root.Find<Transform>("PlacePoint");
    }
}
