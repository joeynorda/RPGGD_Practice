using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

internal class TouchEx : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    //[SerializeField] private Transform target;
    //[SerializeField] private float roateSpeed=30;


    public Action<PointerEventData> PointerDownCallback;
    public Action<PointerEventData> PointerUpCallback;

    //拖动回调
    public Action<PointerEventData> DragCallback;

    public void OnDrag(PointerEventData eventData)
    {
        //target.transform.Rotate(Vector3.up, -eventData.delta.x * roateSpeed);

        DragCallback?.Invoke(eventData);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDownCallback?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUpCallback?.Invoke(eventData);
    }
}

