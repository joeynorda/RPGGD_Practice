using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Description: 管理所有的定时器
/// Author:csy
/// ContacT:sycheng@pisx.com
/// Date:#Date#
/// Modify:
/// </summary>
public class TimerMgr : Singleton<TimerMgr>
{

    //遍历容器的时候不允许修改容器本身 不能修改其成员 List<Timer>

    //驱动loop循环
    public event Action<float> TimerLoopCallback;

    /// <summary>
    /// 定时器
    /// </summary>
    /// <param name="deltaTime">间隔时间</param>
    /// <param name="repeatTimes">重复次数</param>
    /// /// <param name="callback">委托</param>
    /// <returns></returns>
    public Timer CreateTimer(float deltaTime, int repeatTimes, Action callback)
    {
        Timer timer = new Timer();

        timer.deltaTime = deltaTime;
        timer.repeatTimes = repeatTimes;
        timer.callback = callback;
        return timer;
    }



    /// <summary>
    /// 创建定时器并开始计时
    /// </summary>
    /// <param name="deltaTime"></param>
    /// <param name="repeatTimes"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public Timer CreateTimerAndStart(float deltaTime, int repeatTimes, Action callback)
    {
        var timer = CreateTimer(deltaTime,repeatTimes,callback);
        timer.StartTick();
        return timer;
    }










    //驱动所有定时器
    //驱动间隔  deltaTime
    public void Loop(float deltaTime)
    {
        if (TimerLoopCallback != null)
        {
            TimerLoopCallback(deltaTime);//原子事件  在内部调用修改   会延时到下一次执行 
        }
    }


}





//定时器
public class Timer
{
    //开始 结束标记
    public bool _isRunning = false;  // 尽量减少给标记位赋值


    public float deltaTime;         //间隔时间
    public int repeatTimes;         //重复次数
    public Action callback;         //回调  如何计时


    private float _passedTime;//持续时间

    private int _repeatedTimes;//已经执行次数


    //开始
    public void StartTick()
    {
        Reset();
        TimerMgr.Instance.TimerLoopCallback += Loop;
        _isRunning = true;
    }

    //暂停
    public void Pause()
    {
        TimerMgr.Instance.TimerLoopCallback -= Loop;
        _isRunning = false;
    }

    //结束
    public void StopTick()
    {
        Pause();
        Reset();
    }



    void Reset()
    {
        _passedTime = 0;
        _repeatedTimes = 0;
        Pause();
    }




    //驱动定时器
    public void Loop(float driverTime)
    {
        //判断间隔时间 
        _passedTime += driverTime;

        //浮点数的相等不能用=判断  
        //浮点值 有精度问题 有限位数小数点 近似值  小于0.001f  阈值
        //if(_passedTime >= deltaTime)
        if (_passedTime > deltaTime || Util.FloatEqual(_passedTime,deltaTime))
        {
            ++_repeatedTimes;
            _passedTime -= deltaTime;

            // 执行Callback
            callback?.Invoke();

            if (repeatTimes>0 && _repeatedTimes >= repeatTimes)
            {
                StopTick();
            }
        }
    }
}