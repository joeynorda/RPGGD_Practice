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

    Vector2 _dir;//�����ƶ�����

    public Joystick()
    {

        _root = UIMgr.Instance.Add("UI/FightUI/JoyStick", UILayer.FightUI);

        _innerBall = _root.Find<RectTransform>("bg/inner");
        _outer = _root.Find<RectTransform>("bg");


        //С���ƶ��뾶
        _radius = _outer.rect.width/2;

        //��������ĵ�
        _centerPos = Vector2.zero;//_outer.position;



        _touchRotate=_outer.AddComponent<TouchEx>();

        _touchRotate.DragCallback = OnDragCallback;
        _touchRotate.PointerDownCallback = OnPointerDownCallback;
        _touchRotate.PointerUpCallback = OnPointerUpCallback;

     

        //
        //��ʱ�� ֹͣ��ק��ʱ�� Ҳ��Ҫ�����ƶ�
        TimerMgr.Instance.CreateTimerAndStart(0.1f, -1, OnLoop);
    }


    //��ʱ�� ��ʱˢ�� 
    private void OnLoop()
    {

        // �ж� С�������ĵ�  ���ƶ� return
        if (_dir == Vector2.zero)
        {
            return;
        }

        //��������϶�����  -> �ƶ�����
        OnMoveDir?.Invoke(_dir);
    }

    private void OnPointerUpCallback(PointerEventData obj)
    {
        _innerBall.localPosition = _centerPos;

        _dir = Vector2.zero;

        //�ƶ�����
        OnMoveEnd?.Invoke();
    }

    private void OnPointerDownCallback(PointerEventData eventData)
    {
        OnDragCallback(eventData);
    }


    private void OnDragCallback(PointerEventData eventData)
    {
       
        //λ���޶�

        Vector2 targetpos;    //ת�����Դ������� Ϊ����  ����ϵ  ע������Pivot
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_outer, eventData.position, eventData.pressEventCamera, out targetpos);

        var vec = targetpos - _centerPos;
        _dir = vec.normalized;
        var dis = vec.magnitude;

        _innerBall.localPosition = _dir * Mathf.Min(dis,_radius);

    }
}