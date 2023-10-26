using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EasyEditor:Editor
{
    //�����ļ�������
    //Resources ·����


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
        Debug.Log("<color=#7FFF00><size=12>" + $"�����ļ��������!" + "</size></color>");
    }
}