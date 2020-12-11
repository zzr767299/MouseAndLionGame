using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFrameWork.UI;
using SimpleFrameWork.Event;
using SimpleFrameWork;
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
public class MainMenuScene : ISceneState
{
    private GameObject btnSelects;
    private GameObject btnSelectsTwo;


    private Button btn_Select1;
    private Button btn_Select2;

    private Button btn_Select2_1;
    private Button btn_Select2_2;



  

    private GameManager gameManager;
   
    public MainMenuScene(SceneStateController controller) : base(controller, "MainScene")
    {
        

    }

    public override void StateStart()
    {
        gameManager = GameManager.Instance;


        EventCenter.AddListener(EventDefine.ShowSelectOneBtn, SelectOneBtnShow);
        EventCenter.AddListener(EventDefine.ShowSelectTwoBtn, ShowSelectTwoBtn);

        btnSelects = UITool.FindUIGameObject("btnSelects");
        btnSelectsTwo = UITool.FindUIGameObject("btnSelectsTwo");
     

        btn_Select1 = UITool.GetButton("btn_Select1");
        btn_Select2 = UITool.GetButton("btn_Select2");

        btn_Select2_1 = UITool.GetButton("btn_Select2_1");
        btn_Select2_2 = UITool.GetButton("btn_Select2_2");

      

        btnSelects.SetActive(false);
        btnSelectsTwo.SetActive(false);
     

        btn_Select1.onClick.AddListener(() =>
        {
            gameManager.IsContiute = false;
            btnSelects.SetActive(false);
        });
        btn_Select2.onClick.AddListener(() =>
        {
            gameManager.IsContiute = false;
            EventCenter.Broadcast(EventDefine.SetFromTextAsset, gameManager.Select2);
            btnSelects.SetActive(false);
        });
        btn_Select2_1.onClick.AddListener(() =>
        {
            gameManager.IsContiute = false;
            btnSelectsTwo.SetActive(false);
            controller.SetState(new GameOneScene(controller));
        });
        btn_Select2_2.onClick.AddListener(() =>
        {
            gameManager.IsContiute = false;
            btnSelectsTwo.SetActive(false);
            controller.SetState(new GameThreeScene(controller));
        });

      
   
    }
    ~MainMenuScene()
    {
        EventCenter.RemoveListener(EventDefine.ShowSelectOneBtn, SelectOneBtnShow);
        EventCenter.RemoveListener(EventDefine.ShowSelectTwoBtn, ShowSelectTwoBtn);
   
    }
    public void SelectOneBtnShow()
    {
        btnSelects.SetActive(true);
    }

    public void ShowSelectTwoBtn()
    {
        btnSelectsTwo.SetActive(true);
    }

  
}
