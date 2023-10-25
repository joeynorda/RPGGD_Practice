using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Singleton<T> where T :new()
{
    static T _instance;

    static Singleton()
    {
        _instance = new T();
    }

    public static T Instance
    {
        get => _instance;
    }
}

