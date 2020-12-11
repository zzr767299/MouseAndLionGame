//-----------------------------------------------
//脚本说明：
//
//-----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


namespace SimpleFrameWork.Resouces
{

    public class ResInfoCreator
    {
        [MenuItem("Tools/CreateResMap")]
        public static void CreateResMap()
        {
            string path = Application.dataPath + "/Resources/";
            List<FileInfo> fileinfos = new List<FileInfo>();
            List<FileInfo> cache = new List<FileInfo>();
            GetAllObjectAtPath(path, fileinfos);

            // fileinfos.Operate(a => { if (a.FullName.EndsWith(".meta")) fileinfos.Remove(a); });

            for (int i = 0; i < fileinfos.Count; i++)
            {
                if (!fileinfos[i].FullName.EndsWith(".meta"))
                {
                    cache.Add(fileinfos[i]);
                }



            }

            FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets/ResMap.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            
            for (int i = 0; i < cache.Count; i++)
            {
                string relativePathByResources = "";
                string filesName = Path.GetFileNameWithoutExtension(cache[i].FullName);


                string totalPath = cache[i].FullName.Split('.')[0];

                string[] scrap = totalPath.Split('\\');
                bool @lock = false;
                
                for (int x = 0; x < scrap.Length; x++)
                {
                    if (scrap[x] == "Resources" || @lock)
                    {
                        if (@lock)
                        {
                            if (x != scrap.Length - 1)
                            {
                                relativePathByResources += scrap[x] + "/";
                            }
                            else
                            {
                                relativePathByResources += scrap[x];
                            }
                          
                        }
                        @lock = true;
                    }
                }

              //  Debug.Log(filesName + "=" + relativePathByResources);

               writer.WriteLine(filesName + "=" + relativePathByResources);

               
            }

            writer.Close();

            AssetDatabase.Refresh();

            // fileinfos.ToArray().Operate(a => Debug.Log(a.FullName));


            //FileInfo[] files = dir.GetFiles();


            // files.Print(a => Debug.Log(a.FullName));

            //Debug.Log("The number of this array is:" + allResource.Length);
            ///
            ///获取 给定 UnityEngine.Object 的 绝对路径。
            ///
            //string path = AssetDatabase.GetAssetPath(allResource[0]);

            // allResource.Print(a => Debug.Log(a.name));



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">the obsolute path</param>
        /// <returns></returns>
        public static void GetAllObjectAtPath(string path, List<FileInfo> cache)
        {

            DirectoryInfo layer = new DirectoryInfo(path);

            FileInfo[] files = layer.GetFiles();
            cache.AddRange(files);

            DirectoryInfo[] subLayerce = layer.GetDirectories();

            if (subLayerce.Length > 0)
            {
                for (int i = 0; i < subLayerce.Length; i++)
                {
                    GetAllObjectAtPath(subLayerce[i].FullName, cache);
                }
            }

        }


    }
}

