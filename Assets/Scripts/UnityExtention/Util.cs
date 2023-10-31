using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Util
{
    /// <summary>
    /// 浮点数相等 判断
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool FloatEqual(float a,float b)
    {
        return MathF.Abs(a - b) < 0.001f;
    }
}