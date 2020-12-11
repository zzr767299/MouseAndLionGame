using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SimpleFrameWork;
using SimpleFrameWork.Resouces;
using SimpleFrameWork.Timer;
/// <summary>
/// 资源管理者，管理所有所有物体的对象池，负责加载保存相应的资源
/// </summary>
public class ResourceManager
{
    private GameManager gameManager;
    public ResourceManager()
    {
        gameManager = GameManager.Instance;
        InitFactory();
        InitPoolDic();
    }

    #region 资源管理
    //资源工厂
    public ResFactory<Sprite> spritesFactory;
    public ResFactory<GameObject> prefabsFactory;
    public ResFactory<AudioClip> audioClipFactory;
    public ResFactory<TextAsset> textAssetFactory;

    public ResFactory<Sprite[]> spritesFactoryList;
    public ResFactory<GameObject[]> prefabsFactoryList;
    public ResFactory<AudioClip[]> audioClipFactoryList;
    //工厂默认路径
    public const string spritesResPath = "Sprites/";
    public const string prefabsResPath = "Prefabs/";
    public const string audioclipResPath = "AudioClips/";

    /// <summary>
    /// 初始化资源工厂
    /// </summary>
    public void InitFactory()
    {
        spritesFactory = new ResFactory<Sprite>();
        spritesFactoryList = new ResFactory<Sprite[]>();
        spritesFactory.InitFactory(
            (resName) =>
            {
                if (spritesFactory.resDic.ContainsKey(resName))
                {
                    return spritesFactory.resDic[resName];
                }
                else
                {
                    // return Resources.Load<Sprite>(spritesResPath + resName);
                    return ResourceTool.LoadRes<Sprite>(resName);
                }
            }
            );
        spritesFactoryList.InitFactory(
        (folderName) =>
        {
            if (spritesFactoryList.resDic.ContainsKey(folderName))
            {
                return spritesFactoryList.resDic[folderName];
            }
            else 
            {
                return Resources.LoadAll<Sprite>(spritesResPath + folderName);
            }
        });



        prefabsFactory = new ResFactory<GameObject>();
        prefabsFactoryList = new ResFactory<GameObject[]>();
        prefabsFactory.InitFactory(
             (resName) =>
             {
                 if (prefabsFactory.resDic.ContainsKey(resName))
                 {
                     return prefabsFactory.resDic[resName];
                 }
                 else
                 {
                     //return Resources.Load<GameObject>(prefabsResPath + resName);
                     return ResourceTool.LoadRes<GameObject>(resName);
                 }
             }
            );
        prefabsFactoryList.InitFactory(
        (folderName) =>
        {
            if (prefabsFactoryList.resDic.ContainsKey(folderName))
            {
                return prefabsFactoryList.resDic[folderName];
            }
            else
            {
                return Resources.LoadAll<GameObject>(prefabsResPath + folderName);
            }
        });

        audioClipFactory = new ResFactory<AudioClip>();
        audioClipFactoryList = new ResFactory<AudioClip[]>();
        audioClipFactory.InitFactory(
           (resName) =>
           {
               if (audioClipFactory.resDic.ContainsKey(resName))
               {
                   return audioClipFactory.resDic[resName];
               }
               else
               {
                   // return Resources.Load<AudioClip>(audioclipResPath + resName);
                   return ResourceTool.LoadRes<AudioClip>(resName);
               }
           }
            );


        audioClipFactoryList.InitFactory(
       (folderName) =>
       {
           if (audioClipFactoryList.resDic.ContainsKey(folderName))
           {
      
               return audioClipFactoryList.resDic[folderName];
           }
           else
           {
               return Resources.LoadAll<AudioClip>(audioclipResPath + folderName);
           }
       });
       
        textAssetFactory = new ResFactory<TextAsset>();
        textAssetFactory.InitFactory(
            (resName) =>
        {
            if (audioClipFactoryList.resDic.ContainsKey(resName))
            {
                return textAssetFactory.resDic[resName];
            }
            else
            {
                return ResourceTool.LoadRes<TextAsset>(resName);
            }
        });
    }
    /// <summary>
    /// 获取相应资源
    /// </summary>
    /// <param name="resName"></param>
    /// <returns></returns>
    public Sprite GetSpriteRes(string resName)
    {
        return spritesFactory.GetRes(resName);
    }
    public Sprite[] GetSprites(string folderName)
    {
        return spritesFactoryList.GetRes(folderName);
    }
    public AudioClip GetAudioclipRes(string resName)
    {
        return audioClipFactory.GetRes(resName);
    }
    public AudioClip[] GetAudioclips(string folderName)
    {
        return audioClipFactoryList.GetRes(folderName);
    }
    public GameObject GetPrefabRes(string resName)
    {
        return prefabsFactory.GetRes(resName);
    }
    public GameObject[] GetPrefabs(string folderName)
    {
        return prefabsFactoryList.GetRes(folderName);
    }

    public TextAsset GetTextAsset(string textName)
    {
        return textAssetFactory.GetRes(textName);
    }

    #endregion

    #region 对象池
    //对象池字典
    public Dictionary<string, GameObjectPool> poolDic;
    /// <summary>
    /// 对象池字典的初始化方法
    /// </summary>
    public void InitPoolDic()
    {
        poolDic = new Dictionary<string, GameObjectPool>();
    }
    /// <summary>
    /// 获取对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj(string objName, int recycleTime = 3,int initCount = 0)
    {
        if (!poolDic.ContainsKey(objName))
        {
            poolDic.Add(objName, new GameObjectPool(prefabsFactory, objName, initCount));
        }
       
        return poolDic[objName].GetObj(objName,recycleTime);
    }
    /// <summary>
    /// 回收对象
    /// </summary>
    public void RecycleObj(string objName, GameObject obj, Action resetMethod = null)
    {
        if (!poolDic.ContainsKey(objName))
        {
            Debug.Log("没有'" + objName + "'类型的游戏物体对象池");
            return;
        }
        poolDic[objName].Recycle(obj, resetMethod);
    }
    #endregion
}
