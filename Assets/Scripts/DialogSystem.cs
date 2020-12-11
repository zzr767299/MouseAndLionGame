using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFrameWork.UI;
using SimpleFrameWork.Event;
using SimpleFrameWork;
public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;
    public GameObject panel;

    [Header("文本文件")]


    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite Jerry;
    public Sprite Lion;

    private bool isFinished;

    private Button btn_Select1_1;
    private Button btn_Select1_2;
    private GameObject btnSelectThree;

    private Image img_BG;

    List<string> textlist = new List<string>();

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
        //panel.SetActive(false);
        GetTextFormFile(gameManager.textFile);
        index = 0;

     
   
    }

    private void Start()
    {
        btnSelectThree = UITool.FindUIGameObject("btnSelectsThree");
        btn_Select1_1 = UITool.GetButton("btn_Select1_1");
        btn_Select1_2 = UITool.GetButton("btn_Select1_2");
        img_BG = UITool.GetImage("BG");


        EventCenter.AddListener<TextAsset>(EventDefine.SetFromTextAsset, GetTextFormFile);

        btnSelectThree.SetActive(false);


        btn_Select1_1.onClick.AddListener(() =>
        {
           gameManager.Controller.SetState(new GameTwoScene(gameManager.Controller));
        });
        btn_Select1_2.onClick.AddListener(() =>
        {

        });
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<TextAsset>(EventDefine.SetFromTextAsset, GetTextFormFile);
    }
    private void OnEnable()
    {
        StartCoroutine(SetTextUI());
        isFinished = true;
        //textLabel.text = textlist[index];
        //index++;
    }

    private void Update()
    {
        if (gameManager.IsContiute) return;
        if (Input.GetKeyDown(KeyCode.R) && index ==textlist.Count)
        {
            index = 0;
            panel.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isFinished)
            {
                StartCoroutine(SetTextUI());
            }
         
            //textLabel.text = textlist[index];
            //index++;
        }
    }

    IEnumerator SetTextUI()
    {
        isFinished = false;
        textLabel.text = "";
        // Debug.Log("textlist[index]:" + textlist[index]);
        string str = textlist[index].Split(':')[0];
        Debug.Log("str:" + str);
        switch (str)
        {
          
           case "Jerry":
                Debug.Log("Jerry");
                faceImage.gameObject.SetActive(true);
                faceImage.sprite = Jerry;
               break;
           case "Lion":
                Debug.Log("Lion");
                faceImage.gameObject.SetActive(true);
                faceImage.sprite = Lion;
               break;
           case "你立刻......":
                gameManager.IsContiute = true;
                EventCenter.Broadcast(EventDefine.ShowSelectOneBtn);
                GetTextFormFile(gameManager.Select1);
                break;
            case "进入游戏一":
                gameManager.Controller.SetState(new GameOneScene(gameManager.Controller));
                break;
            case "挣扎":
                gameManager.IsContiute = true;
                EventCenter.Broadcast(EventDefine.ShowSelectTwoBtn);
                GetTextFormFile(gameManager.Select2);
                break;
            case "进入游戏三":
                gameManager.Controller.SetState(new GameThreeScene(gameManager.Controller));
                break;
            case "进入游戏四":
                gameManager.Controller.SetState(new GameFourScene(gameManager.Controller));
                break;
            case "选择":
                gameManager.IsContiute = true;
                btnSelectThree.SetActive(true);
                break;
            case "场景二":
                Debug.Log("场景二:"+gameManager.BackGrounds.Length);
                img_BG.sprite = gameManager.BackGrounds[1];
                break;
            case "场景三":
                img_BG.sprite = gameManager.BackGrounds[2];
                break;
            case "场景四":
                img_BG.sprite = gameManager.BackGrounds[3];
                break;
            case "结束":
                gameManager.Controller.SetState(new StartScene(gameManager.Controller));
                break;
            default:
                faceImage.gameObject.SetActive(false);
                break;
        }

        for (int i = 0; i < textlist[index].Length; i++)
        {
            textLabel.text += textlist[index][i];

            yield return new WaitForSeconds(textSpeed);
        }

        index++;
        isFinished = true;
    }

    public void GetTextFormFile(TextAsset file)
    {
        Debug.Log("设置");
        textlist.Clear();
        index = 0;
        var lineDate = file.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
      
        foreach (var line in lineDate)
        {
            textlist.Add(line);
        }
    }


   

}
