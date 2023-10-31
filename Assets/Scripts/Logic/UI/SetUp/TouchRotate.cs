using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

internal class TouchRotate : MonoBehaviour, IDragHandler
{
    //[SerializeField] private Transform target;
    //[SerializeField] private float roateSpeed=30;

    //拖动回调
    public Action<PointerEventData> DragCallback;

    public void OnDrag(PointerEventData eventData)
    {
        //target.transform.Rotate(Vector3.up, -eventData.delta.x * roateSpeed);

        DragCallback?.Invoke(eventData);

    }
}

