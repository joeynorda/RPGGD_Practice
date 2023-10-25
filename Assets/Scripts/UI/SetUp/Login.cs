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
        //���ӷ����� �ȴ���������

        //�����ݴ���

        SceneManager.LoadScene("SelectRole");

        Debug.Log("<color=#7FFF00><size=12>" + $"btn ok" + "</size></color>");
    }

}
