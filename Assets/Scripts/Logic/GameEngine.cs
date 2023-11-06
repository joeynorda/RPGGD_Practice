using System.Collections;
using UnityEngine;


public class GameEngine : MonoBehaviour
{
    Timer timer1;

    private void Awake()
    {
        //lua 初始化以及Update
    }


    // Use this for initialization
    void Start()
    {
        //不负责 开始动作
        //var timer = TimerMgr.Instance.CreateTimer(1, 5, () => { Debug.Log("<color=#7FFF00><size=12>" + $"timer." + "</size></color>"); });
        //timer.Start();

        //无限循环 <0 表示无线循环
        //timer1 = TimerMgr.Instance.CreateTimer(.5f, -1, () => { Debug.Log("<color=#7FFF00><size=12>" + $"无限刷新.{Time.time}" + "</size></color>"); });
        //timer1.StartTick();

    }


    // Update is called once per frame
    void Update()
    {
        //定时器刷新 驱动定时器
        TimerMgr.Instance.Loop(Time.deltaTime);
        
    }
}
