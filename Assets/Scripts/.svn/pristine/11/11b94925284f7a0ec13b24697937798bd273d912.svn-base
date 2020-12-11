using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//------------------------------------------
//* 
//* 作者名称: ZzrDream
//* 
//* 时间:2020/12/1
//* 
//* 环境:Unity2019+VS2017
//* 
//* 描述:状态接口
//* 
//------------------------------------------
public class ISceneState 
{
    protected SceneStateController controller;
    protected string sceneName;

    public string SceneName { get => sceneName; set => sceneName = value; }

    public ISceneState(SceneStateController controller, string sceneName)
    {
        this.controller = controller;
        this.SceneName = sceneName;
    }


    public virtual void StateStart()
    {

    }
    public virtual void StateUpdate()
    {

    }

    public virtual void StateEnd()
    {

    }
    
}
