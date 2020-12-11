using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFrameWork.Resouces
{
    /// <summary>
    /// 用于获取Resources 文件夹下的资源
    /// </summary>
    public class ResourceTool
    {
        private static Dictionary<string, string> map;
        static ResourceTool()
        {
            map = new Dictionary<string, string>();
            CreateMap();
        }
        private static void CreateMap()
        {

            string content = ReadConfig.ReadConfigFile("ResMap.txt");
            ReadConfig.LoadConfigFile(content, MapBuild);
        }

        private static void MapBuild(string line)
        {
            string[] keyValue = line.Split('=');
            map.Add(keyValue[0], keyValue[1]);
        }
        /// <summary>
        /// 获取想要的资源，必须在Resources 文件夹下，必须是ResMap 中有记录的。
        /// </summary>
        /// <typeparam name="T">获取的资源的类型</typeparam>
        /// <param name="ResName">资源名字</param>
        /// <returns></returns>
        public static T LoadRes<T>(string ResName) where T : UnityEngine.Object
        {
            //Debug.Log("resName:" + ResName);
            if (!map.ContainsKey(ResName))
            {
                Debug.LogError("指定的 Key ：" + ResName + "，不在表中，请确认 ResMap 表格");
                return null;
            }
            else
                return Resources.Load<T>(map[ResName]);
        }
    }
}