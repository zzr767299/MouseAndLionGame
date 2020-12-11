using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFrameWork;
using SimpleFrameWork.UI;
public class GameResetController2 : MonoBehaviour
{
    private Button btn_Reset;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.Instance;
        btn_Reset = UITool.GetButton("btn_Reset");

        btn_Reset.onClick.AddListener(() =>
        {
            gameManager.ResetGameOneScene();
            
            gameManager.Controller.SetState(new GameOneScene(gameManager.Controller));
        });
    }

  
}
