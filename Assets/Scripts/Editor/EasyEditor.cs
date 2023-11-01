using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EasyEditor:Editor
{
    //配置文件放置在
    //Resources 路径下


    [MenuItem("Custom/ConfigToResources")]
    public static void ConfigToResources()
    {
        var srcPath = Application.dataPath + "/../Config/";
        var dstPath = Application.dataPath + "/Resources/Config/";

        //copy
        foreach (var filePath in Directory.GetFiles(srcPath))
        {
            var fileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            File.Copy(filePath, dstPath + fileName + ".bytes",true);
        }
        Debug.Log("<color=#7FFF00><size=12>" + $"配置文件复制完毕!" + "</size></color>");
    }


    [MenuItem("Custom/GotoSetUp")]
    public static void GotoSetUp()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/Setup.unity");
    }

    [MenuItem("Custom/GotoUIEditor")]
    public static void GotoUIEditor()
    {
        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/UIEditor.unity");
    }
}