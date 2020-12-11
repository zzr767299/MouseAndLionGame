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
public class Branch : CollectibleHealth
{

    protected override void Update()
    {
        if (isActive)
        {
            if (value >= 5)
            {
                Destroy(gameObject);
                GameManager.Instance.BranchNum++;
                return;
            }
            value += Time.deltaTime;
            img_Timer.transform.parent.gameObject.SetActive(true);
            img_Timer.fillAmount = value / 5.0f;
        }
    }
}
