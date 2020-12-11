using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace SimpleFrameWork.Resouces
{
	/// <summary>
	/// 配置文件读取器 , 
	/// </summary>
	public class ReadConfig
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="filePath">StreamingAssets目录下的文件名称</param>
        /// <returns></returns>
        public static string ReadConfigFile(string filePath)
        {
            //备注：因为部分安卓平台无法获取该文件，所以建议分平台自行判断。
            //string path = Application.streamingAssetsPath + "/AI_01.txt";
            string path;
#if UNITY_EDITOR || UNITY_STANDALONE
            path =  Application.dataPath + "/StreamingAssets/" + filePath;
#elif UNITY_ANDROID
              path = "jar:file://" + Application.dataPath + "!/assets/"+ filePath;
#elif UNITY_IPHONE
              path = "file://" + Application.dataPath + "/Raw/"+ filePath;
#endif
            //备注：因为在移动端中，无法获取文件的绝对路径，IO不能使用，所以使用WWW读取。




            return File.ReadAllText(path);
        }

        /// <summary>
        /// 加载配置文件 , 委托中写处理每一行string  的逻辑
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="handle">处理逻辑</param>
        public static void LoadConfigFile(string configFile,Action<string> handle )
        { 
            using (StringReader reader = new StringReader(configFile))
            { 
                string line = null; 
                while ((line = reader.ReadLine()) != null)
                {
                    handle(line);
                }
            }
        }
    }
}