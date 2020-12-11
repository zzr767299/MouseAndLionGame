using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///*-------------------------------------------------------
///*  作者: ZzrDream
///* 
///*  时间: 2020/11/17
///* 
///*  开发环境: Unity2019.4.2f1 + VS2019
///* 
///*  作用: 定时任务数据类
///*------------------------------------------------------
public class PETimerTask 
{
    public Action callBack;//需要执行的具体任务
    public float destTime; //目标时间(当前游戏运行时间+你需要延迟的时间)
    public int count;      //循环执行的次数
    public float delay;    //当第一次执行完成后，之后需要循环执行时等待的时间(毫秒)
    public int tid;        //全局唯一ID

    public PETimerTask(Action callBack, float destTime,float delay, int count,int tid)
    {
        this.callBack = callBack;
        this.destTime = destTime;
        this.count = count;
        this.delay = delay;
        this.tid = tid;
    }



}

public class PEFrameTask
{
    public Action callBack; //需要执行的具体任务
    public int destFrame;   //目标时间(当前游戏运行时间+你需要延迟的时间)
    public int count;       //循环执行的次数
    public int delay;       //当第一次执行完成后，之后需要循环执行时等待的时间(毫秒)
    public int tid;         //全局唯一ID

    public PEFrameTask(Action callBack, int destFrame, int delay, int count, int tid)
    {
        this.callBack = callBack;
        this.destFrame = destFrame;
        this.count = count;
        this.delay = delay;
        this.tid = tid;
    }



}



public enum PETimeType
{
    Millisecond,
    Second,
    Minute,
    Hour,
    Day
}