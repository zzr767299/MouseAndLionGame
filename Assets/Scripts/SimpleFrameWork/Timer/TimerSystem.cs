using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///*-------------------------------------------------------
///*  作者: ZzrDream
///* 
///*  时间: 2020/9/1
///* 
///*  开发环境: Unity2019.4.2f1 + VS2019
///* 
///*  作用: 定时系统
///*------------------------------------------------------
namespace SimpleFrameWork.Timer
{
    public class TimerSystem : MonoSingleton<TimerSystem>
    {
    


        /// <summary>
        /// 存储所有的定时任务
        /// </summary>
        private List<PETimerTask> timerTasks = new List<PETimerTask>();
        /// <summary>
        /// 缓存列表
        /// </summary>
        private List<PETimerTask> tempTimerTasks = new List<PETimerTask>();


        private List<PEFrameTask> frameTasks = new List<PEFrameTask>();
        private List<PEFrameTask> tempFrameTasks = new List<PEFrameTask>();

        /// <summary>
        /// 当前的帧
        /// </summary>
        private int frameCounter;

        private static readonly string obj = "lock";
        /// <summary>
        /// 全局Tid
        /// </summary>
        private int tid;

        /// <summary>
        /// 全局TID集合
        /// </summary>
        private List<int> idList = new List<int>();
        /// <summary>
        /// 回收全局TID集合
        /// </summary>
        private List<int> recTidList = new List<int>();
        public void Awake()
        {
            Debug.Log("Timer Init Done..");
        }

        void Update()
        {
            CheakTimerTask();
            CheakFrameTask();

            if (recTidList.Count > 0)
            {
                RecycleTid();  //回收已经删除的tid
            }

        }
        /// <summary>
        /// 检测时间任务
        /// </summary>
        private void CheakTimerTask()
        {
            //为什么要先加入缓存列表，因为延长时间并不是当前帧执行，需要等待一帧，所以在上一帧加入缓存列表中，在下一帧再取出
            for (int tempIndex = 0; tempIndex < tempTimerTasks.Count; tempIndex++)
            {
                timerTasks.Add(tempTimerTasks[tempIndex]);

            }
            tempTimerTasks.Clear();


            for (int index = 0; index < timerTasks.Count; index++)
            {
                PETimerTask timerTask = timerTasks[index];
                if (timerTask.destTime > Time.realtimeSinceStartup * 1000)
                {
                    continue;
                }
                else
                {
                    Action action = timerTask.callBack;
                    if (action != null)
                    {
                        action();
                    }
                    if (timerTask.count == 1)
                    {
                        timerTasks.RemoveAt(index);
                        index--;
                        recTidList.Add(timerTask.tid);
                    }
                    else
                    {
                        if (timerTask.count != 0)
                        {
                            timerTask.count -= 1;
                        }
                        timerTask.destTime += timerTask.delay;
                    }


                }
            }

        }
        /// <summary>
        /// 检测帧任务
        /// </summary>
        private void CheakFrameTask()
        {
            //为什么要先加入缓存列表，因为延长时间并不是当前帧执行，需要等待一帧，所以在上一帧加入缓存列表中，在下一帧再取出
            for (int tempIndex = 0; tempIndex < tempFrameTasks.Count; tempIndex++)
            {
                frameTasks.Add(tempFrameTasks[tempIndex]);

            }
            tempFrameTasks.Clear();

            frameCounter += 1;

            for (int index = 0; index < frameTasks.Count; index++)
            {
                PEFrameTask frameTask = frameTasks[index];
                if (frameCounter < frameTask.destFrame)
                {
                    continue;
                }
                else
                {
                    Action action = frameTask.callBack;
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.ToString());
                    }

                    if (frameTask.count == 1)
                    {
                        frameTasks.RemoveAt(index);
                        index--;
                        recTidList.Add(frameTask.tid);
                    }
                    else
                    {
                        if (frameTask.count != 0)
                        {
                            frameTask.count -= 1;
                        }
                        frameTask.destFrame += frameTask.delay;
                    }


                }
            }

        }

        #region Timer Task
        /// <summary>
        /// 添加时间任务
        /// </summary>
        /// <param name="callback">等待一段时间后需要执行的方法</param>
        /// <param name="destTime">第一次执行等待的时间</param>
        /// <param name="delay">(第二次执行后)重复执行等待的时间</param>
        /// <param name="count">重复执行的次数，0为无限重复执行</param>
        /// <param name="timeType">添加的时间类型，默认毫秒为单位，用秒需要乘1000</param>
        /// <returns></returns>
        public int AddTimerTask(Action callback, float destTime, float delay, int count = 1, PETimeType timeType = PETimeType.Millisecond)
        {
            if (timeType != PETimeType.Millisecond)
            {
                switch (timeType)
                {
                    case PETimeType.Second:
                        destTime = destTime * 1000;
                        break;
                    case PETimeType.Minute:
                        destTime = destTime * 1000 * 60;
                        break;
                    case PETimeType.Hour:
                        destTime = destTime * 1000 * 60 * 60;
                        break;
                    case PETimeType.Day:
                        destTime = destTime * 1000 * 60 * 60 * 24;
                        break;
                    default:
                        Debug.Log("Add Time Type Error！");
                        break;
                }
            }
            int tid = GetID();
            destTime = Time.realtimeSinceStartup * 1000 + destTime;
            PETimerTask timerTask = new PETimerTask(callback, destTime, delay, count, tid);
            tempTimerTasks.Add(timerTask);
            idList.Add(tid);
            return tid;
        }
        /// <summary>
        /// 删除定时任务
        /// </summary>
        public bool DeleteTimerTask(int tid)
        {
            //是否删除任务成功
            bool result = false;
            for (int i = 0; i < timerTasks.Count; i++)
            {
                PETimerTask timerTask = timerTasks[i];
                if (timerTask.tid == tid)
                {
                    timerTasks.RemoveAt(i);
                    for (int j = 0; j < idList.Count; j++)
                    {
                        if (tid == idList[j])
                        {
                            idList.RemoveAt(j);
                            break;
                        }
                    }
                    result = true;
                    break;
                }
            }

            //从缓存列表里面找
            if (!result)
            {
                for (int i = 0; i < tempTimerTasks.Count; i++)
                {
                    PETimerTask timerTask = tempTimerTasks[i];
                    if (timerTask.tid == tid)
                    {
                        tempTimerTasks.RemoveAt(i);
                        for (int j = 0; j < idList.Count; j++)
                        {
                            if (tid == idList[j])
                            {
                                idList.RemoveAt(j);
                                break;
                            }
                        }
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 更换定时任务
        /// </summary>
        public bool ReplaceTimerTask(int tid, Action callback, float destTime, float delay, int count = 1, PETimeType timeType = PETimeType.Millisecond)
        {
            if (timeType != PETimeType.Millisecond)
            {
                switch (timeType)
                {
                    case PETimeType.Second:
                        destTime = destTime * 1000;
                        break;
                    case PETimeType.Minute:
                        destTime = destTime * 1000 * 60;
                        break;
                    case PETimeType.Hour:
                        destTime = destTime * 1000 * 60 * 60;
                        break;
                    case PETimeType.Day:
                        destTime = destTime * 1000 * 60 * 60 * 24;
                        break;
                    default:
                        Debug.Log("Add Time Type Error！");
                        break;
                }
            }
            destTime = Time.realtimeSinceStartup * 1000 + destTime;
            PETimerTask newTask = new PETimerTask(callback, destTime, delay, count, tid);
            bool isRep = false;
            for (int i = 0; i < timerTasks.Count; i++)
            {
                if (tid == timerTasks[i].tid)
                {
                    timerTasks[i] = newTask;
                    isRep = true;
                    break;
                }
            }

            if (!isRep)
            {
                for (int i = 0; i < tempTimerTasks.Count; i++)
                {
                    if (tid == tempTimerTasks[i].tid)
                    {
                        tempTimerTasks[i] = newTask;
                        isRep = true;
                        break;
                    }
                }
            }

            return isRep;
        }
        #endregion

        #region Frame Task
        /// <summary>
        /// 添加时间任务
        /// </summary>
        /// <param name="callback">回调方法</param>
        /// <param name="destTime">等待的时间</param>
        /// <param name="delay">延迟的时间</param>
        /// <param name="count"></param>
        /// <param name="timeType">添加的时间类型</param>
        /// <returns></returns>
        public int AddFrameTask(Action callback, int destTime, int delay, int count = 1)
        {

            int tid = GetID();
            destTime = frameCounter + destTime;
            PEFrameTask frameTask = new PEFrameTask(callback, destTime, delay, count, tid);
            tempFrameTasks.Add(frameTask);
            idList.Add(tid);
            return tid;
        }
        /// <summary>
        /// 删除定时任务
        /// </summary>
        public bool DeleteFrameTask(int tid)
        {
            //是否删除任务成功
            bool result = false;
            for (int i = 0; i < frameTasks.Count; i++)
            {
                PEFrameTask frameTask = frameTasks[i];
                if (frameTask.tid == tid)
                {
                    frameTasks.RemoveAt(i);
                    for (int j = 0; j < idList.Count; j++)
                    {
                        if (tid == idList[j])
                        {
                            idList.RemoveAt(j);
                            break;
                        }
                    }
                    result = true;
                    break;
                }
            }

            //从缓存列表里面找
            if (!result)
            {
                for (int i = 0; i < tempFrameTasks.Count; i++)
                {
                    PEFrameTask frameTask = tempFrameTasks[i];
                    if (frameTask.tid == tid)
                    {
                        tempFrameTasks.RemoveAt(i);
                        for (int j = 0; j < idList.Count; j++)
                        {
                            if (tid == idList[j])
                            {
                                idList.RemoveAt(j);
                                break;
                            }
                        }
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 更换定时任务
        /// </summary>
        public bool ReplaceFrameTask(int tid, Action callback, int destTime, int delay, int count = 1)
        {

            destTime = frameCounter + destTime;
            PEFrameTask newTask = new PEFrameTask(callback, destTime, delay, count, tid);
            bool isRep = false;
            for (int i = 0; i < frameTasks.Count; i++)
            {
                if (tid == frameTasks[i].tid)
                {
                    frameTasks[i] = newTask;
                    isRep = true;
                    break;
                }
            }

            if (!isRep)
            {
                for (int i = 0; i < tempFrameTasks.Count; i++)
                {
                    if (tid == tempFrameTasks[i].tid)
                    {
                        tempFrameTasks[i] = newTask;
                        isRep = true;
                        break;
                    }
                }
            }

            return isRep;
        }
        #endregion


        #region Tools
        /// <summary>
        /// 获取全局ID
        /// </summary>
        /// <returns></returns>
        private int GetID()
        {
            lock (obj)
            {
                tid += 1;

                //安全代码
                while (true)
                {
                    if (tid == int.MaxValue)
                    {
                        tid = 0;
                    }
                    bool used = false;
                    for (int i = 0; i < idList.Count; i++)
                    {
                        if (tid == idList[i])
                        {
                            used = true;
                            break;
                        }
                    }
                    if (!used)
                    {
                        break;
                    }
                    else
                    {
                        tid += 1;
                    }

                }
            }

            return tid;
        }

        /// <summary>
        /// 回收全局tid
        /// </summary>
        private void RecycleTid()
        {
            for (int i = 0; i < recTidList.Count; i++)
            {
                int tid = recTidList[i];
                for (int j = 0; j < idList.Count; j++)
                {
                    if (tid == idList[j])
                    {
                        recTidList.Remove(j);
                        break;
                    }
                }
            }
            recTidList.Clear();



        }
        #endregion
    }

}
