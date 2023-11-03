using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick
{


    public Action<Vector2> OnMoveDir;
    public Action OnMoveEnd;


    GameObject _root;

    RectTransform _innerBall;
    RectTransform _outer;

    TouchEx _touchRotate;

    float _radius;
    Vector2 _centerPos;

    Vector2 _dir;//人物移动方向

    public Joystick()
    {

        _root = UIMgr.Instance.Add("UI/FightUI/JoyStick", UILayer.FightUI);

        _innerBall = _root.Find<RectTransform>("bg/inner");
        _outer = _root.Find<RectTransform>("bg");


        //小球移动半径
        _radius = _outer.rect.width/2;

        //大球的中心点
        _centerPos = Vector2.zero;//_outer.position;



        _touchRotate=_outer.AddComponent<TouchEx>();

        _touchRotate.DragCallback = OnDragCallback;
        _touchRotate.PointerDownCallback = OnPointerDownCallback;
        _touchRotate.PointerUpCallback = OnPointerUpCallback;

     

        //
        //计时器 停止拖拽的时候 也需要发送移动
        TimerMgr.Instance.CreateTimerAndStart(0.1f, -1, OnLoop);
    }


    //定时器 定时刷新 
    private void OnLoop()
    {

        // 判断 小球在中心点  不移动 return
        if (_dir == Vector2.zero)
        {
            return;
        }

        //告诉外界拖动方向  -> 移动方向
        OnMoveDir?.Invoke(_dir);
    }

    private void OnPointerUpCallback(PointerEventData obj)
    {
        _innerBall.localPosition = _centerPos;

        _dir = Vector2.zero;

        //移动结束
        OnMoveEnd?.Invoke();
    }

    private void OnPointerDownCallback(PointerEventData eventData)
    {
        OnDragCallback(eventData);
    }


    private void OnDragCallback(PointerEventData eventData)
    {
       
        //位置限定

        Vector2 targetpos;    //转换成以大球中心 为零点的  坐标系  注意大球的Pivot
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_outer, eventData.position, eventData.pressEventCamera, out targetpos);

        var vec = targetpos - _centerPos;
        _dir = vec.normalized;
        var dis = vec.magnitude;

        _innerBall.localPosition = _dir * Mathf.Min(dis,_radius);

    }
}