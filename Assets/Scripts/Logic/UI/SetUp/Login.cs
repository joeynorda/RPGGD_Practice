using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//启动 闪屏logo 检查更新 更新重启 公告 登录
public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _accountInput;

    [SerializeField]
    private TMP_InputField _passwordInput;

    [SerializeField]
    private Button _btnOK;

    private void Awake()
    {
        _accountInput = transform.Find<TMP_InputField>("InputAccount");
        _passwordInput = transform.Find<TMP_InputField>("InputPassword");
        _btnOK = transform.Find<Button>("LoginBtn");

        _btnOK.onClick.AddListener(OnBtnOKClick);
    }

    private void OnBtnOKClick()
    {

        //发送
        var account = _accountInput.text;
        var password = _passwordInput.text;
        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            return;
        }

        _accountInput.enabled = false;
        _passwordInput.enabled = false;
        _btnOK.interactable = false;



        Debug.Log("<color=#7FFF00><size=12>" + $"点击连接服务器.." + "</size></color>");

        //连接服务器 等待返回数据
        Net.Instance.ConnectServer(DoSuccess,DoFailed);

        //SceneManager.LoadScene("SelectRole");
    }

    /// <summary>
    /// 连接服务器失败回调
    /// </summary>
    private void DoFailed()
    {
        _accountInput.enabled = true;
        _passwordInput.enabled = true;
        _btnOK.interactable = true;
    }


    /// <summary>
    /// 连接成功回调
    /// </summary>
    private void DoSuccess()
    {
        var account = _accountInput.text;
        var password = _passwordInput.text;

        Debug.Log("<color=#7FFF00><size=12>" + $"连接服务器成功后  给服务器发送LoginCommand" + "</size></color>");

        //连接服务器成功后  给服务器发送LoginCommand
        var cmd = new LoginCmd() { Account = account, Password = password };
        Net.Instance.SendCmd(cmd);
    }
}
