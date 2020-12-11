using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 资源工厂基类与接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IResFactory<T>
{
    void InitFactory(Func<string, T> factoryMehtod);

    T GetRes(string resName);
}

public class ResFactory<T> : IResFactory<T>
{
    public Dictionary<string, T> resDic;

    private Func<string, T> mFactoryMethod;
    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T GetRes(string resName)
    {
        return mFactoryMethod(resName);
    }
    /// <summary>
    /// 初始化工厂
    /// </summary>
    /// <param name="factoryMehtod"></param>
    public void InitFactory(Func<string, T> factoryMehtod)
    {
        resDic = new Dictionary<string, T>();
        mFactoryMethod = factoryMehtod;
    }
}

