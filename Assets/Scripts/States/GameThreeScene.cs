using SimpleFrameWork;
using SimpleFrameWork.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
public class GameThreeScene : ISceneState
{
   private Text txt_Timer;
   private Text txt_BranchNum;
   private GameManager gameManager;
   private int totalBranchNum = 5;
    public GameThreeScene(SceneStateController controller) : base(controller, "GameThreeScene")
    {




    }
    public override void StateStart()
    {
        gameManager = GameManager.Instance;
        txt_Timer = UITool.GetText("txt_Timer");
        txt_BranchNum = UITool.GetText("txt_BrancheNum");
    }
    public override void StateUpdate()
    {
        txt_BranchNum.text = "Wild fruits:" + gameManager.BranchNum + "/" + totalBranchNum;
      
        if (gameManager.ThirdGameIndex < 0)
        {
            controller.SetState(new GameResetScene(controller));
        }
        if (gameManager.FruitNum >= totalBranchNum)
        {
            controller.SetState(new MainMenuScene(controller));
            gameManager.textFile = gameManager.End2;
        }
        if (gameManager.ThirdGameIndex <= 0)
        {
            controller.SetState(new GameResetScene(controller));
        }
        gameManager.ThirdTimer -= Time.deltaTime;
        txt_Timer.text = "Time:" + Math.Round(gameManager.ThirdTimer, 0) + "s";
    }
}
