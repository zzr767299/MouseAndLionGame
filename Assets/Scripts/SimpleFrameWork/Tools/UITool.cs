using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace SimpleFrameWork.UI
{

    public static class UITool
    {
        private static GameObject m_CanvasObj = null;

        public static void ReleaseCanvas()
        {
            m_CanvasObj = null;
        }

        /// <summary>
        /// 寻找在Canvas下面的游戏物体
        /// </summary>
        /// <param name="UIName">UI的名称</param>
        /// <returns></returns>
        public static GameObject FindUIGameObject(string UIName)
        {
            if (m_CanvasObj == null)
                m_CanvasObj = UnityTool.FindGameObject("Canvas");
            if (m_CanvasObj == null)
                return null;
            return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
        }

        /// <summary>
        /// 查找并获取UI组件
        /// </summary>
        /// <returns></returns>
        public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
        {
            // 找出子物件 
            GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
            if (ChildGameObject == null)
                return null;

            T tempObj = ChildGameObject.GetComponent<T>();
            if (tempObj == null)
            {
                Debug.LogWarning("组件[" + UIName + "]不是[" + typeof(T) + "]");
                return null;
            }
            return tempObj;
        }

        /// <summary>
        /// 获取到Button组件
        /// </summary>
        /// <param name="BtnName"></param>
        /// <returns></returns>
        public static Button GetButton(string BtnName)
        {
            // 取得Canvas
            GameObject UIRoot = GameObject.Find("Canvas");
            if (UIRoot == null)
            {
                Debug.LogWarning("场景中沒有UI Canvas");
                return null;
            }

            Transform[] allChildren = UIRoot.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == BtnName)
                {
                    Button tmpBtn = child.gameObject.GetComponent<Button>();
                    if (tmpBtn == null)
                        Debug.LogWarning("UI原件[" + BtnName + "]不是Button");
                    return tmpBtn;
                }
            }

            Debug.LogWarning("UI Canvas中没有Button[" + BtnName + "]存在");
            return null;
        }
        public static Image GetImage(string imgName)
        {
            return GetUIComponent<Image>(imgName);
        }
        public static Text GetText(string txtName)
        {
            return GetUIComponent<Text>(txtName);
        }


        // 取得UI元件
        public static T GetUIComponent<T>(string UIName) where T : UnityEngine.Component
        {
            // 取得Canvas
            GameObject UIRoot = GameObject.Find("Canvas");
            if (UIRoot == null)
            {
                Debug.LogWarning("场景中没有UI Canvas");
                return null;
            }
            return GetUIComponent<T>(UIRoot, UIName);
        }
    }

}