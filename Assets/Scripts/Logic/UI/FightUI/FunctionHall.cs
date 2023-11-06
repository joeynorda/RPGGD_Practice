using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;



//功能大厅
public class FunctionHall
{
    GameObject _root;

    RoleAttrDlg _roleAttrDlg;

    public FunctionHall()
    {
        _root = UIMgr.Instance.Add("UI/FightUI/FunctionHall", UILayer.FightUI);

        var btnRoleAttr = _root.Find<Button>("BtnRole");

        btnRoleAttr.onClick.AddListener(OnRoleAttrClick);


        var btnInventory = _root.Find<Button>("BtnInventory");

        btnInventory.onClick.AddListener(OnInventoryBtnClick);
    }

    private void OnInventoryBtnClick()
    {
        var inventoryDlg = DialogMgr.Instance.Open<InventoryDlg>();
        Debug.Log("<color=#7FFF00><size=12>" + $"inventoryDlg.IsAlive:{inventoryDlg.IsAlive}" + "</size></color>");
    }

    private void OnRoleAttrClick()
    {
        var rolDlg = DialogMgr.Instance.Open<RoleAttrDlg>();
        Debug.Log("<color=#7FFF00><size=12>" + $"rolDlg.IsAlive:{rolDlg.IsAlive}" + "</size></color>");
    }
}
