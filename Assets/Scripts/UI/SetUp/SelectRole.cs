using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectRole : MonoBehaviour
{
    public GameObject _roleListContent;
    public Button _btnEnter;
    public ToggleGroup _roleListToggleGroup;

    private string roleItemPath = "UI/SelectRole/RoleItem";

    private int _selectRoleIndex = -1;
    private int _lastRoleIndex = 2;

    [SerializeField] private Transform _placeTrans;

    private TouchRotate _modelTouchRotate;


    private void Awake()
    {
        _roleListContent = transform.Find<Transform>("RoleList/Viewport/Content").gameObject;
        _btnEnter = transform.Find<Button>("BtnEnter");
        _roleListToggleGroup = _roleListContent.GetComponent<ToggleGroup>();
        _modelTouchRotate = transform.Find<TouchRotate>("RawImage");

        _modelTouchRotate.DragCallback = x => {
            _placeTrans.Rotate(Vector3.up, -x.delta.x);
        };

        //初始化角色列表
        InitUserData();

        _btnEnter.onClick.AddListener(OnBtnEnterClick);

    }


    private void InitUserData()
    {
        int i = 0;

        foreach (var roleIno in RoleTable.Instance.GetAll().Values)
        {
            var roleItem =GameObject.Instantiate(Resources.Load<GameObject>(roleItemPath));

            var textName = roleItem.transform.Find<TextMeshProUGUI>("Label");

            var toggle = roleItem.GetComponent<Toggle>();
            toggle.group = _roleListToggleGroup;

            textName.text = roleIno.Name;

            //闭包
            var roleIndex = i;
            ++i;
            toggle.onValueChanged.AddListener((isOn)=>OnToggleValueChanged(roleIndex,isOn));

            roleItem.transform.SetParent(_roleListContent.transform);

            toggle.isOn = roleIndex == _lastRoleIndex;
        }
    }



    private void OnToggleValueChanged(int index,bool isOn)
    {
        Debug.Log("<color=#7FFF00><size=12>" + $"{index},{isOn}" + "</size></color>");

        if (isOn)
        {
            if (index == _selectRoleIndex) return;

            //先清除之前留下的模型  清空子节点
            _placeTrans.ClearAllChilds();

            _selectRoleIndex = index;

            var curRoleInfo = RoleTable.Instance[index];

            Debug.Log("<color=#7FFF00><size=12>" + $"{curRoleInfo.ToString()}" + "</size></color>");

            var model = GameObject.Instantiate(Resources.Load<GameObject>(curRoleInfo.ModelPath));
            model.transform.SetParent(_placeTrans, false);
        }
    }


    private void OnBtnEnterClick()
    {

    }
}
