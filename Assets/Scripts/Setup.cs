using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    //���� ���� ��¼

    private void Awake()
    {


        GameManager.Instance.Init();
    }
}

