﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFrameWork.UI;
using UnityEngine.UI;
//------------------------------------------
//* 
//* 作者名称: ZzrDream
//* 
//* 时间:2020/12/1 
//* 
//* 环境:Unity2019+VS2017
//* 
//* 描述:开始场景
//* 
//------------------------------------------
public class StartScene : ISceneState
{
    private Button btn_Player;
    private Button btn_End;
    private Button btn_Help;
    private Button btn_Back;

    private GameObject startPanel;
    private GameObject helpPanel;
    public StartScene(SceneStateController controller) : base(controller, "StartScene")
    {
        btn_Player = UITool.GetButton("btn_Play");
        btn_Help = UITool.GetButton("btn_Help");
        btn_End = UITool.GetButton("btn_End");
        btn_Back = UITool.GetButton("btn_Back");


        startPanel = UITool.FindUIGameObject("StartPanel");
        helpPanel = UITool.FindUIGameObject("HelpPanel");

      
    }

    public override void StateStart()
    {
        helpPanel.SetActive(false);

        btn_Player.onClick.AddListener(() =>
        {
            controller.SetState(new MainMenuScene(controller));  
        });

        btn_Help.onClick.AddListener(() =>
        {
            startPanel.SetActive(false);
            helpPanel.SetActive(true);
        });

        btn_Back.onClick.AddListener(() =>
        {
            startPanel.SetActive(true);
            helpPanel.SetActive(false);

        });
        btn_End.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
    

    public override void StateUpdate()
    {
    
    }

}
