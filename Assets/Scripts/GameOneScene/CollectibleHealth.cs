using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFrameWork;
using SimpleFrameWork.UI;
public class CollectibleHealth : MonoBehaviour
{
    protected Image img_Timer;
    protected bool isActive;
    protected float value;

   protected virtual void Start()
    {
        img_Timer = transform.Find("UICanvas/img_BG/img_Timer").GetComponent<Image>();
        img_Timer.fillAmount = 0.0f;
        img_Timer.transform.parent.gameObject.SetActive(false);
    
    }

   protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.Player)
        {
            isActive = true;
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == Tags.Player)
        {
            isActive = false;
            img_Timer.transform.parent.gameObject.SetActive(false);
            value = 0;
        }
    }

    protected virtual void Update()
    {

        if (isActive)
        {
            if (value >= 5)
            {
                Destroy(gameObject);
                GameManager.Instance.FruitNum++;
                return;
            }
            value += Time.deltaTime;
            img_Timer.transform.parent.gameObject.SetActive(true);
            img_Timer.fillAmount = value / 5.0f;
        }
    }
}
