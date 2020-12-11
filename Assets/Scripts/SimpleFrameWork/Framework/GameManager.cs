using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleFrameWork.Timer;

namespace SimpleFrameWork
{

    public class GameManager : MonoSingleton<GameManager>
    {
        #region Manager
        private ResourceManager resourceManager;
        private UIManager uiManager;
        private AudioManager audioManager;
        #endregion

        SceneStateController controller;

        #region FirstScene
        private int fisrtGameIndex = 4;
        private float firstTimer = 60.0f;

        private int fruitNum;
        #endregion

        #region MainScene
        public TextAsset textFile;
        private Sprite[] backGrounds;
        #endregion

        #region TextAsset
        private TextAsset select1;
        private TextAsset select2;
        private TextAsset polt1_1;
        private TextAsset polt1_2;
        private TextAsset polt2_1;
        private TextAsset polt2_2;
        private TextAsset end1;
        private TextAsset end2;
        private TextAsset end3;
        #endregion


        #region GameTreeScene
        private int branchNum;
        private int thirdGameIndex = 4;
        private float thirdTimer = 60.0f;
        #endregion
        #region Property 
        public bool IsContiute { get => isContiute; set => isContiute = value; }
        public SceneStateController Controller { get => controller; set => controller = value; }

        public int FruitNum { get => fruitNum; set => fruitNum = value; }
        public int FisrtGameIndex { get => fisrtGameIndex; set => fisrtGameIndex = value; }
        public float FirstTimer { get => firstTimer; set => firstTimer = value; }
        public Sprite[] BackGrounds { get => backGrounds; set => backGrounds = value; }
        public int BranchNum { get => branchNum; set => branchNum = value; }
        public int ThirdGameIndex { get => thirdGameIndex; set => thirdGameIndex = value; }
        public float ThirdTimer { get => thirdTimer; set => thirdTimer = value; }
        public TextAsset Select1 { get => select1; set => select1 = value; }
        public TextAsset Select2 { get => select2; set => select2 = value; }
        public TextAsset Polt1_1 { get => polt1_1; set => polt1_1 = value; }
        public TextAsset Polt1_2 { get => polt1_2; set => polt1_2 = value; }
        public TextAsset Polt2_1 { get => polt2_1; set => polt2_1 = value; }
        public TextAsset Polt2_2 { get => polt2_2; set => polt2_2 = value; }
        public TextAsset End1 { get => end1; set => end1 = value; }
        public TextAsset End2 { get => end2; set => end2 = value; }
        public TextAsset End3 { get => end3; set => end3 = value; }
        #endregion
        private bool isContiute;
        private void Awake()
        {

            Controller = new SceneStateController();
      
            DontDestroyOnLoad(gameObject);
          
            InitComponent();
            InitValue();
            InitManager();
            InitBGTexture();
        }
        private void Start()
        {
            InitAssetText();
      
            Controller.SetState(new StartScene(Controller), false);
        
          
        }

        void Update()
        {
            Controller.StateUpdate();

            //Test
           //if (Input.GetKeyDown(KeyCode.Space))
           //{
           //    Controller.SetState(new GameOneScene(Controller));
           //
           //}
            if (Input.GetKeyDown(KeyCode.L))
            {
                Time.timeScale = 5;

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                Time.timeScale = 1;

            }

        }

        public void ResetGameOneScene()
        {
            fruitNum = 0;
            fisrtGameIndex = 4;
            firstTimer = 120;
        }
        private void InitAssetText()
        {
            Select1 = GetTextAsset("Select1");
            Select2 = GetTextAsset("Select2");
            Polt1_1 = GetTextAsset("Polt1_1");
            Polt1_2 = GetTextAsset("Polt1_2");
            Polt2_1 = GetTextAsset("Polt2_1");
            Polt2_2 = GetTextAsset("Polt2_2");
            End1 = GetTextAsset("End1");
            End2 = GetTextAsset("End2");
            End3 = GetTextAsset("End3");
        }
        private void InitBGTexture()
        {
            BackGrounds = resourceManager.GetSprites("BackGrounds");
            Debug.Log("BackGrounds=" + BackGrounds.Length);
        }

        private void InitValue()
        {
            gameObject.name = "GameManager";

        }

        private void InitManager()
        {
            InitUIManager();
            resourceManager = new ResourceManager();
            audioManager = new AudioManager();

        }

        private void InitUIManager()
        {
            if (GameObject.Find("Canvas") == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<Canvas>();
                go.AddComponent<UIManager>();
                go.name = "Canvas";
            }
            else
            {
                uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            }
        }
        private void InitComponent()
        {
            gameObject.AddComponent<AudioSource>();
            gameObject.AddComponent<AudioSource>();
            gameObject.AddComponent<TimerSystem>();
        }

        #region 资源管理
        public Sprite GetSpriteRes(string resName)
        {
            return resourceManager.GetSpriteRes(resName);
        }
        public Sprite[] GetSprites(string folderName)
        {
            return resourceManager.GetSprites(folderName);
        }
        public AudioClip GetAudioClipRes(string resName)
        {
            return resourceManager.GetAudioclipRes(resName);
        }
        public AudioClip[] GetAudioClips(string folderName)
        {
            return resourceManager.GetAudioclips(folderName);
        }

        public GameObject GetPrefabRes(string resName)
        {
            return resourceManager.GetPrefabRes(resName);
        }
        public GameObject[] GetPrefabs(string folderName)
        {
            return resourceManager.GetPrefabs(folderName);
        }
        public TextAsset GetTextAsset(string txtName)
        {
            return resourceManager.GetTextAsset(txtName);
        }

        public GameObject GetObj(string objName, int recycleTime = 3,int initCount = 0)
        {
            return resourceManager.GetObj(objName, recycleTime,initCount);
        }
        public void RecycleObj(string objName, GameObject obj, Action resetMethod = null)
        {
            resourceManager.RecycleObj(objName, obj, resetMethod);
        }
        #endregion

        #region 音频管理
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="soundName"></param>
        public void PlaySound(string soundName)
        {
            audioManager.PlaySound(soundName);
        }
        /// <summary>
        /// 随机播放一个文件夹中一个音效
        /// </summary>
        public void PlaySounds(string folderName)
        {
            audioManager.PlaySounds(folderName);
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="musicName"></param>
        /// <param name="loop"></param>
        public void PlayMusic(string musicName, bool loop = true)
        {
            audioManager.PlayMusic(musicName, loop);
        }
        /// <summary>
        /// 停止音乐
        /// </summary>
        public void StopMusic()
        {
            audioManager.StopMusic();
        }

        public void PlayAudioAtPoint(Vector3 pos, string audioPath, bool isFolderPath = false, bool is3DSound = true, float volume = 1)
        {
            audioManager.PlayAudioAtPoint(pos, audioPath, isFolderPath, is3DSound, volume);
        }
        #endregion

        #region 游戏管理

        #endregion

   
    }

    
}

