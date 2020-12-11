﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//------------------------------------------
//* 
//* 作者名称: ZzrDream
//* 
//* 时间:2020/10/18 
//* 
//* 环境:Unity2019+VS2017
//* 
//* 描述:状态模式模板代码
//* 
//------------------------------------------
public class SceneStateController : MonoBehaviour
{
    private ISceneState state;
    private bool isRunStart;


    private AsyncOperation m_AO;

    public SceneStateController() { }

    public void SetState(ISceneState state, bool isLoadScene = true)
    {

        //通知前一个State结束
        if (state != null)
        {
            state.StateEnd();
        }
        Debug.Log("SetState:" + state.ToString());

        this.state = state;

        if (isLoadScene)
        {
            m_AO = SceneManager.LoadSceneAsync(state.SceneName);
            isRunStart = false;
        }
        else
        {
            state.StateStart();
            isRunStart = true;
        }




    }
    public void StateUpdate()
    {
        if (m_AO != null && !m_AO.isDone) return;

        if (!isRunStart && m_AO != null && m_AO.isDone)
        {
            state.StateStart();
            isRunStart = true;
        }
        if (state != null)
        {
            state.StateUpdate();
        }
    }
}
