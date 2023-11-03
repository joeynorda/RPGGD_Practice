using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScene
{
    GameObject _root;
    TouchEx _touchEx;


    //检测层级
    LayerMask layerMask ;

    public Action<RaycastHit> OnHitStmCallback;

    public TouchScene()
    {
        _root = UIMgr.Instance.Add("UI/FightUI/TouchScene", UILayer.Touch);

        _touchEx = _root.AddComponent<TouchEx>();

        _touchEx.PointerUpCallback = OnTouchScene;
    }


    //点击弹起  UI->场景交互  射线
    private void OnTouchScene(PointerEventData eventData)
    {
        //点击位置坐标 eventData.position;

        //发出射线
        //Ray ray = Camera.main.ScreenPointToRay(eventData.position);

        Ray ray = CameraControll.Instance.GetComponent<Camera>().ScreenPointToRay(eventData.position);

        layerMask = LayerMask.NameToLayer("Floor");


        //~(1<<layerMask)  除layerMask 所有层
        if (Physics.Raycast(ray, out RaycastHit hit, layerMask))
        {
            OnHitStmCallback?.Invoke(hit);
            Debug.Log("<color=#7FFF00><size=12>" + $"{hit.transform.gameObject.name}" + "</size></color>");
        }
    }
}
