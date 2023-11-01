using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Դ���ع���
/// ʹ����Դ���ط�ʽ �� ʹ���߼�����
/// �Ա���֧���ȸ��¡������
/// ��ͳһ���
/// </summary>
public class ResMgr : Singleton<ResMgr>
{


    /// <summary>
    /// ��ȡʵ��
    /// </summary>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public GameObject GetInstance(string resPath)
    {

        var tmp = GetResources<GameObject>(resPath);
        if (tmp == null)
        {
            Debug.Log("<color=#EE2C2C><size=12>" + $"{resPath} δ�ҵ�" + "</size></color>");
            return null;
        }
        return GameObject.Instantiate(tmp);
    }

    /// <summary>
    /// ����T ������Դ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resPath"></param>
    /// <returns></returns>
    public T GetResources<T>(string resPath)where T :Object
    {
        //************
        //ȫ��Ψһ  ֻ����һ���ط�����Resources.Load<T>()
        return Resources.Load<T>(resPath);
    }


    /// <summary>
    /// ��Դ �ͷ�
    /// </summary>
    public void Release(GameObject target)
    {
        //���յ������

        //Ӳɾ��
        GameObject.Destroy(target);
    }

}