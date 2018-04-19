
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class mip : MonoBehaviour
{

    // Use this for initialization
    [MenuItem("Tools/font check")]
    static void CheckSceneSetting()
    {
        List<string> dirs = new List<string>();//存放所有预制体路径
        GetDirs(Application.dataPath, ref dirs);
    }

    private static void GetDirs(string dirPath, ref List<string> dirs)
    {
        foreach (string path in Directory.GetFiles(dirPath))//得到目录中的所有文件
        {
            if (System.IO.Path.GetExtension(path) == ".prefab")
            {
                dirs.Add(path.Substring(path.IndexOf("Assets")));//将unity的工程中预制体路径放在list表中
                GameObject obj = (GameObject)AssetDatabase.LoadAssetAtPath(path.Substring(path.IndexOf("Assets")), typeof(GameObject));//加载工程文件中的预制体到内存中
                if (obj != null)
                {
                    GameObject instanse = Instantiate<GameObject>(obj);
                    UILabel[] labels = instanse.GetComponentsInChildren<UILabel>();
                    foreach (UILabel label in labels)
                    {
                        if (label.trueTypeFont != null && label.trueTypeFont.name.Contains("Lucida"))
                        {
                            Debug.Log(label.trueTypeFont.name);
                            Debug.Log(path.Substring(path.IndexOf("Assets")));
                        }
                        if (label.bitmapFont != null && label.bitmapFont.name.Contains("Lucida"))
                        {
                            Debug.Log(label.bitmapFont.name);
                            Debug.Log(path.Substring(path.IndexOf("Assets")));
                        }       
                    }
                    DestroyImmediate(instanse);
                }

            }
        }
    }

}

