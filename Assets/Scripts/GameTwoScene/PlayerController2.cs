using SimpleFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///*-------------------------------------------------------
///*  作者: ZzrDream
///* 
///*  时间: 2020/10/27
///* 
///*  开发环境: Unity2019.4.2f1 + VS2019
///* 
///*  作用: 自己封装的Vector2类
///*------------------------------------------------------
public class PlayerController2 : MonoBehaviour
{
    public float speed;

    Animator animator;
    Vector3 movement;

    private float scale = 5;

    private GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
    }


    void Update()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, 0);

        transform.Translate(movement);//移动

        if (movement != Vector3.zero)//动画
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (movement.x > 0)//翻脸
            transform.localScale = new Vector3(scale, scale, scale);
        if (movement.x < 0)
            transform.localScale = new Vector3(-scale, scale, scale);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.House)
        {
            gameManager.Controller.SetState(new MainMenuScene(gameManager.Controller));
            gameManager.textFile = gameManager.End1;
        }
        if (collision.gameObject.tag == Tags.Death)
        {
            gameManager.Controller.SetState(new GameResetScene2(gameManager.Controller));
        }
    }
}
