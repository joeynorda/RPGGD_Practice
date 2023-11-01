using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private static CameraControll _instance;
    public static CameraControll Instance { get => _instance; }

    public Transform TheTransform
    {
        get => m_cameraTransform;
    }


    //跟随对象
    public Transform m_followTarget = null;
    public float m_smoothFactor = 0.01f;
    public Vector3 m_cameraRotOffset;
    public Vector3 m_cameraPosOffset;

    private Transform m_cameraTransform;

    private void Awake()
    {
        _instance = this;

        m_cameraTransform = transform;
        SetCameraRotation();
    }



    private void SetCameraRotation()
    {
        m_cameraTransform.localEulerAngles = m_cameraRotOffset;
    }

    private void LateUpdate()
    {
        if (m_followTarget != null)
        {
#if UNITY_EDITOR
            SetCameraRotation();
#endif
            Vector3 targetPos = m_followTarget.position + m_cameraPosOffset;
            m_cameraTransform.position = Vector3.Lerp(m_cameraTransform.position, targetPos, Time.deltaTime * m_smoothFactor);
        }
    }


    /// <summary>
    /// 设定跟随目标
    /// </summary>
    /// <param name="targetTrans"></param>
    public void SetFollowTarget(Transform targetTrans)
    {
        m_followTarget = targetTrans;
    }
}
