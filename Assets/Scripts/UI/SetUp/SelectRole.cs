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
    private int _lastRoleIndex = 1;

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

        //��ʼ����ɫ�б�
        InitUserData();

        _btnEnter.onClick.AddListener(OnBtnEnterClick);

    }


    private void InitUserData()
    {
        int i = 0;

        foreach (var roleIno in RoleTable.Instance.GetAll().Values)
        {
            var roleItem = ResMgr.Instance.GetInstance(roleItemPath); 

            var textName = roleItem.transform.Find<TextMeshProUGUI>("Label");

            var toggle = roleItem.GetComponent<Toggle>();
            toggle.group = _roleListToggleGroup;

            textName.text = roleIno.Name;

            //�հ�
            var roleIndex = i;
            ++i;
            toggle.onValueChanged.AddListener((isOn)=>OnToggleValueChanged(roleIndex,isOn));

            roleItem.transform.SetParent(_roleListContent.transform);

            toggle.isOn = roleIndex == _lastRoleIndex;
        }
    }



    private void OnToggleValueChanged(int index,bool isOn)
    {
        if (isOn)
        {
            if (index == _selectRoleIndex) return;

            //�����֮ǰ���µ�ģ��  ����ӽڵ�
            _placeTrans.ClearAllChilds();

            _selectRoleIndex = index;

            var curRoleInfo = RoleTable.Instance[index];

            var model = ResMgr.Instance.GetInstance(curRoleInfo.ModelPath);
            //var model = GameObject.Instantiate(Resources.Load<GameObject>(curRoleInfo.ModelPath));
            model.transform.SetParent(_placeTrans, false);
        }
    }


    private void OnBtnEnterClick()
    {

    }
}
