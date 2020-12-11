using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSingleton<T> where T : class, new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T t = new T();
                if (t is MonoBehaviour)
                {
                    Debug.LogError("改类不是普通类，需要继承MonoSingleton");
                    return null;
                }
                instance = t;
            }
            return instance;
        }
    }
}
