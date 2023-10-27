using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuickCoroutine:Singleton<QuickCoroutine>
{
    GameObject _root;

    MonoBehaviour _coroutineMono; //用来跑协程

    public void Init()
    {
        _root = new GameObject("QuickCouroutine");
        GameObject.DontDestroyOnLoad(_root);
        _coroutineMono = _root.AddComponent<MonoScript>();
    }


    //开始协程
    public Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return _coroutineMono.StartCoroutine(coroutine);
    }

    //Stop

}


public class MonoScript : MonoBehaviour
{ }
