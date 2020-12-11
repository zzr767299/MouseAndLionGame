using SimpleFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFrameWork.Timer;
/// <summary>
/// 音频管理
/// </summary>

class AudioManager
{
    private Dictionary<AudioClip, Queue<GameObject>> pool;
    private int maxCount = int.MaxValue;
    public int MaxCount
    {
        get { return maxCount; }
        set
        {
            maxCount = Mathf.Clamp(value, 0, int.MaxValue);
        }
    }


    private GameManager gameManager;
    private AudioSource musicAudioSouce;
    private AudioSource soundAudioSource;

    public AudioManager()
    {
        pool = new Dictionary<AudioClip, Queue<GameObject>>();
        gameManager = GameManager.Instance;
        AudioSource[] audioSouces = gameManager.GetComponents<AudioSource>();
        musicAudioSouce = audioSouces[0];
        soundAudioSource = audioSouces[1];
    }
 

    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="pos">声音播放的位置</param>
    /// <param name="audioPath">加载音效的路径</param>
    /// <param name="isFolderPath">是否需要加载整个音频文件夹，默认加载单个音频资源</param>
    /// <param name="volume">音频的音量，0-1，默认是1</param>
    public void PlayAudioAtPoint(Vector3 pos, string audioPath, bool isFolderPath = false, bool is3DSound = true, float volume = 1)
    {
        AudioClip clip = null;
        if (clip == null)
        {
            if (isFolderPath)
            {
                AudioClip[] clips = gameManager.GetAudioClips(audioPath);
                if (clips == null || clips.Length <= 0)
                {
                    Debug.LogError("当前传递的路径错误,当前音频的路径为:AudioClips/" + audioPath);
                    return;
                }
                clip = clips[Random.Range(0, clips.Length)];
            }
            else
            {
                // clip = ResourceHelper.LoadRes<AudioClip>(audioPath);
                clip = gameManager.GetAudioClipRes(audioPath);
            }

            if (clip == null)
            {
                Debug.LogError("当前传递的路径错误,当前音频的路径为:AudioClips/" + audioPath);
                return;
            }
        }

        GameObject newObject = null;
        AudioSource audio = null;
        if (!pool.ContainsKey(clip))
        {
            pool.Add(clip, new Queue<GameObject>());
        }
        if (pool[clip].Count == 0)
        {
            newObject = new GameObject();
            audio = newObject.AddComponent<AudioSource>();
            newObject.name = "Audio";
        }
        else
        {

            newObject = pool[clip].Dequeue();
            newObject.SetActive(true);
            audio = newObject.GetComponent<AudioSource>();

        }

        newObject.transform.position = pos;
        audio.volume = volume;
        audio.clip = clip;
        if (is3DSound)
        {
            audio.spatialBlend = 1;
        }
        audio.maxDistance = 30;
        audio.Play();
        PutObject(newObject, clip);



    }

    public void PutObject(GameObject go, AudioClip clip)
    {

        TimerSystem.Instance.AddTimerTask(()=>
        {
            go.SetActive(false);
            pool[clip].Enqueue(go);
        }, clip.length *1000, 0);
    }


    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySound(string soundName)
    {
        soundAudioSource.PlayOneShot(gameManager.GetAudioClipRes(soundName));
    }
    /// <summary>
    /// 随机播放一个文件夹中的一个音效
    /// </summary>
    /// <param name="folderName"></param>
    public void PlaySounds(string folderName)
    {
        AudioClip[] clips = gameManager.GetAudioClips(folderName);
        soundAudioSource.PlayOneShot(clips[Random.Range(0,clips.Length)]);
    }
    /// <summary>
    /// 背景音乐播放
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="loop"></param>
    public void PlayMusic(string musicName, bool loop)
    {
        musicAudioSouce.clip = gameManager.GetAudioClipRes(musicName);
        musicAudioSouce.loop = loop;
        musicAudioSouce.Play();
    }
    /// <summary>
    /// 停止背景音乐的方法
    /// </summary>
    public void StopMusic()
    {
        musicAudioSouce.Stop();
    }
    /// <summary>
    /// 获取当前音乐播放到样本点位置
    /// </summary>
    /// <returns></returns>
    public int GetMusicSample()
    {
        return musicAudioSouce.timeSamples;
    }
}

