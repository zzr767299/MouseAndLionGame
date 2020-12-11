using SimpleFrameWork.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFrameWork;
using SimpleFrameWork.Event;
using System;
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
public class GameOneScene : ISceneState
{
    private Text txt_FruitNum;
    private GameManager gameManager;
    private int totalFruitNum = 5;
    private Text txt_Timer;
    public GameOneScene(SceneStateController controller) : base(controller, "GameOneScene")
    {

     

    }

    public override void StateStart()
    {

        gameManager = GameManager.Instance;


        txt_FruitNum = UITool.GetText("txt_FruitNum");
        txt_Timer = UITool.GetText("txt_Timer");
    }

    public override void StateUpdate()
    {
        txt_FruitNum.text = "Wild fruits:" + gameManager.FruitNum + "/"+ totalFruitNum;
        Debug.Log("gameManager:"+gameManager.FisrtGameIndex);
        if (gameManager.FisrtGameIndex < 0)
        {
            controller.SetState(new GameResetScene(controller));
        }
        if (gameManager.FruitNum >= totalFruitNum)
        {
            controller.SetState(new MainMenuScene(controller));
            //  EventCenter.Broadcast(EventDefine.SetFromTextAsset, gameManager.Select1_2);
            gameManager.textFile = gameManager.Polt1_1;
        }
        if (gameManager.FirstTimer <= 0)
        {
            controller.SetState(new GameResetScene(controller)); 
        }
        gameManager.FirstTimer -= Time.deltaTime;
        txt_Timer.text = "Time:" + Math.Round(gameManager.FirstTimer, 0) + "s";
    }



}
