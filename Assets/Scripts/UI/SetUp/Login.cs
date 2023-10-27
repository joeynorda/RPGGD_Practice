using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//���� ����logo ������ �������� ���� ��¼
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

        //����
        var account = _accountInput.text;
        var password = _passwordInput.text;
        if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
        {
            return;
        }

        _accountInput.enabled = false;
        _passwordInput.enabled = false;
        _btnOK.interactable = false;



        Debug.Log("<color=#7FFF00><size=12>" + $"������ӷ�����.." + "</size></color>");

        //���ӷ����� �ȴ���������
        Net.Instance.ConnectServer(DoSuccess,DoFailed);

        //SceneManager.LoadScene("SelectRole");
    }

    /// <summary>
    /// ���ӷ�����ʧ�ܻص�
    /// </summary>
    private void DoFailed()
    {
        _accountInput.enabled = true;
        _passwordInput.enabled = true;
        _btnOK.interactable = true;
    }


    /// <summary>
    /// ���ӳɹ��ص�
    /// </summary>
    private void DoSuccess()
    {
        var account = _accountInput.text;
        var password = _passwordInput.text;

        Debug.Log("<color=#7FFF00><size=12>" + $"���ӷ������ɹ���  ������������LoginCommand" + "</size></color>");

        //���ӷ������ɹ���  ������������LoginCommand
        var cmd = new LoginCmd() { Account = account, Password = password };
        Net.Instance.SendCmd(cmd);
    }
}
