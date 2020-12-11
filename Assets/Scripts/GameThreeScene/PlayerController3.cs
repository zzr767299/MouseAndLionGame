using SimpleFrameWork;
using SimpleFrameWork.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController3 : MonoBehaviour
{
    public float speed;

    Animator animator;
    Vector3 movement;

    private GameManager gameManager;


    private GameObject HPs;
    private Image[] img_Hps;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameManager.Instance;
        HPs = UITool.FindUIGameObject("img_Hps");
        img_Hps = HPs.transform.GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
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
            transform.localScale = new Vector3(5, 5, 5);
        if (movement.x < 0)
            transform.localScale = new Vector3(-5, 5, 5);
    }

 
    public void Hit()
    {
        for (int i = 0; i < img_Hps.Length; i++)
        {
            if (gameManager.ThirdGameIndex >= 0)
            {
                img_Hps[gameManager.ThirdGameIndex].gameObject.SetActive(false);
            }


        }

        gameManager.ThirdGameIndex--;
    }
}
